using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpfApp;
using WpfApp.Models;
using WpfApp.ViewModels;
using Xunit;

namespace Kooliprojekt.UnitTests
{
    public class WPFAppTests
    {
        [Fact]
        public void Verify_SaveCarViewmodel_Save_Function()
        {
            Mock<IHttpClient> MockHttpCLient = new Mock<IHttpClient>();
            var CarsSaveViewmodel = new SaveCarViewmodel(MockHttpCLient.Object);


            MockHttpCLient.Setup(x => x.Save(It.IsAny<Car>())).Verifiable();

            CarsSaveViewmodel.Save(null);
            MockHttpCLient.VerifyAll();

        }

        [Fact]
        public async Task Verify_CarViewmodel_Load_Function()
        {
            Mock<IHttpClient> MockHttpCLient = new Mock<IHttpClient>();
            var CarViewmodel = new CarViewModel(MockHttpCLient.Object);

            var NewCar = new Car { LicencePlate = "3252", KmFare = 45 };
            var NewCar2 = new Car { LicencePlate = "3252", KmFare = 45 };
            IList<Car> Cars = new List<Car>() { NewCar, NewCar2 };

            MockHttpCLient.Setup(x => x.List(1)).Returns(Task.FromResult(Cars)).Verifiable();

            await CarViewmodel.Load();
            MockHttpCLient.VerifyAll();

        }
    }
}
