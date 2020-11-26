using AppCounty.Common.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppCounty.Common.Services
{
    public class ApiService : IApiService
    {
        public async Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),

                };
                string url = $"{servicePrefix}{controller}";

                HttpResponseMessage response = await client.GetAsync(url);

                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response { IsSuccess = false, Message = result, };
                }

                T value = JsonConvert.DeserializeObject<T>(result);

                return new Response { IsSuccess = true, Result = value };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = ex.Message };
            }
        }
    }

}
