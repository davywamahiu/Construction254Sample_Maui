using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Construction_Ke.Model
{
    public class FinalReading
    {
        
        [Key]
        public int Code { get; set; }
        public double NetWeight { get; set; }
        public string Driver { get; set; } = null!;
        public long Phone { get; set; }
        public string Plate { get; set; } = null!;
        public string Material { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public string Time { get; set; }
        public int Ticket { get; set; }
        public double Tonage { get; set; }
        public double TareWeight { get; set; }
        public double GrossWeight { get; set; }
        public double TonageRate { get; set; }
        
        public double TotalAmount { get; set; }
        public double Balanc { get; set; }
        public double InBank { get; set; }

        public SysLogin SysLogins { get; set; } = null!;
    }
}
