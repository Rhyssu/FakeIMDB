using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeIMDB
{
    internal interface IAPIController
    {
        static private HttpClient? Client
        {
            get;
        }

        static private string? APIKey
        {
            get;
        }

        static private string? BaseUriAddress
        {
            get;
        }

        Task<String> GetResponse(Dictionary<string, string> dict);

        Task<String> CheckResponse();

    }
}
