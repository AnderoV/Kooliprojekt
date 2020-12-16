using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp.Models;
using WindowsFormsApp.Presenter;

namespace WindowsFormsApp.Views
{
   public class CarViewModel
    {
        public interface ICarsView
        {
            IList<Car> List { get; set; }
            CarsPresenter Presenter { get; set; }
        }
    }
}
