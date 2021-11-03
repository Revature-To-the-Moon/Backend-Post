using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DL
{
    public class DBRepo : IRepo
    {
        private PostDB _context;

        public DBRepo(PostDB context)
        {
            _context = context;
        }


        //------------------------------------Methods For Getting List--------------------------------------

        public Task<List<Root>> GetRootListAsync()
        {
            throw new NotImplementedException();
        }

    }
}
