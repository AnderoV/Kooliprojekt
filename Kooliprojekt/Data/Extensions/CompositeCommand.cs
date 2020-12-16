using Kooliprojekt.Models;
using Kooliprojekt.Models.BookingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Data.Extensions
{
    public class CompositeCommand : CompositeCommandBase<BookingEditModel>
    {
        public CompositeCommand(ITenantProvider tenantProvider,
            SaveBookingToDatabaseCommand saveBookingToDatabase,
            Command1 command1,
            Command2 command2)
        {
            var tenant = tenantProvider.GetTenant();

            if (tenant.NewCarNotification)
            {
                Children.Add(command1);
            }

            if (tenant.UseThumbnails)
            {
                Children.Add(command2);
            }
            if (tenant.test)
            {
                Children.Add(saveBookingToDatabase);
            }
        }
    }
}
