using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DL
{
    public interface IRepo
    {
        //------------------------------------Methods For Getting List--------------------------------------

        Task<List<Root>> GetRootListAsync();

        //------------------------------------Methods For Getting Data by Id--------------------------------

        Task<Root> GetRootByIdAsync(int id);

    }
}
