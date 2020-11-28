using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using System.Text;

namespace Equilobe.DAL.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public DateTime StartDate { get; set; }

        private double totalMinutes;
        public double TotalMinutes
        {
            get
            {
                return (DateTime.Now - StartDate).TotalMinutes;
            }
            set
            {
                totalMinutes = value;
            }
        }

    }
}
