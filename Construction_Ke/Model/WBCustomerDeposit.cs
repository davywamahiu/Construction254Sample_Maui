using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction_Ke.Model
{
    public class WBCustomerDeposit
    {
        public WBCustomerDeposit()
        {
        }

        public int Code { get; set; }
        public string Driver { get; set; }
        public long Phone { get; set; }
        public string Plate { get; set; }
        public string AccountNo { get; set; }
        public string MpesaUid { get; set; }
        public double Deposit { get; set; }
        public double SpentAmount { get; set; }
        public double Balance { get; set; }
        public string PaidOn { get; set; }
        public string Material { get; set; }
        public double Tonage { get; set; }
        public int Customer { get; set; }
        public double Payment { get; set; }
        public int NewCustomers { get; set; }
        public double InBank { get; set; }
    }
}
