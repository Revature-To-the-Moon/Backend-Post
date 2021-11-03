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
    }
}
