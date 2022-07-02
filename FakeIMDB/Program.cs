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
        static void Main(string[] args)
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

                Task<String> task = Task.Run (() => apiManager.GetResponse(data));
                Movie? movie = JsonSerializer.Deserialize<Movie>(task.Result);
                PropertyInfo[] properties = typeof(Movie).GetProperties();
                foreach(PropertyInfo property in properties)
                {
                    Console.WriteLine(" {0} {1} ", property.Name, property.GetValue(movie));
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
