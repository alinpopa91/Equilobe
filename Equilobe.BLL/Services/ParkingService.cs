using Equilobe.BLL.Contracts;
using Equilobe.DAL.Model;
using Equilobe.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Equilobe.BLL.Services
{
    public class ParkingService : IParkingService
    {
        private readonly ParkingContext _parkingContext;

        public ParkingService(ParkingContext parkingContext)
        {
            _parkingContext = parkingContext;
        }

        public CarResponse EnterCar(string RegNo)
        {
            CarResponse toReturn = new CarResponse
            {
                Success = true,
                Message = "The car has been inserted"
            };

            Car carItem = new Car
            {
                RegNo = RegNo,
                StartDate = DateTime.Now
            };

            if (_parkingContext.Cars.Count() >= 10)
            {
                toReturn.Success = false;
                toReturn.Message = "The parking is full";
            }

            if (_parkingContext.Cars.Count(a => a.RegNo == RegNo) > 0)
            {
                toReturn.Success = false;
                toReturn.Message = "This car is already parked here";
            }

            if (toReturn.Success)
            {
                carItem.StartDate = DateTime.Now;
                _parkingContext.Add(carItem);
                _parkingContext.SaveChanges();
            }
            return toReturn;
        }

        public CarResponse ExitCar(string RegNo)
        {
            CarResponse toReturn = new CarResponse
            {
                Success = true,
                Message = "The car has been inserted"
            };

            if (!_parkingContext.Cars.Any(a => a.RegNo == RegNo))
            {
                toReturn.Success = false;
                toReturn.Message = "Car not found in our Parking";
                return toReturn;
            }

            Car currentCar = _parkingContext.Cars.FirstOrDefault(a => a.RegNo == RegNo);
            int amountToPay = 10;
            if (currentCar.TotalMinutes <= 60)
                amountToPay = 10;
            else
                amountToPay = ((int)currentCar.TotalMinutes / 60) * 5 + 5;

            toReturn.Message = $"The car {currentCar.RegNo} have to pay {amountToPay} RON";

            _parkingContext.Cars.Remove(currentCar);
            _parkingContext.SaveChanges();

            return toReturn;
        }

        public List<Car> GetAllCars()
        {
            return _parkingContext.Cars.ToList();
        }
    }
}
