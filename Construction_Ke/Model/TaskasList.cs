using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction_Ke.Model
{
    public class TaskasList
    {
        public int Code { get; set; }
        public string Activitiz { get; set; } = null;
        public string TasDependsOn { get; set; }
        public string TasCompCreteria { get; set; }
        public DateTime TasStartDate { get; set; }
        public DateTime TasEndDate { get; set; }
    }
}
