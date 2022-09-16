using Construction_Ke.Views.AssetsView;
using Construction_Ke.Model;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace Construction_Ke.ViewModel.AssetsViewModel
{
    public class AssetViewModel : BaseViewModel
    {
        private int code;
        private int mId;
        private string regNo;
        private double cost;
        private string logbook;
        private string yearz;
        private int vId;
        private string plate;
        private string condition;
        private string vehiclType;
        private string usedStatus;
        private string chasisNo;
        private string engineNo;
        MySqlConnection mcon;
        string connString = "server=localhost;uid=root;pwd=;database=roben;";
        public Command SaveVehicle { get;}
        public Command LoadSavedVehicles { get; }
        public Command LoadVehicle { get; }
        public Command LoadMachinery { get; }
        public Command SaveMachinery { get; }
        public Command LoadSaveMachinery { get; }
        public int Code { get => code; set => code = value; }
        public int MId { get => mId; set => mId = value; }
        public string RegNo { get => regNo; set => regNo = value; }
        public double Cost { get => cost; set => cost = value; }
        public string Logbook { get => logbook; set => logbook = value; }
        public string Yearz { get => yearz; set => yearz = value; }
        public string Condition { get => condition; set => condition = value; }
        public string VehiclType { get => vehiclType; set => vehiclType = value; }
        public string UsedStatus { get => usedStatus; set => usedStatus = value; }
        public string ChasisNo { get => chasisNo; set => chasisNo = value; }
        public string EngineNo { get => engineNo; set => engineNo = value; }
        public int VId { get => vId; set => vId = value; }
        public string Plate { get => plate; set => plate = value; }

        public AssetViewModel()
        {
            SaveVehicle = new(OnSaveVehicle);
            LoadSavedVehicles = new(async () => await OnLoadSavedVehicles());
            LoadVehicle = new(GetLoadVehicle);
            Vehicles = new();
            Machineries = new();
            LoadMachinery = new(GetLoadMachinery);
            SaveMachinery = new(OnSaveMachinery);
            LoadSaveMachinery = new(async () => await OnLoadSaveMachinery());
        }
        public void OnAppearing()
        {
            IsBusy = true;
            //SelectedItem = null;
        }
        List<RobenMachinery> robenMachineries;
        List<RobenVehicles> robenVehicles;
        DataTable dt = new();
        DataTable dt1 = new();
        public ObservableCollection<RobenMachinery> Machineries { get; }
        public ObservableCollection<RobenVehicles> Vehicles { get; }
        async Task OnLoadSavedVehicles()
        {
            IsBusy = true;
            try
            {
                Vehicles.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Vehicles order by id ASC LIMIT 100";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    robenVehicles = new()
                    {
                        new RobenVehicles
                        {
                            Condition = dt.Rows[i]["VCondition"].ToString(),
                            VId = Convert.ToInt32(dt.Rows[i]["id"].ToString()),
                            ChasisNo = dt.Rows[i]["ChasisNo"].ToString(),
                            Cost = Convert.ToDouble(dt.Rows[i]["Cost"].ToString()),
                            EngineNo = dt.Rows[i]["EngineNo"].ToString(),
                            Logbook = dt.Rows[i]["Logbook"].ToString(),
                            Plate = dt.Rows[i]["Plate"].ToString(),
                            UsedStatus = dt.Rows[i]["UsedStatus"].ToString(),
                            VehiclType = dt.Rows[i]["VehiclType"].ToString(),
                            Yearz = dt.Rows[i]["Yearz"].ToString(),
                        }
                    };
                    if (i < dt.Rows.Count)
                    {
                        foreach (var item in robenVehicles)
                        {
                            Vehicles.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
            finally { IsBusy = false; }
        }
        async Task OnLoadSaveMachinery()
        {
            IsBusy = true;
            try
            {
                Machineries.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Machinery order by id ASC LIMIT 100";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt1.Clear();
                adapter.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    robenMachineries = new()
                    {
                        new RobenMachinery
                        {
                            Condition = dt1.Rows[i]["MCondition"].ToString(),
                            MId = Convert.ToInt32(dt1.Rows[i]["id"].ToString()),
                            ChasisNo = dt1.Rows[i]["ChasisNo"].ToString(),
                            Cost = Convert.ToDouble(dt1.Rows[i]["Cost"].ToString()),
                            EngineNo = dt1.Rows[i]["EngineNo"].ToString(),
                            Logbook = dt1.Rows[i]["Logbook"].ToString(),
                            RegNo = dt1.Rows[i]["RegNo"].ToString(),
                            UsedStatus = dt1.Rows[i]["UsedStatus"].ToString(),
                            VehiclType = dt1.Rows[i]["VehiclType"].ToString(),
                            Yearz = dt1.Rows[i]["Yearz"].ToString(),
                        }
                    };
                    if (i < dt1.Rows.Count)
                    {
                        foreach (var item in robenMachineries)
                        {
                            Machineries.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
            finally { IsBusy = false; }
        }
        async void OnSaveVehicle(object obj)
        {
            try
            {
                RobenVehicles vehicles = new()
                {
                    Condition = Condition,
                    ChasisNo = ChasisNo,
                    Cost = Cost,
                    EngineNo = EngineNo,
                    Logbook = logbook,
                    Plate = Plate,
                    UsedStatus = UsedStatus,
                    VehiclType = VehiclType,
                    Yearz = Yearz
                };
                mcon = new(connString);
                mcon.Open();
                string cmdText = "INSERT INTO Vehicles (VCondition, ChasisNo, Cost," +
                    "EngineNo, logbook, Plate, UsedStatus, VehiclType, Yearz) " +
                    "VALUES (@Condition, @ChasisNo, @Cost," +
                    "@EngineNo,@logbook, @Plate, @UsedStatus, @VehiclType, @Yearz)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@ChasisNo", vehicles.ChasisNo);
                cmd.Parameters.AddWithValue("@Condition", vehicles.Condition);
                cmd.Parameters.AddWithValue("@Cost", vehicles.Cost);
                cmd.Parameters.AddWithValue("@EngineNo", vehicles.EngineNo);
                cmd.Parameters.AddWithValue("@Logbook", vehicles.Logbook);
                cmd.Parameters.AddWithValue("@Plate", vehicles.Plate);
                cmd.Parameters.AddWithValue("@UsedStatus", vehicles.UsedStatus);
                cmd.Parameters.AddWithValue("@VehiclType", vehicles.VehiclType);
                cmd.Parameters.AddWithValue("@Yearz", vehicles.Yearz);

                cmd.ExecuteNonQuery();


                await Shell.Current.DisplayAlert("Success!!!", "Asset Recorded.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        async void OnSaveMachinery(object obj)
        {
            try
            {
                RobenMachinery machinery = new()
                {
                    Condition = Condition,
                    ChasisNo = ChasisNo,
                    Cost = Cost,
                    EngineNo = EngineNo,
                    Logbook = logbook,
                    RegNo = RegNo,
                    UsedStatus = UsedStatus,
                    VehiclType = VehiclType,
                    Yearz = Yearz,
                };
                mcon = new(connString);
                mcon.Open();
                string cmdText = "INSERT INTO Machinery (MCondition, ChasisNo, Cost," +
                    "EngineNo, logbook, RegNo, UsedStatus, VehiclType, Yearz) " +
                    "VALUES (@Condition, @ChasisNo, @Cost," +
                    "@EngineNo,@logbook, @RegNo, @UsedStatus, @VehiclType, @Yearz)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@ChasisNo", machinery.ChasisNo);
                cmd.Parameters.AddWithValue("@Condition", machinery.Condition);
                cmd.Parameters.AddWithValue("@Cost", machinery.Cost);
                cmd.Parameters.AddWithValue("@EngineNo", machinery.EngineNo);
                cmd.Parameters.AddWithValue("@Logbook", machinery.Logbook);
                cmd.Parameters.AddWithValue("@RegNo", machinery.RegNo);
                cmd.Parameters.AddWithValue("@UsedStatus", machinery.UsedStatus);
                cmd.Parameters.AddWithValue("@VehiclType", machinery.VehiclType);
                cmd.Parameters.AddWithValue("@Yearz", machinery.Yearz);

                cmd.ExecuteNonQuery();


                await Shell.Current.DisplayAlert("Success!!!", "Machinery Recorded.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        async void GetLoadMachinery(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(MachineryManager));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        private async void GetLoadVehicle(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(VehicleManager));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
    }
    
}
