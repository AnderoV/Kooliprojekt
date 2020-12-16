using Kooliprojekt.Models;
using Kooliprojekt.Models.BookingModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Data.Extensions
{
    public class SaveBookingToDatabaseCommand : ICommand<BookingEditModel>
    {
        public void Execute(BookingEditModel model)
        {
            Debug.WriteLine("SaveBookingCommand executed");
        }
    }
}
