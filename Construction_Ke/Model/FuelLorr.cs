using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction_Ke.Model
{
    public class FuelLorr
    {
        public int Code { get; set; }
        public int VehicleId { get; set; }
        public string NumberPlate { get; set; } = null!;
        public string Refuelier { get; set; } = null!;
        public string Driver { get; set; } = null!;
        public double Milage { get; set; }
        public double PrevMilage { get; set; }
        public string FuelType { get; set; } = null!;
        public decimal RefueliedLitters { get; set; }
        public DateTime RefuelDate { get; set; }
        public string RefuelTime { get; set; }
    }
}
