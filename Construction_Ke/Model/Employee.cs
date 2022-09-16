using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction_Ke.Model;
public class Employee
{
    public int Code { get;set; }
    public string Surname { get; set; }
    public string MiddleName { get; set; }
    public string FirstName { get; set; }
    public long Phone { get; set; }
    public string KRAPin { get; set; }
    public string EPin { get;set; }
    public long NatID { get; set; }
    public long Salary { get; set; }
    public string Role { get; set; }
    public string Profession { get; set; }
    public string Supervisor { get; set; }
    public string Country { get; set; }
    public string County { get; set; }
    public string SubCounty { get; set; }
    public string Village { get; set; }
    public string Wages { get; set; }
    public string OvertimeRates { get; set; }
    public string Departmnet { get; set; }
    public string EStatus { get; set; }
    public int DriverAsigned { get; set; }
    public int DriverNonAssigneed { get; set; }
    public int OperatorAssigneed { get; set; }
    public int OperatorNonAssigneed { get; set; }
    public int Totals { get; set; } 
    public int Drivers { get; set; }
    public int Operatas { get; set; }
    public int Engineers { get; set; }
}
