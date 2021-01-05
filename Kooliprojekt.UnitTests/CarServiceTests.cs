using Kooliprojekt.Data;
using Kooliprojekt.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kooliprojekt.UnitTests
{
    public class CarServiceTests : TestBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly CarService service;

        public CarServiceTests()
        {
            dbContext = GetDbContext();
            service = new CarService(dbContext, Mapper);
        }


        [Fact]
        public async Task GetList_should_return_Cars_list()
        {
            dbContext.Cars.Add(new Car
            {
                Id = 1,
                CarModel = new CarModel(),
                LicencePlate = "plate1",
                KmFare = 1,
                TimeFare = 1,
                Pictures = new List<Image>(),
                Bookings = new List<Booking>(),
                Operations = new List<Operation>()
            });
            dbContext.Cars.Add(new Car
            {
                Id = 2,
                CarModel = new CarModel(),
                LicencePlate = "plate2",
                KmFare = 1,
                TimeFare = 1,
                Pictures = new List<Image>(),
                Bookings = new List<Booking>(),
                Operations = new List<Operation>()
            });
            await dbContext.SaveChangesAsync();

            var result = await service.GetCarListItems();

            Assert.NotNull(result);
            Assert.Equal(2, result.Result.Count);
        }

        [Fact]
        public async Task GetList_should_return_null_on_empty_list()
        {

            var result = await service.GetCarListItems();

            Assert.Equal(0, result.Result.Count);
        }

        [Fact]
        public async Task GetCarDetails_should_return_Car_details()
        {
            dbContext.Cars.Add(new Car
            {
                Id = 1,
                CarModel = new CarModel(),
                LicencePlate = "plate1",
                KmFare = 1,
                TimeFare = 1,
                Pictures = new List<Image>(),
            });
            await dbContext.SaveChangesAsync();

            var result = await service.GetCarDetails(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Result.Id);
        }

        [Fact]
        public async Task GetCarDetails_should_return_Null_on_invalid_id()
        {
            dbContext.Cars.Add(new Car
            {
                Id = 1,
                CarModel = new CarModel(),
                LicencePlate = "plate1",
                KmFare = 1,
                TimeFare = 1,
                Pictures = new List<Image>(),
            });
            await dbContext.SaveChangesAsync();

            var result = await service.GetCarDetails(2);

            Assert.Null(result.Result);
        }

        [Fact]
        public async Task GetCarDelete_should_return_Car_detail_model()
        {
            dbContext.Cars.Add(new Car
            {
                Id = 1,
                CarModel = new CarModel(),
                LicencePlate = "plate1",
                KmFare = 1,
                TimeFare = 1,
            });
            await dbContext.SaveChangesAsync();

            var result = await service.GetCarDeleteModel(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Result.Id);
        }

        [Fact]
        public async Task GetCarDelete_should_return_Null_on_invalid_id()
        {
            dbContext.Cars.Add(new Car
            {
                Id = 1,
                CarModel = new CarModel(),
                LicencePlate = "plate1",
                KmFare = 1,
                TimeFare = 1,
            });
            await dbContext.SaveChangesAsync();

            var result = await service.GetCarDeleteModel(2);

            Assert.Null(result.Result);
        }

        [Fact]
        public async Task DeleteCar_should_return_null_when_deleting_a_car()
        {
            dbContext.Cars.Add(new Car
            {
                Id = 1,
                CarModel = new CarModel(),
                LicencePlate = "plate1",
                KmFare = 1,
                TimeFare = 1,
            });
            await dbContext.SaveChangesAsync();

            var result = await service.DeleteCar(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCarEditModel_should_return_Car_edit_model()
        {

            dbContext.Cars.Add(new Car
            {
                Id = 1,
                CarModel = null,
                LicencePlate = "plate1",
                KmFare = 1,
                TimeFare = 1,
            });


            await dbContext.SaveChangesAsync();

            var result = await service.GetCarEditModel(1);


            Assert.NotNull(result);
            Assert.Equal(1, result.Result.Id);

        }


        [Fact]
        public async Task EditCar_should_return_null_on_edit()
        {
            var id = 1;
            int modelId = 1;
            var car = new Car
            {
                Id = 1,
                CarModel = new CarModel(),
                LicencePlate = "plate1",
                KmFare = 1,
                TimeFare = 1,
            };

            dbContext.Cars.Add(car);
            await dbContext.SaveChangesAsync();

            var result = await service.EditCar(id, car, modelId);

            Assert.Null(result);

        }

        [Fact]
        public async Task GetCarCreateModel_should_return_Car_create_model()
        {

            dbContext.Cars.Add(new Car
            {
                Id = 1,
                CarModel = null,
                LicencePlate = "plate1",
                KmFare = 1,
                TimeFare = 1,
            });


            await dbContext.SaveChangesAsync();

            var result = await service.GetCarCreateModel();


            Assert.NotNull(result);
            Assert.Equal(0, result.Result.Id);

        }

        [Fact]
        public async Task CreateCar_should_return_null_on_edit()
        {
            int modelId = 1;
            var car = new Car
            {
                Id = 1,
                CarModel = new CarModel(),
                LicencePlate = "plate1",
                KmFare = 1,
                TimeFare = 1,
            };

            dbContext.Cars.Add(car);
            await dbContext.SaveChangesAsync();

            var result = await service.CreateCar(car, modelId);

            Assert.Null(result);

        }

    }
}
