using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction_Ke.Model
{
    public class RecieveFuelModel
    {
        public int Code { get; set; }
        public int VehicleId { get; set; }
        public string NumberPlate { get; set; } = null!;
        public string Supplier { get; set; } = null!;
        public string Driver { get; set; } = null!;
        public long SupplierPhone { get; set; }
        public string FuelType { get; set; } = null!;
        public decimal SuppliedLitters { get; set; }
        public DateTime SupplyDate { get; set; }
        public string SupplyTime { get; set; } = null!;
    }
}
