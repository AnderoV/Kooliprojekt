using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp
{
    public class CarClient
    {
        private const string BaseUrl = "http://bigcorp:5000/api/";
        public async Task<IList<Car>> List(int page)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var json = await client.GetStringAsync(BaseUrl);
                    return JsonConvert.DeserializeObject<List<Car>>(json);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task Save(Car car)
        {
            HttpClient client = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
            await client.PostAsync("http://bigcorp:5000/api/", stringContent);

        }

    }
}
