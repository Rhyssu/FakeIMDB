using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using System.Reflection;

namespace FakeIMDB
{
    class Program
    {
        static async Task Main(string[] args)
        {
            APIManager apiManager = new APIManager();
            while(true)
            {
                Console.WriteLine("Siemaneczko, podaj swoje opcje wyszukiwania: ");
                Dictionary<string, string> data = new Dictionary<string, string>();
                while(true)
                {
                    string str = Console.ReadLine();
                    if (!string.IsNullOrEmpty(str))
                    {
                        data.Add(str.Split(" ")[0], str.Split(" ")[1]);
                    } 
                    else
                    {
                        break;
                    }
                }

                await apiManager.GetResponse(data);

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
