using Equilobe.BLL.Services;
using Equilobe.DAL.Model;
using Equilobe.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Equilobe.UnitTests
{
    public class Tests
    {
        ParkingService _parkingService;

        IQueryable<Car> data = new List<Car>
            {
                new Car { RegNo = "PH91ALP", StartDate = new System.DateTime(2020,7,23) },
                new Car { RegNo = "TL11AAA" },
                new Car { RegNo = "AG01WIN" },
            }.AsQueryable();


        [SetUp]
        public void Setup()
        {
            IDictionary<object, object> Properties = new Dictionary<object, object>();
            var context = new HostBuilderContext(Properties)
            {
                HostingEnvironment = mockEnvironment.Object
            };
            var mockBuilder = new Mock<IHostBuilder>();
            mockBuilder.Setup(x =>
                x.ConfigureAppConfiguration(It.IsAny<Action<HostBuilderContext, IConfigurationBuilder>>()))
              .Callback((Action<HostBuilderContext, IConfigurationBuilder> x) =>
                x.Invoke(context, mockConfigurationBuilder.Object));
            mockBuilder.Setup(x => x.ConfigureServices(It.IsAny<Action<IServiceCollection>>()))
              .Returns(mockBuilder.Object);


            var mockSet = new Mock<DbSet<Car>>();
            mockSet.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var mockContext = new Mock<ParkingContext>();
            mockContext.Setup(c => c.Cars).Returns(mockSet.Object);

            _parkingService = new ParkingService(mockContext.Object);
        }

        [Test]
        public void CheckAllCars()
        {
            var cars = _parkingService.GetAllCars();

            Assert.AreEqual(3, cars.Count);
            Assert.AreEqual("PH91ALP", cars[0].RegNo);
            Assert.AreEqual("TL11AAA", cars[1].RegNo);
            Assert.AreEqual("AG01WIN", cars[2].RegNo);
        }
    }
}