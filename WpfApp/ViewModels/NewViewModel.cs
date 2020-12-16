using Kooliprojekt.Data;
using System;
using System.Collections.Generic;
using System.Text;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    class NewViewModel : NotifyPropertyChangedBase
    {
        private readonly CarClient _httpClient = new CarClient();
        public RelayCommand<object> SaveCommand { get; private set; }
       
        public string LicencePlate { get; set; }
        public float KmFare { get; set; }
        public float TimeFare { get; set; }

        public NewViewModel()
        {
            SaveCommand = new RelayCommand<object>(Save);
        }

        public async void Save(object message)
        {
            var car = new Models.Car {LicencePlate = LicencePlate, KmFare = KmFare, TimeFare = TimeFare};
           await  _httpClient.Save(car);

           
        }
    }
}
