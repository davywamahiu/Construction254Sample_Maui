using System.ComponentModel.DataAnnotations;


namespace Construction_Ke.Model
{
    public class Drivers
    {

        public Drivers(int code, long driverId, string driverName, long phone, string plate)
        {
            Code = code;
            DriverId = driverId;
            DriverName = driverName;
            Phone = phone;
            Plate = plate;
        }

        public Drivers(string driverName, long phone, string plate)
        {
            DriverName = driverName;
            Phone = phone;
            Plate = plate;
        }

        public Drivers()
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
        [Key]
        public int Code { get; set; }
        public long DriverId { get; set; }
        public string DriverName { get; set; } = null!;
        public long Phone { get; set; }
        public string Plate { get; set; } = null!;

        public SysLogin SysLogins { get; set; } = null!;
    }
}
