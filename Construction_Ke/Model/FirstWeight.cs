using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Construction_Ke.Model
{
    public class FirstWeight
    {
        public FirstWeight(int ticket, double weight, string driver, long phone, string plate, string material, double amount, DateTime dateTime, string time)
        {
            Ticket = ticket;
            Weight = weight;
            Driver = driver;
            Phone = phone;
            Plate = plate;
            Material = material;
            Amount = amount;
            DateTime = dateTime;
            Time = time;
        }

        public FirstWeight(int code, int ticket, double weight, string driver, long phone, string plate, string material, double amount, DateTime dateTime, string time)
        {
            Code = code;
            Ticket = ticket;
            Weight = weight;
            Driver = driver;
            Phone = phone;
            Plate = plate;
            Material = material;
            Amount = amount;
            DateTime = dateTime;
            Time = time;
        }

        public FirstWeight()
        {

        }
        [Key]
        public int Code { get; set; }
        public double Weight { get; set; }
        public string Driver { get; set; } = null!;
        public long Phone { get; set; }
        public string Plate { get; set; } = null!;
        public string Material { get; set; } = null!;
        [Column(TypeName = "decimal(6,2)")]
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
        public string Time { get; set; }
        public int Ticket { get; set; }
        public SysLogin SysLogins { get; set; } = null!;
    }
}
