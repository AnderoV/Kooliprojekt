using Kooliprojekt.Models;
using Kooliprojekt.Models.BookingModels;
using System.Diagnostics;

namespace Kooliprojekt.Data.Extensions
{
    public class Command2 : ICommand<BookingEditModel>
    {
        public void Execute(BookingEditModel model)
        {
            Debug.WriteLine("Command2 executed");
        }
    }
}