using Construction_Ke.Views.HRView;
using System.Collections.ObjectModel;
using Construction_Ke.Model;
using System.Data;
using MySql.Data.MySqlClient;
namespace Construction_Ke.ViewModel.EmployeeViewModel
{
    public class NewEmpViewModel : BaseViewModel
    {
        private int code;
        private string surname;
        private string middleName;
        private string firstName;
        private long phone;
        private string kRAPin;
        private string ePin;
        private long natID;
        private long salary;
        private string role;
        private string profession;
        private string supervisor;
        private string country;
        private string county;
        private string subCounty;
        private string village;
        private string wages;
        private string overtimeRates;
        private string departmnet;
        private string eStatus;
        private string fullName;
        private string projectz;
        private string statuss;
        private string plate;
        private string vehicle;
        private string insurance;
        private int driverAsigned;
        private int driverNonAssigneed;
        private int operatorAssigneed;
        private int operatorNonAssigneed;
        private string assigneed;
        private int totals;
        private int drivers;
        private int operatas;
        private int engineers;
        private int totals1;
        private int drivers1;
        private int operatas1;
        private int engineers1;
        public Command LoadEmployee { get; } 
        public Command LoadCasualEmp { get; }
        public Command SaveEmployee { get; }
        public Command SaveCasualEmp { get; }
        public Command LoadSavedEmployee { get; }
        public Command LoadSavedCasualEmp { get; }
        public Command LoadFleetManager { get; }
        public Command LoadAddFleetAsignment { get; }
        public ObservableCollection<Employee> Employees { get; }
        public ObservableCollection<Employee> Casuals { get; }
        public ObservableCollection<FleetAssigned> Fleet { get; }
        public Command OnSaveAssignment { get; }
        public Command LoadOnSaveAssignment { get; }
        public int Code { get => code; set => code = value; }
        public string Surname { get => surname; set => surname = value; }
        public string MiddleName { get => middleName; set => middleName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public long Phone { get => phone; set => phone = value; }
        public string KRAPin { get => kRAPin; set => kRAPin = value; }
        public string EPin { get => ePin; set => ePin = value; }
        public long NatID { get => natID; set => natID = value; }
        public long Salary { get => salary; set => salary = value; }
        public string Role { get => role; set => role = value; }
        public string Profession { get => profession; set => profession = value; }
        public string Supervisor { get => supervisor; set => supervisor = value; }
        public string Country { get => country; set => country = value; }
        public string County { get => county; set => county = value; }
        public string SubCounty { get => subCounty; set => subCounty = value; }
        public string Village { get => village; set => village = value; }
        public string Wages { get => wages; set => wages = value; }
        public string OvertimeRates { get => overtimeRates; set => overtimeRates = value; }
        public string Departmnet { get => departmnet; set => departmnet = value; }
        public string EStatus { get => eStatus; set => eStatus = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string Projectz { get => projectz; set => projectz = value; }
        public string Statuss { get => statuss; set => statuss = value; }
        public string Plate { get => plate; set => plate = value; }
        public string Vehicle { get => vehicle; set => vehicle = value; }
        public string Insurance { get => insurance; set => insurance = value; }
        public int DriverAsigned { get => driverAsigned; set => driverAsigned = value; }
        public int DriverNonAssigneed { get => driverNonAssigneed; set => driverNonAssigneed = value; }
        public int OperatorAssigneed { get => operatorAssigneed; set => operatorAssigneed = value; }
        public int OperatorNonAssigneed { get => operatorNonAssigneed; set => operatorNonAssigneed = value; }
        public string Assigneed { get => assigneed; set => assigneed = value; }
        public int Totals { get => totals; set => totals = value; }
        public int Drivers { get => drivers; set => drivers = value; }
        public int Operatas { get => operatas; set => operatas = value; }
        public int Engineers { get => engineers; set => engineers = value; }
        public int Totals1 { get => totals1; set => totals1 = value; }
        public int Drivers1 { get => drivers1; set => drivers1 = value; }
        public int Operatas1 { get => operatas1; set => operatas1 = value; }
        public int Engineers1 { get => engineers1; set => engineers1 = value; }

        public NewEmpViewModel()
        {
            Employees = new();
            Casuals = new();
            Fleet = new();
            LoadSavedCasualEmp = new Command(async () => await GetLoadSavedCasualEmp());
            LoadSavedEmployee = new Command(async () => await GetLoadSavedEmployee());
            LoadOnSaveAssignment = new Command(async () => await OnLoadOnSaveAssignment());
            LoadEmployee = new(GetLoadEmployee);
            LoadCasualEmp = new(GetLoadCasualEmp);
            SaveCasualEmp = new(GetSaveCasualEmp);
            SaveEmployee = new(GetSaveEmployee);
            LoadFleetManager = new(GetLoadFleetManager);
            LoadAddFleetAsignment = new(GetLoadAddFleetAsignment);
            OnSaveAssignment = new(GetOnSaveAssignment);
            UpDateMyDrivers();
            UpDateMyEmployees();
            UpdateMyCasuals();
        }

        private async void UpdateMyCasuals()
        {
            try
            {
                Casuals.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Casuals order by id ASC LIMIT 100";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt1.Clear();
                adapter.Fill(dt1);
                //survey
                Drivers1 = 0;
                //earth works
                Operatas1 = 0;
                //Production
                Engineers1 = 0;
                //all employees
                Totals1 = dt1.Rows.Count;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows[i]["Department"].ToString().Equals("Survey"))
                        Drivers1++;
                    else if (dt1.Rows[i]["Department"].ToString().Contains("Works"))
                        Operatas1++;
                    else if (dt1.Rows[i]["Department"].ToString().Equals("Production"))
                        Engineers1++;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }

        private async void UpDateMyEmployees()
        {
            try
            {
                Employees.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Employee order by id ASC LIMIT 100";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                Drivers = 0;
                Operatas = 0;
                Engineers = 0;
                Totals = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Role"].ToString().Equals("Drivers"))
                        Drivers++;
                    else if (dt.Rows[i]["Role"].ToString().Equals("Operators"))
                        Operatas++;
                    else if (dt.Rows[i]["Role"].ToString().Equals("Engineer") &&
                        dt.Rows[i]["Role"].ToString().Equals("Surveyor"))
                        Engineers++;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }

        private async void UpDateMyDrivers()
        {
            try
            {
                Fleet.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from FleetAssigned left join Vehicles on FleetAssigned.Plate=Vehicles.Plate order by FleetAssigned.id ASC LIMIT 100";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt2.Clear();
                adapter.Fill(dt2);
                DriverAsigned = 0;
                DriverNonAssigneed = 0;
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["Statuss"].ToString().Equals("Active"))
                        DriverAsigned++;
                    else if (dt2.Rows[i]["Statuss"].ToString().Equals("Inactive"))
                        DriverNonAssigneed++;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            //SelectedItem = null;
        }
        List<FleetAssigned> Fleets;
        async Task OnLoadOnSaveAssignment()
        {
            IsBusy = true;
            try
            {
                Fleet.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from FleetAssigned left join Vehicles on FleetAssigned.Plate=Vehicles.Plate order by FleetAssigned.id ASC LIMIT 100";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt2.Clear();
                adapter.Fill(dt2);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    Fleets = new()
                    {
                        new FleetAssigned
                        {
                            Code= Convert.ToInt32(dt2.Rows[i]["id"].ToString()),
                            Projectz = dt2.Rows[i]["Projectz"].ToString(),
                            Plate = dt2.Rows[i]["Plate"].ToString(),
                            Assigneed= dt2.Rows[i]["Assigneed"].ToString(),
                            FullName = dt2.Rows[i]["FullName"].ToString(),
                            EPin = dt2.Rows[i]["EPin"].ToString(),
                            Insurance = dt2.Rows[i]["UsedStatus"].ToString(),
                            Statuss = dt2.Rows[i]["Statuss"].ToString(),
                            VehiclType = dt2.Rows[i]["VehiclType"].ToString(),
                            NatID = Convert.ToInt64(dt2.Rows[i]["NatID"].ToString()),
                            Condition = dt2.Rows[i]["VCondition"].ToString(),

                        }
                    };
                    if (i < dt2.Rows.Count)
                        foreach (var item in Fleets)
                            Fleet.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
            finally { IsBusy = false; }
        }

        async void GetOnSaveAssignment(object obj)
        {
            try
            {
                mcon = new(connString);
                mcon.Open();
                string cmdText1 = "Select * from FleetAssigned where Statuss='active' and NatID='" + NatID + "'";
                MySqlCommand cmd1 = new(cmdText1, mcon);
                MySqlDataAdapter adapter1 = new();
                adapter1.SelectCommand = cmd1;
                dt2.Clear();
                adapter1.Fill(dt2);
                if (dt2.Rows.Count != 0)
                {
                    await Shell.Current.DisplayAlert("Error!!!", "Driver has active assignment.", "Continue");
                    return;
                }
                    
                FleetAssigned fleet = new()
                {
                    Assigneed = "",
                    EPin = EPin,
                    FullName = FirstName +" "+ Surname,
                    NatID = NatID,
                    Plate = Plate,
                    Projectz = Projectz,
                    Statuss = Statuss
                };
                string cmdText = "INSERT INTO FleetAssigned (Assigneed, FullName, EPin," +
                    "Plate, Projectz, Statuss, NatID) " +
                    "VALUES (@Assigneed, @FullName, @EPin," +
                    "@Plate, @Projectz, @Statuss, @NatID)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@Assigneed", fleet.Assigneed);
                cmd.Parameters.AddWithValue("@FullName", fleet.FullName);
                cmd.Parameters.AddWithValue("@EPin", fleet.EPin);
                cmd.Parameters.AddWithValue("@Plate", fleet.Plate);
                cmd.Parameters.AddWithValue("@Projectz", fleet.Projectz);
                cmd.Parameters.AddWithValue("@Statuss", fleet.Statuss);
                cmd.Parameters.AddWithValue("@NatID", fleet.NatID);

                cmd.ExecuteNonQuery();


                await Shell.Current.DisplayAlert("Success!!!", "Assignment has been Recorded.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        private async void UpdateDriverNam()
        {
            try
            {
                Employees.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Employee order by id ASC LIMIT 100";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Perma = new()
                    {
                        new Employee
                        {
                            Code= Convert.ToInt32(dt.Rows[i]["id"].ToString()),
                            Country = dt.Rows[i]["Country"].ToString(),
                            County = dt.Rows[i]["County"].ToString(),
                            SubCounty= dt.Rows[i]["SubCounty"].ToString(),
                            Village = dt.Rows[i]["Village"].ToString(),
                            EPin = dt.Rows[i]["EPin"].ToString(),
                            EStatus = dt.Rows[i]["EStatus"].ToString(),
                            FirstName = dt.Rows[i]["FirstName"].ToString(),
                            MiddleName = dt.Rows[i]["MiddleName"].ToString(),
                            Surname = dt.Rows[i]["Surname"].ToString(),
                            Supervisor = dt.Rows[i]["Supervisor"].ToString(),
                            Phone = Convert.ToInt64(dt.Rows[i]["Phone"].ToString()),
                            Role = dt.Rows[i]["Role"].ToString(),
                            KRAPin = dt.Rows[i]["KRAPin"].ToString(),
                            Profession = dt.Rows[i]["Profession"].ToString(),
                            Salary = Convert.ToInt64(dt.Rows[i]["Salary"].ToString()),
                            NatID = Convert.ToInt64(dt.Rows[i]["NatID"].ToString()),
                        }
                    };
                    if (i < dt.Rows.Count)
                        foreach (var item in Perma)
                            Employees.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }

        MySqlConnection mcon;
        string connString = "server=localhost;uid=root;pwd=;database=roben;";
        List<Employee> Perma;
        List<Employee> Casualz;
        DataTable dt = new();
        DataTable dt1 = new();
        DataTable dt2 = new();
        async void GetLoadAddFleetAsignment(object obj)
        {
            try
            {
                await Shell.Current.DisplayAlert("Error","Test", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        async void GetLoadFleetManager(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(FleetManagerView));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        async Task GetLoadSavedEmployee()
        {
            IsBusy = true;
            try
            {
                Employees.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Employee order by id ASC LIMIT 100";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Perma = new()
                    {
                        new Employee
                        {
                            Code= Convert.ToInt32(dt.Rows[i]["id"].ToString()),
                            Country = dt.Rows[i]["Country"].ToString(),
                            County = dt.Rows[i]["County"].ToString(),
                            SubCounty= dt.Rows[i]["SubCounty"].ToString(),
                            Village = dt.Rows[i]["Village"].ToString(),
                            EPin = dt.Rows[i]["EPin"].ToString(),
                            EStatus = dt.Rows[i]["EStatus"].ToString(),
                            FirstName = dt.Rows[i]["FirstName"].ToString(),
                            MiddleName = dt.Rows[i]["MiddleName"].ToString(),
                            Surname = dt.Rows[i]["Surname"].ToString(),
                            Supervisor = dt.Rows[i]["Supervisor"].ToString(),
                            Phone = Convert.ToInt64(dt.Rows[i]["Phone"].ToString()),
                            Role = dt.Rows[i]["Role"].ToString(),
                            KRAPin = dt.Rows[i]["KRAPin"].ToString(),
                            Profession = dt.Rows[i]["Profession"].ToString(),
                            Salary = Convert.ToInt64(dt.Rows[i]["Salary"].ToString()),
                            NatID = Convert.ToInt64(dt.Rows[i]["NatID"].ToString()),
                        }
                    };
                    if (i < dt.Rows.Count)
                        foreach (var item in Perma)
                            Employees.Add(item);
                }
                
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
            finally { IsBusy = false; }
        }
        async Task GetLoadSavedCasualEmp()
        {
            IsBusy = true;
            try
            {
                Casuals.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Casuals order by id ASC LIMIT 100";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt1.Clear();
                adapter.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    Casualz = new()
                    {
                        new Employee
                        {
                            Code= Convert.ToInt32(dt1.Rows[i]["id"].ToString()),
                            EPin = dt1.Rows[i]["EPin"].ToString(),
                            EStatus = dt1.Rows[i]["EStatus"].ToString(),
                            FirstName = dt1.Rows[i]["FirstName"].ToString(),
                            MiddleName = dt1.Rows[i]["MiddleName"].ToString(),
                            Surname = dt1.Rows[i]["Surname"].ToString(),
                            Supervisor = dt1.Rows[i]["Supervisor"].ToString(),
                            Phone = Convert.ToInt64(dt1.Rows[i]["Phone"].ToString()),
                            Wages = dt1.Rows[i]["Wages"].ToString(),
                            Departmnet = dt1.Rows[i]["Department"].ToString(),
                            OvertimeRates = dt1.Rows[i]["OvertimeRates"].ToString(),
                            NatID = Convert.ToInt64(dt1.Rows[i]["NatID"].ToString()),
                        }
                    };
                    if (i < dt1.Rows.Count)
                        foreach (var item in Casualz)
                            Casuals.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
            finally { IsBusy = false; }
        }
        async void GetSaveEmployee(object obj)
        {
            try
            {
                Employee emp = new()
                {
                    Country = Country,
                    County = County,
                    SubCounty= SubCounty,
                    Village = Village,
                    EPin= EPin,
                    EStatus = "ACTIVE",
                    FirstName = FirstName,
                    MiddleName = MiddleName,
                    Surname = Surname,
                    Supervisor = Supervisor,
                    Role = Role,
                    Phone = Phone,
                    KRAPin = KRAPin,
                    Profession = Profession,
                    Salary = Salary,
                    NatID = NatID
                };
                mcon = new(connString);
                mcon.Open();
                string cmdText = "INSERT INTO Employee (Country, County, EPin," +
                    "EStatus, FirstName, KRAPin, MiddleName, NatID, SubCounty," +
                    "Phone, Profession, Role, Village, Supervisor, Surname, Salary) " +
                    "VALUES (@Country, @County, @EPin," +
                    "@EStatus,@FirstName, @KRAPin, @MiddleName, @NatID, @SubCounty," +
                    "@Phone, @Profession, @Role, @Village, @Supervisor, @Surname," +
                    "@Salary)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@Country", emp.Country);
                cmd.Parameters.AddWithValue("@County", emp.County);
                cmd.Parameters.AddWithValue("@EPin", emp.EPin);
                cmd.Parameters.AddWithValue("@EStatus", emp.EStatus);
                cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                cmd.Parameters.AddWithValue("@KRAPin", emp.KRAPin);
                cmd.Parameters.AddWithValue("@MiddleName", emp.MiddleName);
                cmd.Parameters.AddWithValue("@NatID", emp.NatID);
                cmd.Parameters.AddWithValue("@SubCounty", emp.SubCounty);
                cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                cmd.Parameters.AddWithValue("@Profession", emp.Profession);
                cmd.Parameters.AddWithValue("@Role", emp.Role);
                cmd.Parameters.AddWithValue("@Village", emp.Village);
                cmd.Parameters.AddWithValue("@Supervisor", emp.Supervisor);
                cmd.Parameters.AddWithValue("@Surname", emp.Surname);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);

                cmd.ExecuteNonQuery();


                await Shell.Current.DisplayAlert("Success!!!", "Employee has been Recorded.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        async void GetSaveCasualEmp(object obj)
        {
            try
            {
                Employee emp = new()
                {
                    EPin = EPin,
                    EStatus = EStatus,
                    FirstName = FirstName,
                    MiddleName = MiddleName,
                    Surname = Surname,
                    Supervisor = Supervisor,
                    Phone = Phone,
                    Wages = Wages,
                    Departmnet = Departmnet,
                    OvertimeRates = OvertimeRates,
                    NatID = NatID
                };
                mcon = new(connString);
                mcon.Open();
                string cmdText = "INSERT INTO Casuals (EPin," +
                    "EStatus, FirstName, MiddleName, NatID," +
                    "Phone, Department, OvertimeRates, Supervisor, Surname, Wages) " +
                    "VALUES (@EPin," +
                    "@EStatus,@FirstName, @MiddleName, @NatID," +
                    "@Phone, @Departmnet, @OvertimeRates, @Supervisor, @Surname," +
                    "@Wages)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@EPin", emp.EPin);
                cmd.Parameters.AddWithValue("@EStatus", emp.EStatus);
                cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", emp.MiddleName);
                cmd.Parameters.AddWithValue("@NatID", emp.NatID);
                cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                cmd.Parameters.AddWithValue("@Departmnet", emp.Departmnet);
                cmd.Parameters.AddWithValue("@OvertimeRates", emp.OvertimeRates);
                cmd.Parameters.AddWithValue("@Supervisor", emp.Supervisor);
                cmd.Parameters.AddWithValue("@Surname", emp.Surname);
                cmd.Parameters.AddWithValue("@Wages", emp.Wages);

                cmd.ExecuteNonQuery();


                await Shell.Current.DisplayAlert("Success!!!", "Casual Employee has been Recorded.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        async void GetLoadEmployee(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(NewEmployee));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
                throw;
            }
        }
        async void GetLoadCasualEmp()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(NewCasualEmployee)); 
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
                throw;
            }
        }
    }
}
