using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.Json;


namespace FakeIMDB
{
    internal class APIManager : IAPIController
    {
        static private readonly HttpClient client = new HttpClient();
        static private readonly string? APIKey = ConfigurationManager.AppSettings["APIKey"];
        static private readonly string BaseUriAddress = "http://www.omdbapi.com";

        public APIManager()
        {
            client.BaseAddress = new Uri(BaseUriAddress);
        }

        public async Task<string> CheckResponse()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Format("/?i=tt3896198&apikey={0}", APIKey));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                return e.Message;
            }
        }

        public async Task<string> GetResponse(Dictionary<string, string> dict)
        {
            string addedString = String.Empty;

            if (dict.ContainsKey("i") || dict.ContainsKey("t"))
            {
                foreach (KeyValuePair<string, string> item in dict) 
                {
                    string[] possibleOptions = ConfigurationManager.AppSettings["ByTitle"].Split(", ");

                    if (possibleOptions.Contains(item.Key))
                    {
                        addedString += String.Format("&{0}={1}", item.Key, item.Value);
                    } 
                    else
                    {
                        return addedString;
                    }
                }
            } 
            else if (dict.ContainsKey("s"))
            {
                foreach (KeyValuePair<string, string> item in dict)
                {
                    string[] possibleOptions = ConfigurationManager.AppSettings["BySearch"].Split(", ");

                    if (possibleOptions.Contains(item.Key))
                    {
                        addedString += String.Format("&{0}={1}", item.Key, item.Value);
                    }
                    else
                    {
                        return addedString;
                    }
                }
            }
            else
            {
                return addedString;
            }

            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Format("/?{0}&apikey={1}", addedString, APIKey));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                return e.Message;
            }
        }
    }
}
