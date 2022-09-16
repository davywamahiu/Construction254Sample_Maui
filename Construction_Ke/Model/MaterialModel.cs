using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction_Ke.Model
{
    public class MaterialModel
    {
        public int Code { get; set; }
        public string Material { get; set; } = null!;
        public double Cost { get; set; }
        public string MatSerial { get; set; } = null!;
        public double Amount { get; set; }
        public double Rati { get; set; }
        public double Quantity { get; set; }
        public string Uniti { get; set; }
        public string Projectz { get; set; }
    }
}
