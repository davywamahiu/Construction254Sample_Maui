using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction_Ke.Model
{
    public class ActAndTask
    {
        public int Code { get; set; } 
        public string ProjectName { get; set; }
        public string MyACtivitiz { get; set; } = null;
        public string ActName { get; set; } = null;
        public string ActDependsOn { get; set; }
        public string ActCompCreteria { get; set; }
        public DateTime ActStartDate { get; set; }
        public DateTime ActEndDate { get; set; }
    }
}
