using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using Construction_Ke.Database;
using Construction_Ke.Model;

using System.Collections.ObjectModel;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Data;

namespace Construction_Ke.ViewModel.FuelViewModels
{
    public class AddNewFuelVM : BaseViewModel
    {
        DataTable dt = new();
        private int code;
        private int vehicleId;
        private string driver;
        private long supplierPhone;
        private string numberPlate;
        private string supplier;
        private decimal suppliedLitters;
        private DateTime supplyDate;
        private string supplyTime;
        private string fuelType;
        private decimal refueliedLitters;
        private DateTime refuelDate;
        private string refuelTime;
        private double milage;
        private double prevMilage;
        private string refuelier;
        private double diesel;
        private double petrol;
        private double superpetrol;
        private double myFuels;
        public ObservableCollection<FuelLorr> Fuels { get; }
        public ObservableCollection<RecieveFuelModel> ReFuel { get; }
        public Command LoadFuelCommand { get; }
        public Command LoadReFuelCommand { get; }
        public Command SaveNewRefuel { get; }
        public Command LoadRefueling { get; }
        public Command SaveSupplier { get; }
        public AddNewFuelVM()
        {
            LoadFuelCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadReFuelCommand = new Command(async () => await ExecuteLoadRefuelCommand());
            SaveCommand1 = new Command(OnSave);
            recieveFuel1 = new();
            SaveSupplier = new(OnSaveSupplier);
            Fuels = new();
            ReFuel = new();
            LoadFuelPrev();
            LoadReFuelPrev();
            upDateVId();
            SupplyDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            RefuelDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            CancelCommand1 = new Command(OnCancel);
            SaveNewRefuel = new(GetSaveNewRefuel);
            this.PropertyChanged +=
                (_, __) => SaveCommand1.ChangeCanExecute();
        }
        async void OnSaveSupplier(object obj)
        {
            FuelSuppliers fuel = new() { 
                Driver = Driver, 
                NumberPlate = NumberPlate, 
                Supplier = Supplier, 
                SupplierPhone = SupplierPhone
            };
            try
            {
                mcon = new(connString);
                mcon.Open();
                string cmdText = "INSERT INTO mysuppliers (Driver, Supplier, Plate, SupplierPhone) " +
                    "VALUES (@Driva, @Supplier, @NumberPlate, @SupplierPhone)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@Driva", fuel.Driver);
                cmd.Parameters.AddWithValue("@Supplier", fuel.Supplier);
                cmd.Parameters.AddWithValue("@NumberPlate", fuel.NumberPlate);
                cmd.Parameters.AddWithValue("@SupplierPhone", fuel.SupplierPhone);

                cmd.ExecuteNonQuery();
                mcon.Close();
                await Shell.Current.DisplayAlert("Success!!!", "Supplier Added", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        
        private async void upDateVId()
        {
            DataTable dt6 = new();
            IsBusy = true;
            try
            {
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select id from Refuel ORDER BY id ASC LIMIT 1";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt6.Clear();
                adapter.Fill(dt6);
                if (dt6.Rows.Count != 0)
                {
                    VehicleId = Convert.ToInt32(dt6.Rows[0]["id"].ToString()) + 1;
                }

            }
            catch (Exception ex)
            {
                mcon.Close();
                await Shell.Current.DisplayAlert("Database Connection Error", ex.Message, "Continue");
            }
            finally
            {
                IsBusy = false;
            }
        }
        async Task ExecuteLoadRefuelCommand()
        {
            IsBusy = true;
            try
            {
                ReFuel.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from FuelSupply";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                List<RecieveFuelModel> recieveFuel;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    recieveFuel = new()
                    {
                        new RecieveFuelModel
                        {
                            Code = Convert.ToInt32(dt.Rows[i]["id"].ToString()), Driver = dt.Rows[i]["Driva"].ToString(),
                            Supplier = dt.Rows[i]["Supplier"].ToString(), SupplierPhone = Convert.ToInt64(dt.Rows[i]["SupplierPhone"].ToString()),
                            SuppliedLitters = Convert.ToDecimal(dt.Rows[i]["SuppliedLitters"].ToString()), SupplyDate= Convert.ToDateTime(dt.Rows[i]["SupplyDate"].ToString()),
                            SupplyTime = dt.Rows[i]["SupplyTime"].ToString(), VehicleId = Convert.ToInt32(dt.Rows[i]["VehicleId"].ToString()),
                            NumberPlate = dt.Rows[i]["NumberPlate"].ToString(), FuelType = dt.Rows[i]["FuelType"].ToString()
                        }
                    };
                    if (i < dt.Rows.Count)
                    {
                        foreach (var item in recieveFuel)
                        {
                            ReFuel.Add(item);
                        }
                    }
                }
                mcon.Close();
                if (dt.Rows.Count == 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(dt.Rows[i]["FuelType"].ToString().Equals("Diesel"))
                        Diesel += Convert.ToDouble(dt.Rows[i]["SuppliedLitters"].ToString());
                    else if (dt.Rows[i]["FuelType"].ToString().Equals("Super Unleaded"))
                        Superpetrol += Convert.ToDouble(dt.Rows[i]["SuppliedLitters"].ToString());
                    else if (dt.Rows[i]["FuelType"].ToString().Equals("Unleaded"))
                        Petrol += Convert.ToDouble(dt.Rows[i]["SuppliedLitters"].ToString());
                }
            }
            catch (Exception ex)
            {
                mcon.Close();
                await Shell.Current.DisplayAlert("Database Connection Error", ex.Message, "Continue");
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async void LoadFuelPrev()
        {
            IsBusy = true;
            try
            {
                ReFuel.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from FuelSupply";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                List<RecieveFuelModel> recieveFuel;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    recieveFuel = new()
                    {
                        new RecieveFuelModel
                        {
                            Code = Convert.ToInt32(dt.Rows[i]["id"].ToString()), Driver = dt.Rows[i]["Driva"].ToString(),
                            Supplier = dt.Rows[i]["Supplier"].ToString(), SupplierPhone = Convert.ToInt64(dt.Rows[i]["SupplierPhone"].ToString()),
                            SuppliedLitters = Convert.ToDecimal(dt.Rows[i]["SuppliedLitters"].ToString()), SupplyDate= Convert.ToDateTime(dt.Rows[i]["SupplyDate"].ToString()),
                            SupplyTime = dt.Rows[i]["SupplyTime"].ToString(), VehicleId = Convert.ToInt32(dt.Rows[i]["VehicleId"].ToString()),
                            NumberPlate = dt.Rows[i]["NumberPlate"].ToString(), FuelType = dt.Rows[i]["FuelType"].ToString()
                        }
                    };
                    if (i < dt.Rows.Count)
                    {
                        foreach (var item in recieveFuel)
                        {
                            ReFuel.Add(item);
                        }
                    }
                }
                mcon.Close();
                if (dt.Rows.Count == 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["FuelType"].ToString().Equals("Diesel"))
                        Diesel += Convert.ToDouble(dt.Rows[i]["SuppliedLitters"].ToString());
                    else if (dt.Rows[i]["FuelType"].ToString().Equals("Super Unleaded"))
                        Superpetrol += Convert.ToDouble(dt.Rows[i]["SuppliedLitters"].ToString());
                    else if (dt.Rows[i]["FuelType"].ToString().Equals("Unleaded"))
                        Petrol += Convert.ToDouble(dt.Rows[i]["SuppliedLitters"].ToString());
                }
            }
            catch (Exception ex)
            {
                mcon.Close();
                await Shell.Current.DisplayAlert("Database Connection Error", ex.Message, "Continue");
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async void LoadReFuelPrev()
        {
            IsBusy = true;
            try
            {
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Refuel order by id ASC Limit 20";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                List<FuelLorr> recieveFuel;

                recieveFuel1.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    recieveFuel = new()
                    {
                        new FuelLorr
                        {
                            Code = Convert.ToInt32(dt.Rows[i]["id"].ToString()), Driver = dt.Rows[i]["Driver"].ToString(),
                            Refuelier = dt.Rows[i]["Refuelier"].ToString(), Milage = Convert.ToDouble(dt.Rows[i]["Milage"].ToString()),
                            RefueliedLitters = Convert.ToDecimal(dt.Rows[i]["RefueliedLitters"].ToString()), RefuelDate= Convert.ToDateTime(dt.Rows[i]["RefuelDate"].ToString()),
                            RefuelTime = dt.Rows[i]["RefuelTime"].ToString(), VehicleId = Convert.ToInt32(dt.Rows[i]["VehicleId"].ToString()),
                            NumberPlate = dt.Rows[i]["NumberPlate"].ToString(), FuelType = dt.Rows[i]["FuelType"].ToString(),
                            PrevMilage = Convert.ToDouble(dt.Rows[i]["PrevMilage"].ToString())
                        }
                    };
                    if (i < dt.Rows.Count)
                    {
                        foreach (var item in recieveFuel)
                        {
                            Fuels.Add(item);
                        }
                    }
                }
                mcon.Close();
                if (dt.Rows.Count == 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["FuelType"].ToString().Equals("Diesel"))
                        Diesel -= Convert.ToDouble(dt.Rows[i]["RefueliedLitters"].ToString());
                    else if (dt.Rows[i]["FuelType"].ToString().Equals("Super Unleaded"))
                        Superpetrol -= Convert.ToDouble(dt.Rows[i]["RefueliedLitters"].ToString());
                    else if (dt.Rows[i]["FuelType"].ToString().Equals("Unleaded"))
                        Petrol -= Convert.ToDouble(dt.Rows[i]["RefueliedLitters"].ToString());
                }
                MyFuels = Diesel + Superpetrol + Petrol;
            }
            catch (Exception ex)
            {
                mcon.Close();
                await Shell.Current.DisplayAlert("Database Connection Error", ex.Message, "Continue");
            }
            finally
            {
                IsBusy = false;
            }
        }

        //private int code;

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Fuels.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from Refuel order by id ASC Limit 20";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                List<FuelLorr> recieveFuel;

                recieveFuel1.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    recieveFuel = new()
                    {
                        new FuelLorr
                        {
                            Code = Convert.ToInt32(dt.Rows[i]["id"].ToString()), Driver = dt.Rows[i]["Driver"].ToString(),
                            Refuelier = dt.Rows[i]["Refuelier"].ToString(), Milage = Convert.ToDouble(dt.Rows[i]["Milage"].ToString()),
                            RefueliedLitters = Convert.ToDecimal(dt.Rows[i]["RefueliedLitters"].ToString()), RefuelDate= Convert.ToDateTime(dt.Rows[i]["RefuelDate"].ToString()),
                            RefuelTime = dt.Rows[i]["RefuelTime"].ToString(), VehicleId = Convert.ToInt32(dt.Rows[i]["VehicleId"].ToString()),
                            NumberPlate = dt.Rows[i]["NumberPlate"].ToString(), FuelType = dt.Rows[i]["FuelType"].ToString(),
                            PrevMilage = Convert.ToDouble(dt.Rows[i]["PrevMilage"].ToString())
                        }
                    };
                    if (i < dt.Rows.Count)
                    {
                        foreach (var item in recieveFuel)
                        {
                            Fuels.Add(item);
                        }
                    }
                }
                mcon.Close();
                if (dt.Rows.Count == 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["FuelType"].ToString().Equals("Diesel"))
                        Diesel -= Convert.ToDouble(dt.Rows[i]["RefueliedLitters"].ToString());
                    else if (dt.Rows[i]["FuelType"].ToString().Equals("Super Unleaded"))
                        Superpetrol -= Convert.ToDouble(dt.Rows[i]["RefueliedLitters"].ToString());
                    else if (dt.Rows[i]["FuelType"].ToString().Equals("Unleaded"))
                        Petrol -= Convert.ToDouble(dt.Rows[i]["RefueliedLitters"].ToString());
                }
                MyFuels = Diesel + Superpetrol + Petrol;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            //SelectedItem = null;
        }
        public ObservableCollection<RecieveFuelModel> recieveFuel1 { get; }
        public Command SaveCommand1 { get; }
        public Command CancelCommand1 { get; }
        //public int Code { get => code; set => code = value; }
        public int VehicleId { get => vehicleId; set => vehicleId = value; }
        public string Driver { get => driver; set => driver = value; }
        public long SupplierPhone { get => supplierPhone; set => supplierPhone = value; }
        public string NumberPlate { get => numberPlate; set => numberPlate = value; }
        public string Supplier { get => supplier; set => supplier = value; }
        public decimal SuppliedLitters { get => suppliedLitters; set => suppliedLitters = value; }
        public DateTime SupplyDate { get => supplyDate; set => supplyDate = value; }
        public string SupplyTime { get => supplyTime; set => supplyTime = value; }
        public string FuelType { get => fuelType; set => fuelType = value; }
        public int Code { get => code; set => code = value; }
        public decimal RefueliedLitters { get => refueliedLitters; set => refueliedLitters = value; }
        public DateTime RefuelDate { get => refuelDate; set => refuelDate = value; }
        public string RefuelTime { get => refuelTime; set => refuelTime = value; }
        public double Milage { get => milage; set => milage = value; }
        public double PrevMilage { get => prevMilage; set => prevMilage = value; }
        public string Refuelier { get => refuelier; set => refuelier = value; }
        public double Diesel { get => diesel; set => diesel = value; }
        public double Petrol { get => petrol; set => petrol = value; }
        public double Superpetrol { get => superpetrol; set => superpetrol = value; }
        public double MyFuels { get => myFuels; set => myFuels = value; }

        //FuelViewModel fuel = new();
        MySqlConnection mcon;
        string connString = "server=localhost;uid=root;pwd=;database=roben;";
        private async void GetSaveNewRefuel()
        {
            IsBusy = true;
            FuelLorr recieveFuel = new()
            {
                Code = 1,
                PrevMilage = PrevMilage,
                Milage = Milage,
                RefueliedLitters = RefueliedLitters,
                Refuelier = Refuelier,
                Driver = Driver,
                RefuelDate = RefuelDate,
                RefuelTime = RefuelTime,
                FuelType = FuelType,
                NumberPlate = NumberPlate,
                VehicleId = VehicleId,
            };
            try
            {
                if(FuelType.Equals("Diesel") && Diesel < double.Parse(RefueliedLitters.ToString()))
                {
                   await Shell.Current.DisplayAlert("Error!!!", "You do not have enough Desel " + Diesel, "Continue");
                    return;
                }
                else if (FuelType.Equals("Super Unleaded") && Superpetrol < double.Parse(RefueliedLitters.ToString()))
                {
                    await Shell.Current.DisplayAlert("Error!!!", "You do not have enough SuperPetrol " + Superpetrol, "Continue");
                    return;
                }
                else if (FuelType.Equals("Unleaded") && Petrol < double.Parse(RefueliedLitters.ToString()))
                {
                    await Shell.Current.DisplayAlert("Error!!!", "You do not have enough Petrol " + Petrol, "Continue");
                    return;
                }
                mcon = new(connString);
                mcon.Open();
                string cmdText = "INSERT INTO Refuel (PrevMilage, Milage, RefueliedLitters," +
                    "Refuelier, Driver, RefuelDate, RefuelTime, VehicleId, NumberPlate, FuelType) " +
                    "VALUES (@PrevMilage, @Milage, @RefueliedLitters," +
                    "@Refuelier,@Driver, @RefuelDate, @RefuelTime, @VehicleId, @NumberPlate, @FuelType)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@PrevMilage", recieveFuel.PrevMilage);
                cmd.Parameters.AddWithValue("@Milage", recieveFuel.Milage);
                cmd.Parameters.AddWithValue("@RefueliedLitters", recieveFuel.RefueliedLitters);
                cmd.Parameters.AddWithValue("@Refuelier", recieveFuel.Refuelier);
                cmd.Parameters.AddWithValue("@Driver", recieveFuel.Driver);
                cmd.Parameters.AddWithValue("@RefuelDate", recieveFuel.RefuelDate);
                cmd.Parameters.AddWithValue("@RefuelTime", recieveFuel.RefuelTime);
                cmd.Parameters.AddWithValue("@VehicleId", recieveFuel.VehicleId);
                cmd.Parameters.AddWithValue("@NumberPlate", recieveFuel.NumberPlate);
                cmd.Parameters.AddWithValue("@FuelType", recieveFuel.FuelType);

                cmd.ExecuteNonQuery();
                mcon.Close();
                LoadFuelPrev();
                await Shell.Current.DisplayAlert("Success!!!", "Vehicle has been Refueled "+ recieveFuel.NumberPlate, "Continue");
            }
            catch (Exception ex)
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                mcon.Close();
                var snackbarOptions = new SnackbarOptions
                {
                    BackgroundColor = Colors.Red,
                    TextColor = Colors.Green,
                    ActionButtonTextColor = Colors.Yellow,
                    CornerRadius = new CornerRadius(10),
                    Font = Microsoft.Maui.Font.SystemFontOfSize(14),
                    ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14),
                    CharacterSpacing = 0.5
                };

                string text = "This is a Snackbar";
                string actionButtonText = "Click Here to Dismiss";
                Action action = async () => await Shell.Current.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
                TimeSpan duration = TimeSpan.FromSeconds(3);

                var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

                await snackbar.Show(cancellationTokenSource.Token);
                Console.WriteLine(ex.StackTrace);
                await Shell.Current.DisplayAlert("Database Connection Error", ex.Message, "Continue");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async void OnSave()
        {
            RecieveFuelModel recieveFuel = new()
            {
                Code = 1,
                Supplier = Supplier,
                SuppliedLitters = SuppliedLitters,
                SupplierPhone = SupplierPhone,
                Driver = Driver,
                SupplyDate = SupplyDate,
                SupplyTime = SupplyTime,
                FuelType = FuelType,
                NumberPlate = NumberPlate,
                VehicleId = VehicleId
            };
            try
            {
                
                mcon = new(connString);
                mcon.Open();
                string cmdText = "INSERT INTO FuelSupply (Driva, Supplier, SupplierPhone," +
                    "SuppliedLitters, SupplyDate, SupplyTime, VehicleId, NumberPlate, FuelType) " +
                    "VALUES (@Driva, @Supplier, @SupplierPhone," +
                    "@SuppliedLitters,@SupplyDate, @SupplyTime, @VehicleId, @NumberPlate, @FuelType)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@Driva", recieveFuel.Driver);
                cmd.Parameters.AddWithValue("@Supplier", recieveFuel.Supplier);
                cmd.Parameters.AddWithValue("@SupplierPhone", recieveFuel.SupplierPhone);
                cmd.Parameters.AddWithValue("@SuppliedLitters", recieveFuel.SuppliedLitters);
                cmd.Parameters.AddWithValue("@SupplyDate", recieveFuel.SupplyDate);
                cmd.Parameters.AddWithValue("@SupplyTime", recieveFuel.SupplyTime);
                cmd.Parameters.AddWithValue("@VehicleId", recieveFuel.VehicleId);
                cmd.Parameters.AddWithValue("@NumberPlate", recieveFuel.NumberPlate);
                cmd.Parameters.AddWithValue("@FuelType", recieveFuel.FuelType);

                cmd.ExecuteNonQuery();
                mcon.Close();
                LoadFuelPrev();
                await Shell.Current.DisplayAlert("Success!!!", "New Fuel Received", "Continue");
            }
            catch (Exception ex)
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                mcon.Close();
                var snackbarOptions = new SnackbarOptions
                {
                    BackgroundColor = Colors.Red,
                    TextColor = Colors.Green,
                    ActionButtonTextColor = Colors.Yellow,
                    CornerRadius = new CornerRadius(10),
                    Font = Microsoft.Maui.Font.SystemFontOfSize(14),
                    ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14),
                    CharacterSpacing = 0.5
                };

                string text = "This is a Snackbar";
                string actionButtonText = "Click Here to Dismiss";
                Action action = async () => await Shell.Current.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
                TimeSpan duration = TimeSpan.FromSeconds(3);

                var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

                await snackbar.Show(cancellationTokenSource.Token);
                Console.WriteLine(ex.StackTrace);
                await Shell.Current.DisplayAlert("Database Connection Error", ex.Message, "Continue");
            }
            
        }
        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(Driver)
                && !string.IsNullOrWhiteSpace(Supplier)
                && !string.IsNullOrWhiteSpace(NumberPlate);
        }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
