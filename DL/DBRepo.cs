using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class DBRepo : IRepo
    {
        private PostDB _context;

        public DBRepo(PostDB context)
        {
            _context = context;
        }
    }
}
