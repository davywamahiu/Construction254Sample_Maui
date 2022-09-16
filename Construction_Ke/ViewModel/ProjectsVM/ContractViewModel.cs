using System.Collections.ObjectModel;
using Construction_Ke.Model;
using System.Data;
using CommunityToolkit.Mvvm.Input;
using MySql.Data.MySqlClient;
using Construction_Ke.Views.ProjectsDS;

namespace Construction_Ke.ViewModel.ProjectsVM
{
    public class ContractViewModel : BaseViewModel
    {
        public ObservableCollection<ContractsModel> Models { get; } = new();
        public ObservableCollection<ActAndTask> Acts { get; } = new();
        public ObservableCollection<TaskasList> TaskasLists  { get; } = new();
        public ObservableCollection<Taskis> Taskiz  { get; } = new();
        public ObservableCollection<MaterialModel> MaterialModels { get; } = new();
        public ObservableCollection<MaterialModel> MaterialModelz { get; } = new();

        public Command SaveContract { get; }
        public Command LoadSavedContract { get; }
        public Command LoadWorkList { get; }
        public Command SaveNewActivity { get; }
        public Command LoadAllTaskas { get; }
        public Command SaveNewTaskas { get; }
        public Command SaveSubTaskis { get; }
        public Command LoadSavedSubTask { get; }
        public Command<string> ItemTapped { get; }
        public Command LoadSavedActivitaz {get;}
        public Command SaveMaterial {get;}
        public Command LoadSaveMaterial {get;}
        public Command LoadSaveBoqList {get;}
        public Command SaveBoqList { get; }
        private Taskis _taskis;
        private double amount;
        private double rati;
        private double quantity;
        private string uniti;
        private string matSerial;
        private int code;
        private string projectType;
        private string projectName;
        private string client;
        private double cost;
        private string resident;
        private string road;
        private string engineer;
        private string surveyor;
        private double distance;
        private DateTime signedOn;
        private DateTime startD;
        private DateTime expectedD;
        private DateTime revisedD;
        private int allProjects;
        private int compProjects;
        private int pendProjects;
        private int activeProjectz;
        private string myACtivitiz;
        private string actName;
        private string actDependsOn;
        private string actCompCreteria;
        private DateTime actStartDate;
        private DateTime actEndDate;
        private string activitiz;
        private string tasDependsOn;
        private string tasCompCreteria;
        private DateTime tasStartDate;
        private DateTime tasEndDate;
        private string _selectedItem;
        private string subTaski;
        private string description;
        private string rdSection;
        private string materials;
        private string machines;
        private string trucks;
        private string casuals;
        public ContractViewModel()
        {
            DateTime dat = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            ActStartDate = dat;
            ActEndDate= dat;
            SignedOn = dat;
            StartD = dat;
            RevisedD = dat;
            ExpectedD = dat;
            TasStartDate = dat;
            TasEndDate = dat;
            LoadAddProjects = new(GetLoadAddProjects);
            LoadAllProjects = new(GetLoadAllProjects);
            SaveContract = new(OnSaveContract);
            LoadWorkList = new(GetLoadWorkList);
            SaveNewActivity = new(OnSaveNewActivity);
            SaveNewTaskas = new(OnSaveNewTaskas);
            SaveMaterial = new(OnSaveMaterial);
            SaveSubTaskis = new(OnSaveSubTaskis);
            SaveBoqList = new(OnSaveBoqList);
            ItemTapped = new(OnItemSelected);
            LoadSaveBoqList = new(async () => await OnLoadSaveBoqList());
            LoadSaveMaterial = new(async () => await OnLoadSaveMaterial());
            LoadSavedSubTask = new(async () => await OnLoadSavedSubTask());
            LoadSavedActivitaz = new(async () => await OnLoadSavedActivitaz());
            LoadSavedContract = new(async () => await OnLoadSavedContract());
            UpdateAllProjectz();
        }

        async Task OnLoadSaveBoqList()
        {
            IsBusy = true;
            try
            {
                MaterialModelz.Clear();
                mcon = new(connString);// where ActId='" + item + "' 
                mcon.Open();
                string cmdText = "Select * from BoqList ORDER BY id ASC";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    md = new()
                    {
                        new MaterialModel
                        {
                            Code = Convert.ToInt32(dt.Rows[i]["id"].ToString()),
                            Material = dt.Rows[i]["Description"].ToString(),
                            MatSerial = dt.Rows[i]["MatSerial"].ToString(),
                            Uniti = dt.Rows[i]["Uniti"].ToString(),
                            Rati = Convert.ToDouble(dt.Rows[i]["Rati"].ToString()),
                            Projectz = dt.Rows[i]["Projectz"].ToString()
                        }
                    };
                    if (i < dt.Rows.Count)
                        foreach (var item in md)
                            MaterialModelz.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
            finally { IsBusy = false; }
        }

        List<MaterialModel> md = new();
        async Task OnLoadSaveMaterial()
        {
            IsBusy = true;
            try
            {
                MaterialModels.Clear();
                mcon = new(connString);// where ActId='" + item + "' 
                mcon.Open();
                string cmdText = "Select * from BoqMaterial ORDER BY id ASC";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    md = new()
                    {
                        new MaterialModel
                        {
                            Code = Convert.ToInt32(dt.Rows[i]["id"].ToString()),
                            Material = dt.Rows[i]["Material"].ToString(),
                            Amount = Convert.ToDouble(dt.Rows[i]["Amount"].ToString()),
                            MatSerial = dt.Rows[i]["MatSerial"].ToString(),
                            Quantity = Convert.ToDouble(dt.Rows[i]["Quantity"].ToString()),
                            Rati = Convert.ToDouble(dt.Rows[i]["Rati"].ToString()),
                            Uniti = dt.Rows[i]["Uniti"].ToString()
                        }
                    };
                    if (i < dt.Rows.Count)
                        foreach (var item in md)
                            MaterialModels.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
            finally { IsBusy = false; }
        }
        async void OnSaveBoqList(object obj)
        {
            MaterialModel materialModel = new()
            {
                Material = Materials,
                Amount = Amount,
                MatSerial = MatSerial,
                Quantity = Quantity,
                Rati = Rati,
                Uniti = Uniti,
                Projectz = ProjectName
            };
            try
            {
                string cmdText = "INSERT INTO BoqList (Description, MatSerial, Uniti," +
                    "Rati, Projectz) " +
                    "VALUES (@Material, @MatSerial, @Uniti," +
                    "@Rati, @Projectz)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@Material", materialModel.Material);
                cmd.Parameters.AddWithValue("@MatSerial", materialModel.MatSerial);
                cmd.Parameters.AddWithValue("@Uniti", materialModel.Uniti);
                cmd.Parameters.AddWithValue("@Rati", materialModel.Rati);
                cmd.Parameters.AddWithValue("@Projectz", materialModel.Projectz);
                cmd.ExecuteNonQuery();


                await Shell.Current.DisplayAlert("Success!!!", materialModel.Material + " has been Recorded.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        async void OnSaveMaterial(object obj)
        {
            MaterialModel materialModel = new()
            {
                Material = Materials,
                Amount = Amount,
                MatSerial = MatSerial,
                Quantity = Quantity,
                Rati = Rati,
                Uniti = Uniti
            };
            try
            {
                string cmdText = "INSERT INTO BoqMaterial (Material, MatSerial, Quantity," +
                    "Rati, Uniti, Amount) " +
                    "VALUES (@Material, @MatSerial, @Quantity," +
                    "@Rati, @Uniti, @Amount)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@Material", materialModel.Material);
                cmd.Parameters.AddWithValue("@MatSerial", materialModel.MatSerial);
                cmd.Parameters.AddWithValue("@Quantity", materialModel.Quantity);
                cmd.Parameters.AddWithValue("@Rati", materialModel.Rati);
                cmd.Parameters.AddWithValue("@Uniti", materialModel.Uniti);
                cmd.Parameters.AddWithValue("@Amount", materialModel.Amount);
                cmd.ExecuteNonQuery();


                await Shell.Current.DisplayAlert("Success!!!", materialModel.Material + " has been Recorded.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        List<Taskis> SubTusk = new();
        async Task OnLoadSavedSubTask()
        {
            IsBusy = true;
            try
            {
                Taskiz.Clear();
                mcon = new(connString);// where ActId='" + item + "' 
                mcon.Open();
                string cmdText = "Select * from SubTaska ORDER BY id ASC";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SubTusk = new()
                    {
                        new Taskis{ Code = Convert.ToInt32(dt.Rows[i]["id"].ToString()),
                         Casuals = dt.Rows[i]["Casuals"].ToString(),
                         Machines = dt.Rows[i]["Machines"].ToString(),
                         Materials = dt.Rows[i]["Materials"].ToString(),
                         Description = dt.Rows[i]["Description"].ToString(),
                         SubTaski= dt.Rows[i]["SubTaski"].ToString(),
                         RdSection = dt.Rows[i]["RdSection"].ToString(),
                         Trucks = dt.Rows[i]["Trucks"].ToString()}
                    };
                    if (i < dt.Rows.Count)
                        foreach (var itemz in SubTusk)
                        {
                            //Taskiz.Add(itemz);
                        }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
            finally { IsBusy = false; }
        }

        async void OnSaveSubTaskis(object obj)
        {
            TaskasList taskas = new()
            {
                Activitiz = Activitiz
            };
            
            try
            {
                mcon = new(connString);
                mcon.Open();
                string cmdText1 = "Select * from Taskas where Activitiz='" + taskas.Activitiz + "' ORDER BY id ASC";
                MySqlCommand cmd1 = new(cmdText1, mcon);
                MySqlDataAdapter adapter1 = new();
                adapter1.SelectCommand = cmd1;
                dt.Clear();
                adapter1.Fill(dt);
                if (dt.Rows.Count > 0)
                    Code = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                Taskis taskis = new()
                {
                    Casuals = Casuals,
                    Description = Description,
                    Machines = Machines,
                    Materials = Materials,
                    RdSection = RdSection,
                    Trucks = Trucks,
                    SubTaski = SubTaski,
                    Code = Code
                };
                string cmdText = "INSERT INTO SubTaska (SubTaski, Casuals, Description," +
                    "Machines, Materials, RdSection, Trucks, ActId) " +
                    "VALUES (@SubTaski, @Casuals, @Description," +
                    "@Machines, @Materials, @RdSection, @Trucks, @Code)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@SubTaski", taskis.SubTaski);
                cmd.Parameters.AddWithValue("@Casuals", taskis.Casuals);
                cmd.Parameters.AddWithValue("@Description", taskis.Description);
                cmd.Parameters.AddWithValue("@Machines", taskis.Machines);
                cmd.Parameters.AddWithValue("@Materials", taskis.Materials);
                cmd.Parameters.AddWithValue("@RdSection", taskis.RdSection);
                cmd.Parameters.AddWithValue("@Trucks", taskis.Trucks);
                cmd.Parameters.AddWithValue("@Code", taskis.Code);
                cmd.ExecuteNonQuery();


                await Shell.Current.DisplayAlert("Success!!!", taskis.SubTaski + " has been Recorded.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        private TaskasList _TaskasLists;
        public TaskasList SelectedTaskasLists
        {
            get => _TaskasLists;
            set
            {
                SetProperty(ref _TaskasLists, value);
                OnLoadAllTaskas(value);
            }
        }
        public Taskis SelectedSubTask
        {
            get => _taskis;
            set
            {
                SetProperty(ref _taskis, value);
                OnSubTaskSelected(value);
            }
        }
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        async void OnSubTaskSelected(Taskis item)
        {
            if (item == null)
                return;
            try
            {
                Taskiz.Clear();
                mcon = new(connString);// where ActId='" + item + "' 
                mcon.Open();
                string cmdText = "Select * from SubTaska where ActId='" + item.Code + "' ORDER BY id ASC";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SubTusk = new()
                    {
                        new Taskis{ Code = Convert.ToInt32(dt.Rows[i]["id"].ToString()),
                         Casuals = dt.Rows[i]["Casuals"].ToString(),
                         Machines = dt.Rows[i]["Machines"].ToString(),
                         Materials = dt.Rows[i]["Materials"].ToString(),
                         Description = dt.Rows[i]["Description"].ToString(),
                         SubTaski= dt.Rows[i]["SubTaski"].ToString(),
                         RdSection = dt.Rows[i]["RdSection"].ToString(),
                         Trucks = dt.Rows[i]["Trucks"].ToString()}
                    };
                    if (i < dt.Rows.Count)
                        foreach (var itemz in SubTusk)
                        {
                            Taskiz.Add(itemz);
                        }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        async void OnItemSelected(string item)
        {
            if (item == null)
                return;
            try
            {
                Acts.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Activitiza where ProjectName='"+ item +"'  ORDER BY id ASC";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tasks = new()
                    {
                        new ActAndTask{ Code = Convert.ToInt32(dt.Rows[i]["id"].ToString()),
                         ActCompCreteria = dt.Rows[i]["ActCompCreteria"].ToString(),
                         ActDependsOn = dt.Rows[i]["ActDependsOn"].ToString(),
                         ActEndDate = Convert.ToDateTime(dt.Rows[i]["ActEndDate"].ToString()),
                         ActName = dt.Rows[i]["ActName"].ToString(),
                         ActStartDate= Convert.ToDateTime(dt.Rows[i]["ActStartDate"].ToString()),
                         MyACtivitiz = dt.Rows[i]["MyACtivitiz"].ToString(),
                         ProjectName = dt.Rows[i]["ProjectName"].ToString()}
                    };
                    if (i < dt.Rows.Count)
                        foreach (var itemz in tasks)
                        {
                            Acts.Add(itemz);
                        }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        DataTable dt3 = new();
        List<TaskasList> Taskas = new();
        async void OnLoadAllTaskas(TaskasList obj)
        {
            try
            {
                if (obj == null)
                    return;
                mcon = new(connString);
                mcon.Open();
                TaskasLists.Clear();
                string cmdText = "Select * from Taskas where ActId ='"+ obj.Code + "' ORDER BY id ASC";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt3.Clear();
                adapter.Fill(dt3);
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    Taskas = new()
                    {
                        new TaskasList
                        {
                            Code=Convert.ToInt32(dt3.Rows[i]["id"].ToString()),
                            Activitiz = dt3.Rows[i]["Activitiz"].ToString(),
                            TasCompCreteria = dt3.Rows[i]["TasCompCreteria"].ToString(),
                            TasDependsOn = dt3.Rows[i]["TasDependsOn"].ToString(),
                            TasEndDate = Convert.ToDateTime(dt3.Rows[i]["TasEndDate"].ToString()),
                            TasStartDate = Convert.ToDateTime(dt3.Rows[i]["TasStartDate"].ToString())
                        }
                    };
                    if (i < dt3.Rows.Count)
                        foreach (var item in Taskas)
                            TaskasLists.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        async void OnSaveNewTaskas(object obj)
        {
            
            try
            {
                mcon = new(connString);
                mcon.Open();
                ActAndTask actAnd = new()
                {
                    MyACtivitiz = MyACtivitiz
                };
                string cmdText1 = "Select * from Activitiza where MyACtivitiz='" + actAnd.MyACtivitiz + "' ORDER BY id ASC";
                MySqlCommand cmd1 = new(cmdText1, mcon);
                MySqlDataAdapter adapter1 = new();
                adapter1.SelectCommand = cmd1;
                dt.Clear();
                adapter1.Fill(dt);
                if (dt.Rows.Count > 0)
                    Code = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                TaskasList taskas = new()
                {
                    Code = Code,
                    Activitiz = Activitiz,
                    TasCompCreteria = TasCompCreteria,
                    TasDependsOn = TasDependsOn,
                    TasEndDate = TasEndDate,
                    TasStartDate = TasStartDate
                };
                string cmdText = "INSERT INTO Taskas (Activitiz, TasDependsOn," +
                    "TasCompCreteria, TasStartDate, TasEndDate, ActId) " +
                    "VALUES (@Activitiz, @TasDependsOn," +
                    "@TasCompCreteria, @TasStartDate, @TasEndDate, @Code)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@Activitiz", taskas.Activitiz);
                cmd.Parameters.AddWithValue("@TasDependsOn", taskas.TasDependsOn);
                cmd.Parameters.AddWithValue("@TasCompCreteria", taskas.TasCompCreteria);
                cmd.Parameters.AddWithValue("@TasStartDate", taskas.TasStartDate);
                cmd.Parameters.AddWithValue("@TasEndDate", taskas.TasEndDate);
                cmd.Parameters.AddWithValue("@Code", taskas.Code);
                cmd.ExecuteNonQuery();


                await Shell.Current.DisplayAlert("Success!!!", taskas.Activitiz + " has been Recorded.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        DataTable dt = new();
        List<ActAndTask> tasks = new();
        async Task OnLoadSavedActivitaz()
        {
            IsBusy = true;
            try
            {
                Acts.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Activitiza ORDER BY id ASC";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tasks = new()
                    {
                        new ActAndTask{ Code = i+1,
                         ActCompCreteria = dt.Rows[i]["ActCompCreteria"].ToString(),
                         ActDependsOn = dt.Rows[i]["ActDependsOn"].ToString(),
                         ActEndDate = Convert.ToDateTime(dt.Rows[i]["ActEndDate"].ToString()),
                         ActName = dt.Rows[i]["ActName"].ToString(),
                         ActStartDate= Convert.ToDateTime(dt.Rows[i]["ActStartDate"].ToString()),
                         MyACtivitiz = dt.Rows[i]["MyACtivitiz"].ToString(),
                         ProjectName = dt.Rows[i]["ProjectName"].ToString()}
                    };
                    if(i < dt.Rows.Count)
                        foreach (var item in tasks)
                        {
                            Acts.Add(item);
                        }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
            finally { IsBusy = false; }
        }

        private async void OnSaveNewActivity(object bj)
        {
            
            ActAndTask actAnd = new()
            {
                ActCompCreteria = ActCompCreteria,
                ActDependsOn = ActDependsOn,
                ActEndDate = ActEndDate,
                ActName = ActName,
                ActStartDate = ActStartDate,
                MyACtivitiz = MyACtivitiz,
                ProjectName = ProjectName
            };
            try
            {
                mcon = new(connString);
                mcon.Open();
                string cmdText = "INSERT INTO Activitiza (ActCompCreteria, ActDependsOn," +
                    "ActName, ActStartDate, ActEndDate, MyACtivitiz, ProjectName) " +
                    "VALUES (@ActCompCreteria, @ActDependsOn," +
                    "@ActName, @ActStartDate, @ActEndDate, @MyACtivitiz, @ProjectName)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@ActCompCreteria", actAnd.ActCompCreteria);
                cmd.Parameters.AddWithValue("@ActDependsOn", actAnd.ActDependsOn);
                cmd.Parameters.AddWithValue("@ActName", actAnd.ActName);
                cmd.Parameters.AddWithValue("@ActStartDate", actAnd.ActStartDate);
                cmd.Parameters.AddWithValue("@ActEndDate", actAnd.ActEndDate);
                cmd.Parameters.AddWithValue("@MyACtivitiz", actAnd.MyACtivitiz);
                cmd.Parameters.AddWithValue("@ProjectName", actAnd.ProjectName);
                cmd.ExecuteNonQuery();


                await Shell.Current.DisplayAlert("Success!!!", actAnd.ActName + actAnd.MyACtivitiz + " has been Recorded.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }

        async void GetLoadWorkList(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(TaskListPage));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        public List<string> Strings { get; set; } = new();
        private async void UpdateAllProjectz()
        {
            try
            {
                Models.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Contracts ORDER BY id ASC LIMIT 100";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt2.Clear();
                adapter.Fill(dt2);
                AllProjects = dt2.Rows.Count;
                CompProjects = 0;
                PendProjects = 0;
                ActiveProjectz = 0;
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    Strings.Add(dt2.Rows[i]["ProjectName"].ToString());
                    if (Convert.ToDateTime(dt2.Rows[i]["ExpectedD"].ToString()).Date > DateTime.Today.Date)
                        ActiveProjectz++;
                    else if (Convert.ToDateTime(dt2.Rows[i]["StartD"].ToString()).Date > DateTime.Today.Date)
                        PendProjects++;
                    else if (Convert.ToDateTime(dt2.Rows[i]["ExpectedD"].ToString()).Date < DateTime.Today.Date)
                        CompProjects++;
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
            SelectedItem = null;
            SelectedSubTask = null;
        }
        List<ContractsModel> Contracts = new();
        async Task OnLoadSavedContract()
        {
            IsBusy = true;
            try
            {
                Models.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Contracts ORDER BY id ASC LIMIT 100";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt2.Clear();
                adapter.Fill(dt2);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    Contracts = new()
                    {
                        new ContractsModel
                        {
                            Code = Convert.ToInt32(dt2.Rows[i]["id"].ToString()),
                            Client = dt2.Rows[i]["Client"].ToString(),
                            Cost = Convert.ToDouble(dt2.Rows[i]["Cost"].ToString()),
                            Distance = Convert.ToDouble(dt2.Rows[i]["Distance"].ToString()),
                            Engineer = dt2.Rows[i]["Engineer"].ToString(),
                            ExpectedD = Convert.ToDateTime(dt2.Rows[i]["ExpectedD"].ToString()),
                            ProjectName = dt2.Rows[i]["ProjectName"].ToString(),
                            ProjectType = dt2.Rows[i]["ProjectType"].ToString(),
                            Resident = dt2.Rows[i]["Resident"].ToString(),
                            RevisedD = Convert.ToDateTime(dt2.Rows[i]["RevisedD"].ToString()).Date,
                            Road = dt2.Rows[i]["Road"].ToString(),
                            SignedOn = Convert.ToDateTime(dt2.Rows[i]["SignedOn"].ToString()),
                            StartD = Convert.ToDateTime(dt2.Rows[i]["StartD"].ToString()).Date,
                            Surveyor = dt2.Rows[i]["Surveyor"].ToString()
                        }
                    };
                    if (i < dt2.Rows.Count)
                        foreach (var item in Contracts)
                            Models.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
            finally { IsBusy = false; }
        }

        MySqlConnection mcon;
        string connString = "server=localhost;uid=root;pwd=;database=roben;";
        DataTable dt2 = new();
        async void OnSaveContract(object obj)
        {
            try
            {
                ContractsModel contracts = new()
                {
                    Code = Code,
                    Client = Client,
                    Cost = Cost,
                    Distance = Distance,
                    Engineer = Engineer,
                    ExpectedD = ExpectedD,
                    ProjectName = ProjectName,
                    ProjectType = ProjectType,
                    Resident = Resident,
                    RevisedD = RevisedD,
                    Road = Road,
                    SignedOn = SignedOn,
                    StartD = StartD,
                    Surveyor = Surveyor
                };
                mcon = new(connString);
                mcon.Open();
                string cmdText = "INSERT INTO Contracts (Client, Cost," +
                    "Distance, Engineer, ExpectedD, projectName, ProjectType, Resident," +
                    "RevisedD, Road, SignedOn, StartD, Surveyor) " +
                    "VALUES (@Client, @Cost," +
                    "@Distance, @Engineer, @ExpectedD, @projectName, @ProjectType, @Resident," +
                    "@RevisedD, @Road, @SignedOn, @StartD, @Surveyor)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@Client", contracts.Client);
                cmd.Parameters.AddWithValue("@Cost", contracts.Cost);
                cmd.Parameters.AddWithValue("@Distance", contracts.Distance);
                cmd.Parameters.AddWithValue("@Engineer", contracts.Engineer);
                cmd.Parameters.AddWithValue("@ExpectedD", contracts.ExpectedD);
                cmd.Parameters.AddWithValue("@ProjectName", contracts.ProjectName);
                cmd.Parameters.AddWithValue("@ProjectType", contracts.ProjectType);
                cmd.Parameters.AddWithValue("@Resident", contracts.Resident);
                cmd.Parameters.AddWithValue("@RevisedD", contracts.RevisedD);
                cmd.Parameters.AddWithValue("@Road", contracts.Road);
                cmd.Parameters.AddWithValue("@SignedOn", contracts.SignedOn);
                cmd.Parameters.AddWithValue("@StartD", contracts.StartD);
                cmd.Parameters.AddWithValue("@Surveyor", contracts.Surveyor);

                cmd.ExecuteNonQuery();


                await Shell.Current.DisplayAlert("Success!!!", contracts.Road +" has been Recorded.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        public Command LoadAllProjects { get; }
        public Command LoadAddProjects { get; }
        private async void GetLoadAllProjects(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ProjectList));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        private async void GetLoadAddProjects(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AddProjectsToList));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        public int Code { get => code; set => code = value; }
        public string ProjectType { get => projectType; set => projectType = value; }
        public string ProjectName { get => projectName; set => projectName = value; }
        public string Client { get => client; set => client = value; }
        public double Cost { get => cost; set => cost = value; }
        public string Resident { get => resident; set => resident = value; }
        public string Road { get => road; set => road = value; }
        public string Engineer { get => engineer; set => engineer = value; }
        public string Surveyor { get => surveyor; set => surveyor = value; }
        public double Distance { get => distance; set => distance = value; }
        public DateTime SignedOn { get => signedOn; set => signedOn = value; }
        public DateTime StartD { get => startD; set => startD = value; }
        public DateTime ExpectedD { get => expectedD; set => expectedD = value; }
        public DateTime RevisedD { get => revisedD; set => revisedD = value; }
        
        public int AllProjects { get => allProjects; set => allProjects = value; }
        public int CompProjects { get => compProjects; set => compProjects = value; }
        public int PendProjects { get => pendProjects; set => pendProjects = value; }
        public int ActiveProjectz { get => activeProjectz; set => activeProjectz = value; }
        public string ActName { get => actName; set => actName = value; }
        public string ActDependsOn { get => actDependsOn; set => actDependsOn = value; }
        public string ActCompCreteria { get => actCompCreteria; set => actCompCreteria = value; }
        public DateTime ActStartDate { get => actStartDate; set => actStartDate = value; }
        public DateTime ActEndDate { get => actEndDate; set => actEndDate = value; }
        public string MyACtivitiz { get => myACtivitiz; set => myACtivitiz = value; }
        public string Activitiz { get => activitiz; set => activitiz = value; }
        public string TasDependsOn { get => tasDependsOn; set => tasDependsOn = value; }
        public string TasCompCreteria { get => tasCompCreteria; set => tasCompCreteria = value; }
        public DateTime TasStartDate { get => tasStartDate; set => tasStartDate = value; }
        public DateTime TasEndDate { get => tasEndDate; set => tasEndDate = value; }
        public string Machines { get => machines; set => machines = value; }
        public string Trucks { get => trucks; set => trucks = value; }
        public string Casuals { get => casuals; set => casuals = value; }
        public string RdSection { get => rdSection; set => rdSection = value; }
        public string Materials { get => materials; set => materials = value; }
        public string SubTaski { get => subTaski; set => subTaski = value; }
        public string Description { get => description; set => description = value; }
        public string MatSerial { get => matSerial; set => matSerial = value; }
        public double Amount { get => amount; set => amount = value; }
        public double Rati { get => rati; set => rati = value; }
        public double Quantity { get => quantity; set => quantity = value; }
        public string Uniti { get => uniti; set => uniti = value; }
    }
}
