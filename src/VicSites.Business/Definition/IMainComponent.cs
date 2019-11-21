using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VicSites.Business.Definition
{
    public interface IMainComponent
    {
        Task<int> GetNumberOfVisits();
    }
}
