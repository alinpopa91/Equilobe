using Equilobe.BLL.Services;
using Equilobe.DAL.Model;
using Equilobe.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Equilobe.NUnit
{
    public class Tests
    {
        private ParkingService _parkingService;

        IQueryable<Car> data = new List<Car>
        {
            new Car { RegNo = "B-11-BB" },
            new Car { RegNo = "B-11-ZZ" },
            new Car { RegNo = "B-11-AA" },
        }.AsQueryable();

        [SetUp]
        public void Setup()
        {
            var mockSet = new Mock<DbSet<Car>>();
            mockSet.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            //var mockContext = new Mock<ParkingContext>();
            //mockContext.Setup(c => c.Cars).Returns(mockSet.Object);

            //_parkingService = new ParkingService(mockContext.Object);
        }

        [Test]
        public void Test1()
        {

            var cars = _parkingService.GetAllCars();

            Assert.AreEqual(3, cars.Count);
            Assert.AreEqual("B-11-AA", cars[0].RegNo);
            Assert.AreEqual("B-11-BB", cars[1].RegNo);
            Assert.AreEqual("B-11-ZZ", cars[2].RegNo);
            Assert.Pass();
        }

        [Test]
        public void GetAllTest()
        {
            var options = new DbContextOptionsBuilder<ParkingContext>()
                .UseInMemoryDatabase(databaseName: "ParkingContext")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new ParkingContext(options))
            {
                context.Cars.Add(new Car { RegNo = "B-11-BB" });
                context.Cars.Add(new Car { RegNo = "B-11-AA" });
                context.Cars.Add(new Car { RegNo = "B-11-CC" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new ParkingContext(options))
            {
                Assert.AreEqual(3, context.Cars.Count());



            }
        }
    }
}