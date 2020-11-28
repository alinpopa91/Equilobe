using Equilobe.BLL.Contracts;
using Equilobe.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equilobe.BLL.Services
{
    public interface IParkingService
    {
        CarResponse EnterCar(string RegNo);
        CarResponse ExitCar(string RegNo);
        List<Car> GetAllCars();
    }
}
