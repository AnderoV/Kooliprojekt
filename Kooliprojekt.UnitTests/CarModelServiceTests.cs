using Kooliprojekt.Data;
using Kooliprojekt.ServiceClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kooliprojekt.UnitTests
{
    public class CarModelServiceTests : TestBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly CarModelService service;

        public CarModelServiceTests()
        {
            dbContext = GetDbContext();
            service = new CarModelService(dbContext, Mapper);
        }

        [Fact]
        public async Task GetCarModelListItems_returns_list()
        {
            //Arrange         
            dbContext.CarModels.Add(new CarModel
            {
                Id = 1,
                Mark = "a",
                Model = "b",
            });

            dbContext.CarModels.Add(new CarModel
            {
                Id = 2,
                Mark = "c",
                Model = "d",
            });
            await dbContext.SaveChangesAsync();

            //Act
            var result = await service.GetCarModelListItems();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Result.Count);
        }

        [Fact]
        public async Task GetCarModelDetails_returns_model_details()
        {
            var id = 1;
            dbContext.CarModels.Add(new CarModel
            {
                Id = id,
                Mark = "a",
                Model = "b",
            });
            await dbContext.SaveChangesAsync();

            var result = await service.GetCarModelDetails(id);

            Assert.NotNull(result);
            Assert.Equal(1, result.Result.Id);
        }

        [Fact]
        public async Task GetCarModelDetails_returns_no_model_details_with_invalid_id()
        {
            var id = 2;
            dbContext.CarModels.Add(new CarModel
            {
                Id = 1,
                Mark = "a",
                Model = "b",
            });
            await dbContext.SaveChangesAsync();

            var result = await service.GetCarModelDetails(id);

            Assert.Null(result.Result);
        }

        [Fact]
        public async Task CreateCarModel_returns_null()
        {
            var data = new CarModel
            {
                Id = 1,
                Mark = "a",
                Model = "b",
            };

            var result = await service.CreateCarModel(data);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCarEditModel_returns_editmodel()
        {
            var id = 1;
            dbContext.CarModels.Add(new CarModel
            {
                Id = id,
                Mark = "a",
                Model = "b",
            });
            await dbContext.SaveChangesAsync();

            var result = await service.GetCarEditModel(id);

            Assert.NotNull(result);
            Assert.Equal(1, result.Result.Id);
        }

        [Fact]
        public async Task GetCarEditModel_returns_no_editmodel_with_invalid_id()
        {
            var id = 2;
            dbContext.CarModels.Add(new CarModel
            {
                Id = 1,
                Mark = "a",
                Model = "b",
            });
            await dbContext.SaveChangesAsync();

            var result = await service.GetCarEditModel(id);

            Assert.Null(result.Result);
        }

        [Fact]
        public async Task EditCarModel_returns_null()
        {
            var data = new CarModel
            {
                Id = 1,
                Mark = "a",
                Model = "b",
            };
            dbContext.CarModels.Add(data);
            await dbContext.SaveChangesAsync();

            var result = await service.EditCarModel(data);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCarDeleteModel_returns_deletemodel()
        {
            var id = 1;
            dbContext.CarModels.Add(new CarModel
            {
                Id = id,
                Mark = "a",
                Model = "b",
            });
            await dbContext.SaveChangesAsync();

            var result = await service.GetCarDeleteModel(id);

            Assert.NotNull(result);
            Assert.Equal(1, result.Result.Id);
        }

        [Fact]
        public async Task GetCarDeleteModel_returns_no_deletemodel_with_invalid_id()
        {
            var id = 2;
            dbContext.CarModels.Add(new CarModel
            {
                Id = 1,
                Mark = "a",
                Model = "b",
            });
            await dbContext.SaveChangesAsync();

            var result = await service.GetCarDeleteModel(id);

            Assert.Null(result.Result);
        }

        [Fact]
        public async Task DeleteCarModel_returns_null()
        {
            var id = 1;
            var data = new CarModel
            {
                Id = id,
                Mark = "a",
                Model = "b",
            };
            dbContext.CarModels.Add(data);
            await dbContext.SaveChangesAsync();

            var result = await service.DeleteCarModel(id);

            Assert.Null(result);
        }
    }
}
