using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TestExperis.Models;

namespace TestExperis.Data
{
    public class UsersApi
    {
        public static async Task<List<T>> GetList<T>(string url, string controller)
        {
            List<T> list = new List<T>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var client_response = await client.GetAsync(controller);
                    var result = await client_response.Content.ReadAsStringAsync();
                    if (!client_response.IsSuccessStatusCode)
                    {
                        return list;
                    }

                    list = JsonConvert.DeserializeObject<List<T>>(result);
                }
            }
            catch (Exception ex)
            {
                
            }
            return list;
        }
    }
}