using Equilobe.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Equilobe.DAL.Model
{
    public class ParkingContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        //public ParkingContext()
        //{

        //}

        public ParkingContext(DbContextOptions options) : base(options)
        {

        }


        public List<Car> GetAllCars()
        {
            return Cars.Local.ToList<Car>();
        }
    }
}
