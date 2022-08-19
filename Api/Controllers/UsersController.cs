using Data.Models;
using Data.Models.Users;
using Infostructure.InterfaceGeneric;
using Infostructure.IUser;
using Infostructure.IUser.IServicesToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModeDTO.UserDto;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/Identity/[controller]/[action]")]
    public class UsersController : baseApiController
    {
        private readonly ITokenServices token;
        private readonly IGenericRepositery<AppUser> repositery;
        private readonly IUniteOfWork<AppUser> uniteOfWork;
        private readonly IUserRepositery userRepositery;

        public UsersController(ITokenServices token, IGenericRepositery<AppUser> repositery, IUniteOfWork<AppUser> uniteOfWork, IUserRepositery userRepositery)
        {
            this.token = token;
            this.repositery = repositery;
            this.uniteOfWork = uniteOfWork;
            this.userRepositery = userRepositery;
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<AppUser>>> getall( )
        {
            var res = await repositery.ListAllAsync();
            return Ok(res);
        }
        [Authorize]
        [HttpGet("{id:int}", Name = "getByID")]
        public async Task<ActionResult<IEnumerable<AppUser>>> getByID(int id)
        {
            var res =await repositery.GetByIDAsync(id);
            return Ok(res);
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> register([FromBody] registerDto model)
        {
            try
            {
                if (await userExist(model.UserName))
                    return BadRequest("This UserName Is Exist");

                if (ModelState.IsValid)
                {

                    using var hmac = new HMACSHA512();
                    var user = new AppUser
                    {
                        UserName = model.UserName.ToLower(),
                        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password)),
                        passwordSalt=hmac.Key
                    };

                    await repositery.Add(user);
                    uniteOfWork.save();
                    return new UserDto
                    {
                        UserName=user.UserName,
                        Token=token.CreateToken(user)
                    };
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            //string url = Url.Link("getByID", new { id = model.Id });
            return BadRequest();
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login([FromBody] LoginDto model)
        {
            try
            {
                var user = await userRepositery.GetObjectAppUser(model.UserName);
                if (user == null)
                    return Unauthorized("Invalid UserName");


                using var hmac = new HMACSHA512(user.passwordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.passwordHash[i])
                        return Unauthorized("Invalid Password");
                }

                return new UserDto
                {
                    UserName = user.UserName,
                    Token = token.CreateToken(user)
                };
            }
            catch (System.Exception)
            {
                throw;
            }

        }
        private async Task<bool> userExist(string username)
        {
           return await userRepositery.GetFirst(username);
        }

    }
}
