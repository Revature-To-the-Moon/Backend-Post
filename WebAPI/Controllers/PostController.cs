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

        readonly private IBL _bl;

        public PostController(IBL bl)
        {
            _bl = bl;
        }


        // GET: api/<PostController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Root> rootList = await _bl.GetRootListAsync();
            if (rootList != null)
                {
                return Ok(rootList);
                }
            else
                {
                return NoContent();
                }
            }

                    // GET: api/<PostController>
        [HttpGet("Group/{id}")]
        public async Task<IActionResult> GetGroupPosts(int id)
        {
            List<Root> rootGroupList = await _bl.GetRootListByGroupIdAsync(id);
            if (rootGroupList != null)
                {
                return Ok(rootGroupList);
                }
            else
                {
                return NoContent();
                }
            }


        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Root selectedRoot = await _bl.GetRootByIdAsync(id);
            if (selectedRoot != null)
            {
                return Ok(selectedRoot);
            }
            else
            {
                return NoContent();
            }
        }


        // POST api/<PostController>/5
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Root root)
        {
            Root addRoot = await _bl.AddRootAsync(root);
            return Created("api/[controller]", addRoot);
        }


        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Root newRoot)
        {
            Root updatedRoot = await _bl.UpdateRootAsync(newRoot);
            return Ok(updatedRoot);
        }


        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Root delteRoot = await _bl.GetRootByIdAsync(id);
            await _bl.DeleteRootAsync(id);
            return Ok(delteRoot);
        }
    }
}
