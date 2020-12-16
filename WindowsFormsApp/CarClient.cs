using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp.Models;
using WindowsFormsApp.Presenter;
using static WindowsFormsApp.Views.CarViewModel;

namespace WindowsFormsApp
{
    public class CarClient 
    {
        private const string BaseUrl = "http://bigcorp:5000/api/";

        //public CarsPresenter Presenter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //IList<Car> ICarsView.List { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<IList<Car>> List(int page)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var json = await client.GetStringAsync(BaseUrl);
                    return JsonConvert.DeserializeObject<List<Car>>(json);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task Save(Car car)
        {

            HttpClient client = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
            string url = "http://bigcorp:5000/api/" + car.Id.ToString();
            await client.PutAsync(url, stringContent);

        }

    }
}
