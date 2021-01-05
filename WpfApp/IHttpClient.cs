using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp
{
    public interface IHttpClient
    {
        Task<IList<Car>> List(int page);
        Task Save(Car car);
    }
}
