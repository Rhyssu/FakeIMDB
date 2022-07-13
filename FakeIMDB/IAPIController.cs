using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeIMDB
{
    internal interface IAPIController
    {
        Task<string> GetResponse(Dictionary<string, string> APIParameters);

        Task<string> CheckResponse();

        Task<BySearchDOM> BySearch(string title);

        // SearchByTitle / ID / Search
        // movie type by enum
        // year as another nullable function argument
        // page -||- 
        // 
    }
}
