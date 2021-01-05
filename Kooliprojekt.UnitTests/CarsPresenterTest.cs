using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsFormsApp;
using WindowsFormsApp.Models;
using WindowsFormsApp.Presenter;
using Xunit;
using static WindowsFormsApp.Views.CarViewModel;

namespace Kooliprojekt.UnitTests
{

    public class CarsPresenterTest
    {

        [Fact]
        public async Task Veify_CarClient_Load_Function()
        {
            Mock<ICarClient> mockCarClient = new Mock<ICarClient>();
            Mock<ICarsView> mockCarsView = new Mock<ICarsView>();

            var NewCar = new Car { Id = 1, LicencePlate = "3252", KmFare = 45 };
            var NewCar2 = new Car { Id = 2, LicencePlate = "3252", KmFare = 45 };
            IList<Car> Cars = new List<Car>() { NewCar, NewCar2 };

            var CarsPresenter = new CarsPresenter(mockCarsView.Object, mockCarClient.Object);
            mockCarClient.Setup(x => x.List(1)).Returns(Task.FromResult(Cars)).Verifiable();

            await CarsPresenter.LoadCars();
            mockCarClient.VerifyAll();
        }

        [Fact]
        public async Task Verify_CarClient_Save_Function()
        {
            Mock<ICarClient> mockCarClient = new Mock<ICarClient>();
            Mock<ICarsView> mockCarsView = new Mock<ICarsView>();

            var NewCar = new Car { Id = 1, LicencePlate = "3252", KmFare = 45 };
            var NewCar2 = new Car { Id = 2, LicencePlate = "3252", KmFare = 45 };
            IList<Car> Cars = new List<Car>() { NewCar, NewCar2 };

            var CarsPresenter = new CarsPresenter(mockCarsView.Object, mockCarClient.Object);
            mockCarClient.Setup(x => x.Save(NewCar)).Verifiable();

            await CarsPresenter.SaveCars(Cars);
            mockCarClient.VerifyAll();
        }

    }
}

