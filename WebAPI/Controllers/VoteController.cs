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
    public class VoteController : ControllerBase
    {
        readonly private IBL _bl;

        public VoteController(IBL bl)
        {
            _bl = bl;
        }

        // GET: api/<VoteController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Vote> voteList = await _bl.GetVoteListAsync();
            if (voteList != null)
                {
                return Ok(voteList);
                }
            else
                {
                return NoContent();
                }
            }


        // GET api/<VoteController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Vote selectedVote = await _bl.GetVoteByIdAsync(id);
            if (selectedVote != null)
            {
                return Ok(selectedVote);
            }
            else
            {
                return NoContent();
            }
        }


        // POST api/<VoteController>/5
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Vote vote)
        {
            Vote addVote = await _bl.AddVoteAsync(vote);
            return Created("api/[controller]", addVote);
        }


        // PUT api/<VoteController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Vote newVote)
        {
            Vote updatedVote = await _bl.UpdateVoteAsync(newVote);
            return Ok(updatedVote);
        }


        // DELETE api/<VoteController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Vote deleteVote = await _bl.GetVoteByIdAsync(id);
            await _bl.DeleteVoteAsync(id);
            return Ok(deleteVote);
        }
    }
}
