using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp.Models;
using WindowsFormsApp.Views;
using static WindowsFormsApp.Views.CarViewModel;

namespace WindowsFormsApp.Presenter
{
    public class CarsPresenter
    {
        private readonly ICarsView _CarView;
        private readonly ICarClient _CarClient;

        public CarsPresenter(ICarsView CarView, ICarClient CarClient)
        {
            _CarView = CarView;
            _CarClient = CarClient;
            LoadCars();
        }
        public async Task LoadCars()
        {
            _CarView.List = await _CarClient.List(1);
        }

        public async Task AddCar(IList<Car> List)
        {
            List.Add(new Car { LicencePlate = "", KmFare = 0, TimeFare = 0 });
            await SaveCars(List);
            await LoadCars();

        }

        public async Task SaveCars(IList<Car> List)
        {
            for (int i = 0; i < List.Count; i++)
            {
                await _CarClient.Save(List[i]);
            }
        }
    }
}

