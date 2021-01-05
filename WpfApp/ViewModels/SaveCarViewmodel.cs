using Kooliprojekt.Data;
using System;
using System.Collections.Generic;
using System.Text;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class SaveCarViewmodel : NotifyPropertyChangedBase
    {
        //private readonly HttpClient _httpClient = new HttpClient();
        public RelayCommand<object> SaveCommand { get; private set; }
        private readonly IHttpClient _httpClient;

        public string LicencePlate { get; set; }
        public float KmFare { get; set; }
        public float TimeFare { get; set; }

        public SaveCarViewmodel(IHttpClient httpClient)
        {
            _httpClient = httpClient;
            SaveCommand = new RelayCommand<object>(Save);
        }

        public async void Save(object message)
        {
            var car = new Models.Car {LicencePlate = LicencePlate, KmFare = KmFare, TimeFare = TimeFare};
           await  _httpClient.Save(car);

           
        }
    }
}
