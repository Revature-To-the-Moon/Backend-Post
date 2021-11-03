using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PostBl : IBL
    {
        private IRepo _repo;

        public PostBl(IRepo irepo)
        {
            _repo = irepo;
        }
    }
}
