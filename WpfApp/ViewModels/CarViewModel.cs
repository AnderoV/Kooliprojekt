using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class CarViewModel : NotifyPropertyChangedBase
    {
        public int Id { get; set; }
        private readonly IHttpClient _httpClient;
        public ObservableCollection<Car> cars { get; private set; }

        private readonly IWindowService _windowService;

        public CarViewModel(IHttpClient httpClient)
        {


            _httpClient = httpClient;
            cars = new ObservableCollection<Car>();


            Load();
        }


        public async Task Load()
        {
            var invoices = await _httpClient.List(1);
            foreach (var invoice in invoices)
            {
                cars.Add(invoice);
            }
        }
    }
}



