using System.ComponentModel.DataAnnotations;

namespace Construction_Ke.Model
{
    public class SecondWeight
    {
        public SecondWeight(double weight, int ticket, int firstWeightCode)
        {
            SWeight = weight;
            Ticket = ticket;
            FirstWeightCode = firstWeightCode;
        }

        public SecondWeight()
        {

        }

        public SecondWeight(int code, double weight, int ticket, int firstWeightCode)
        {
            Code = code;
            SWeight = weight;
            Ticket = ticket;
            FirstWeightCode = firstWeightCode;
        }
        [Key]
        public int Code { get; set; }
        public double SWeight { get; set; }
        public int Ticket { get; set; }
        public int FirstWeightCode { get; set; }
        public FirstWeight FirstWeights { get; set; } = null!;
    }
}
