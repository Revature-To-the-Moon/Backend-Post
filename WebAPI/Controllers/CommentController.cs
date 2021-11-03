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
    public class CommentController : ControllerBase
    {
        private IBL _bl;

        public CommentController(IBL bl)
        {
            _bl = bl;
        }

        // GET: api/<CommentController>
        [HttpGet]
        public async Task<IEnumerable<Comment>> Get()
        {
            return await _bl.GetCommentListAsync();
        }


        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Comment selectedComment = await _bl.GetCommentByIdAsync(id);
            if (selectedComment != null)
            {
                return Ok(selectedComment);
            }
            else
            {
                return NoContent();
            }
        }


        // POST api/<CommentController>/5
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Comment comment)
        {
            Comment addComment = await _bl.AddCommentAsync(comment);
            return Created("api/[controller]", addComment);
        }


        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Comment newComment)
        {
            Comment updatedComment = await _bl.UpdateCommentAsync(newComment);
            return Ok(updatedComment);
        }


        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _bl.DeleteCommentAsync(id);
        }
    }
}
