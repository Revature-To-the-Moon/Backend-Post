using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DL;

namespace BL
{
    public class PostBL : IBL
    {
        readonly private IRepo _repo;

        public PostBL(IRepo repo)
        {
            _repo = repo;
        }

        //------------------------------------Methods For Getting List--------------------------------------

        public async Task<List<Root>> GetRootListAsync()
        {
            return await _repo.GetRootListAsync();
        }

        public async Task<List<Root>> GetRootListByGroupIdAsync(int groupPostID){
            return await _repo.GetRootListByGroupIdAsync(groupPostID);
        }

        public async Task<List<Comment>> GetCommentListAsync()
        {
            return await _repo.GetCommentListAsync();
        }

        public async Task<List<Vote>> GetVoteListAsync()
        {
            return await _repo.GetVoteListAsync();
        }

        //------------------------------------Methods For Getting Data by Id--------------------------------

        public async Task<Root> GetRootByIdAsync(int id)
        {
            return await _repo.GetRootByIdAsync(id);
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _repo.GetCommentByIdAsync(id);
        }

        public async Task<Vote> GetVoteByIdAsync(int id)
        {
            return await _repo.GetVoteByIdAsync(id);
        }

        //------------------------------------Methods for Adding To DB--------------------------------------

        public async Task<Root> AddRootAsync(Root root)
        {
            return await _repo.AddRootAsync(root);
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            return await _repo.AddCommentAsync(comment);
        }

        public async Task<Vote> AddVoteAsync(Vote vote)
        {
            return await _repo.AddVoteAsync(vote);
        }

        //------------------------------------Methods for Updating DB--------------------------------------

        public async Task<Root> UpdateRootAsync(Root root)
        {
            return await _repo.UpdateRootAsync(root);
        }

        public async Task<Comment> UpdateCommentAsync(Comment comment)
        {
            return await _repo.UpdateCommentAsync(comment);
        }

        public async Task<Vote> UpdateVoteAsync(Vote vote)
        {
            return await _repo.UpdateVoteAsync(vote);
        }

        //------------------------------------Methods for Deleting From DB---------------------------------

        public async Task DeleteRootAsync(int id)
        {
            await _repo.DeleteRootAsync(id);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _repo.DeleteCommentAsync(id);
        }

        public async Task DeleteVoteAsync(int id)
        {
            await _repo.DeleteVoteAsync(id);
        }

    }
}
