using Kooliprojekt.Data;
using Kooliprojekt.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kooliprojekt.UnitTests
{
public class BookingServiceTests : TestBase
    {
    private readonly ApplicationDbContext dbContext;
    private readonly BookingService service;

    public BookingServiceTests()
    {
        dbContext = GetDbContext();
        service = new BookingService(dbContext, Mapper);
    }

    [Fact]
    public async Task GetBookingListItem_returns_list()
    {
        //Arrange               
        dbContext.Bookings.Add(new Booking
        {
            Id = 1,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        //Act

        var result = await service.GetBookingListItem("test");

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
    }

    [Fact]
    public async Task GetBookingListItem_returns_no_list_on_wrong_username()
    {
        dbContext.Bookings.Add(new Booking
        {
            Id = 1,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetBookingListItem("test123");
        Assert.Equal(0, result.Count);
    }

    [Fact]
    public async Task GetBookingDetailModel_returns_detailmodel()
    {
        int id = 1;

        dbContext.Bookings.Add(new Booking
        {
            Id = id,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetBookingDetailModel(id, "test");

        Assert.NotNull(result.Result);
        Assert.Equal(id, result.Result.Id);
    }

    [Fact]
    public async Task GetBookingDetailModel_returns_no_detailmodel_on_wrong_username()
    {
        int id = 1;

        dbContext.Bookings.Add(new Booking
        {
            Id = id,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetBookingDetailModel(id, "test123");

        Assert.Null(result.Result);
    }

    [Fact]
    public async Task GetBookingDetailModel_returns_no_detailmodel_on_wrong_id()
    {
        int id = 1;

        dbContext.Bookings.Add(new Booking
        {
            Id = 2,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetBookingDetailModel(id, "test");

        Assert.Null(result.Result);
    }

    [Fact]
    public async Task GetBookingDeleteModel_returns_detailmodel()
    {
        int id = 1;

        dbContext.Bookings.Add(new Booking
        {
            Id = id,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetBookingDeleteModel(id, "test");

        Assert.NotNull(result.Result);
        Assert.Equal(id, result.Result.Id);
    }

    [Fact]
    public async Task GetBookingDeleteModel_returns_no_detailmodel_on_wrong_username()
    {
        int id = 1;

        dbContext.Bookings.Add(new Booking
        {
            Id = 1,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetBookingDeleteModel(id, "test123");

        Assert.Null(result.Result);
    }

    [Fact]
    public async Task GetBookingDeleteModel_returns_no_detailmodel_on_wrong_id()
    {
        int id = 1;

        dbContext.Bookings.Add(new Booking
        {
            Id = 2,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetBookingDeleteModel(id, "test");

        Assert.Null(result.Result);
    }

    [Fact]
    public async Task DeleteModel_returns_null_upon_delete()
    {
        int id = 1;

        dbContext.Bookings.Add(new Booking
        {
            Id = id,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.DeleteModel(id, "test");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetBookingEditModel_returns_detailmodel()
    {
        int id = 1;

        dbContext.Bookings.Add(new Booking
        {
            Id = id,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetBookingEditModel(id, "test");

        Assert.NotNull(result.Result);
        Assert.Equal(id, result.Result.Id);
    }

    [Fact]
    public async Task GetBookingEditModel_returns_no_detailmodel_on_wrong_username()
    {
        int id = 5;

        dbContext.Bookings.Add(new Booking
        {
            Id = 5,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetBookingEditModel(id, "test123");

        Assert.NotNull(result.Result);
    }

    [Fact]
    public async Task GetBookingEditModel_returns_no_detailmodel_on_wrong_id()
    {
        int id = 1;

        dbContext.Bookings.Add(new Booking
        {
            Id = 2,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetBookingEditModel(id, "test");

        Assert.Null(result.Result);
    }

    [Fact]
    public async Task EditBooking_returns_null_upon_editing()
    {
        int id = 1;

        var data = new Booking
        {
            Id = id,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        };

        dbContext.Bookings.Add(data);
        await dbContext.SaveChangesAsync();

        var result = await service.EditBooking(id, data);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetStopBookingModel_returns_detailmodel()
    {
        int id = 1;

        dbContext.Bookings.Add(new Booking
        {
            Id = id,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetStopBookingModel(id, "test");

        Assert.NotNull(result.Result);
        Assert.Equal(id, result.Result.Id);
    }

    [Fact]
    public async Task GetStopBookingModel_returns_no_detailmodel_on_wrong_username()
    {
        int id = 1;

        dbContext.Bookings.Add(new Booking
        {
            Id = 1,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetStopBookingModel(id, "test123");

        Assert.Null(result.Result);
    }

    [Fact]
    public async Task GetStopBookingModel_returns_no_detailmodel_on_wrong_id()
    {
        int id = 1;

        dbContext.Bookings.Add(new Booking
        {
            Id = 2,
            Car = new Car(),
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        });
        await dbContext.SaveChangesAsync();

        var result = await service.GetStopBookingModel(id, "test");

        Assert.Null(result.Result);
    }

    [Fact]
    public async Task StopBooking_returns_null()
    {
        int id = 1;

        var data = new Booking
        {
            Id = id,
            Car = new Car { Id = 5 },
            Km = 10,
            Pending = false,
            Price = 10,
            Start = new DateTime().Date,
            End = new DateTime().Date,
            User = new IdentityUser { UserName = "test" }
        };

        dbContext.Bookings.Add(data);
        await dbContext.SaveChangesAsync();

        var result = await service.StopBooking(id, data, data.Car.Id, "test");

        Assert.Null(result);
    }
}
}