using Data.Models.Users;
using Infostructure.InterfaceGeneric;
using Infostructure.IUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers
{
    public class BuggyController : baseApiController
    {
        private readonly IGenericRepositery<AppUser> repositery;

        public BuggyController(IGenericRepositery<AppUser> repositery)
        {
            this.repositery = repositery;
        }
        [Authorize]
        [HttpGet("Auth")]
        public ActionResult<string> GetNotFound()
        {
            return "Secret Text";
        }

        [HttpGet("non-found")]
        public ActionResult<AppUser> GetSecret()
        {
            var res = repositery.GetByIDAsync(-1);
            if (res == null) return NotFound();

            return Ok(res);
        }
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            try
            {
                var res = repositery.GetByIDAsync(-1);

                var thankToReturn = res.ToString();
                return Ok(thankToReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(500,"Server say no!");
            }
           
        }
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("this was not a good request");
        }

    }
}
