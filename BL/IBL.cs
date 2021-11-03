using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DL;

namespace BL
{
    public interface IBL
    {
        //------------------------------------Methods For Getting List--------------------------------------

        Task<List<Root>> GetRootListAsync();

        Task<List<Comment>> GetCommentListAsync();

        Task<List<Vote>> GetVoteListAsync();

        //------------------------------------Methods For Getting Data by Id--------------------------------

        Task<Root> GetRootByIdAsync(int id);

        Task<Comment> GetCommentByIdAsync(int id);

        Task<Vote> GetVoteByIdAsync(int id);

        //------------------------------------Methods for Adding To DB--------------------------------------

        Task<Root> AddRootAsync(Root root);

        Task<Comment> AddCommentAsync(Comment comment);

        Task<Vote> AddVoteAsync(Vote vote);

        //------------------------------------Methods for Updating DB--------------------------------------

        Task<Root> UpdateRootAsync(Root root);

        Task<Comment> UpdateCommentAsync(Comment comment);

        Task<Vote> UpdateVoteAsync(Vote vote);

        //------------------------------------Methods for Deleting From DB---------------------------------

        Task DeleteRootAsync(int id);

        Task DeleteCommentAsync(int id);

        Task DeleteVoteAsync(int id);

    }
}
