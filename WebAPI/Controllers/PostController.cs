using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private IBL _bl;

        public PostController(IBL bl)
        {
            _bl = bl;
        }


        // GET: api/<PostController>
        [HttpGet]
        public async Task<IEnumerable<Root>> Get()
        {
            return await _bl.GetRootListAsync();
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Root selectedUser = await _bl.GetRootByIdAsync(id);
            if (selectedUser != null)
            {
                return Ok(selectedUser);
            }
            else
            {
                return NoContent();
            }
        }


    }
}
