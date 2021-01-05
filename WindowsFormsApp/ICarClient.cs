using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp.Models;

namespace WindowsFormsApp
{
    public interface ICarClient
    {
        Task<IList<Car>> List(int page);
        Task Save(Car car);
    }
}