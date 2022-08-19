using Data.Models;
using Infostructure.InterfaceGeneric;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class UsersController : baseApiController
    {
        private readonly IGenericRepositery<test> repositery;

        public UsersController(IGenericRepositery<test> repositery)
        {
            this.repositery = repositery;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<test>>> getall( )
        {
            var res = await repositery.ListAllAsync();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<test>>> getByID(int id)
        {
            var res =await repositery.GetByIDAsync(id);
            return Ok(res);
        }
    }
}
