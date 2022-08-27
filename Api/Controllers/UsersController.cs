using AutoMapper;
using Data.Models;
using Data.Models.Users;
using Infostructure.InterfaceGeneric;
using Infostructure.IUser;
using Infostructure.IUser.IServicesToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModeDTO.DtoAutoMapper;
using ModeDTO.UserDto;
using System.Collections.Generic;
using System.Security.Claims;
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
        private readonly IMapper mapper;

        public UsersController(
            ITokenServices token,
            IGenericRepositery<AppUser> repositery,
            IUniteOfWork<AppUser> uniteOfWork, IUserRepositery userRepositery,
            IMapper mapper
            )
        {
            this.token = token;
            this.repositery = repositery;
            this.uniteOfWork = uniteOfWork;
            this.userRepositery = userRepositery;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> getall()
        {
            var res = await repositery.ListAllAsync();

            var userReturn = mapper.Map<IEnumerable<MemberDto>>(res);
            return Ok(res);
        }
        [Authorize]
        [HttpGet("{id:int}", Name = "getByID")]
        public async Task<ActionResult<MemberDto>> getByID(int id)
        {
            var AppUser = await repositery.GetByIDAsync(id);
            var result = mapper.Map<MemberDto>(AppUser);
            return Ok(result);
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

                    var user = mapper.Map<AppUser>(model);


                    using var hmac = new HMACSHA512();
                    user.UserName = model.UserName.ToLower();
                    user.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
                    user.passwordSalt = hmac.Key;

                    await repositery.Add(user);
                    uniteOfWork.save();
                    return new UserDto
                    {
                        UserName = user.UserName,
                        Token = token.CreateToken(user),
                        KnownAs=user.KnownAs
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
                    Token = token.CreateToken(user),
                    KnownAs = user.KnownAs

                };
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        [HttpPost("UpdateUser")]
        public async Task<ActionResult<MemberDto>> UpdateUser(MemberUpdateDto model)
        {
            try
            {
                var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                AppUser user = await userRepositery.GetObjectAppUser(userName);

                mapper.Map(model, user);
                repositery.update(user);
                if (uniteOfWork.save()) return NoContent();

            }
            catch (System.Exception)
            {
                throw;
            }
            return BadRequest("Failed to update user");
        }

        private async Task<bool> userExist(string username)
        {
            return await userRepositery.GetFirst(username);
        }

    }
}
