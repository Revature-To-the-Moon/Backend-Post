using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DL;

namespace BL
{
    public class PostBl : IBL
    {
        private IRepo _repo;

        public PostBl(IRepo repo)
        {
            _repo = repo;
        }

        //------------------------------------Methods For Getting List--------------------------------------

        public async Task<List<Root>> GetRootListAsync()
        {
            return await _repo.GetRootListAsync();
        }

        //------------------------------------Methods For Getting Data by Id--------------------------------

        public async Task<Root> GetRootByIdAsync(int id)
        {
            return await _repo.GetRootByIdAsync(id);
        }

        //------------------------------------Methods for Adding To DB--------------------------------------

        public async Task<Root> AddRootAsync(Root root)
        {
            return await _repo.AddRootAsync(root);
        }

        //------------------------------------Methods for Updating DB--------------------------------------
        
        public async Task<Root> UpdateRootAsync(Root root)
        {
            return await _repo.UpdateRootAsync(root);
        }

        //------------------------------------Methods for Deleting From DB---------------------------------

        public async Task DeleteRootAsync(int id)
        {
            await _repo.DeleteRootAsync(id);
        }

    }
}
