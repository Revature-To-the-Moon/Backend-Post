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

        //------------------------------------Methods For Getting Data by Id--------------------------------

        Task<Root> GetRootByIdAsync(int id);

        //------------------------------------Methods for Adding To DB--------------------------------------

        Task<Root> AddRootAsync(Root root);

        //------------------------------------Methods for Updating DB--------------------------------------

        Task<Root> UpdateRootAsync(Root root);

    }
}
