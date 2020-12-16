using Kooliprojekt.Models;
using Kooliprojekt.Models.BookingModels;
using System.Diagnostics;

namespace Kooliprojekt.Data.Extensions
{
    public class Command1 : ICommand<BookingEditModel>
    {
        public void Execute(BookingEditModel model)
        {
            Debug.WriteLine("Command1 executed");
        }
    }
}