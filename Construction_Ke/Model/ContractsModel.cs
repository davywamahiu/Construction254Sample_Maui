using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction_Ke.Model
{
    public class ContractsModel
    {
        public int Code { get; set; }
        public string ProjectType { get; set; }
        public string ProjectName { get; set; }
        public string Client { get; set; }
        public double Cost { get; set; }
        public string Resident { get; set; }
        public string Road { get; set; }
        public string Engineer { get; set; }
        public string Surveyor { get; set; }
        public double Distance { get; set; }
        public DateTime SignedOn { get; set; }
        public DateTime StartD { get; set; }
        public DateTime ExpectedD { get; set; }
        public DateTime RevisedD { get; set; }
    }
}
