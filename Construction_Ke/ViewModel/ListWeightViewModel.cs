using Construction_Ke.Model;
using Construction_Ke.Views.WeightBridge;

using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;
using MySql.Data.MySqlClient;

using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Input;

namespace Construction_Ke.ViewModel
{
    public class ListWeightViewModel : BaseViewModel/*, BasePage<MultiplePopupViewModel>*/
    {
        private SerialPort _SerialPort;
        public bool IsDTR { get; private set; }
        public bool IsRTS { get; private set; }
        private int firstcode = 1;
        private double sWeight;
        private double weight;
        private string driver;
        private long phone;
        private string plate;
        private string material;
        private double amount;
        private double tonageRate;
        private double balanc;
        private double tonage;
        private double totalAmount;
        private double inBank;
        private DateTime dateTime;
        private string time;
        private int ticket;
        public int Ticket { get => ticket; set => ticket = value; }
        public double Weight { get => weight; set => weight = value; }
        public string Driver { get => driver; set => driver = value; }
        public long Phone { get => phone; set => phone = value; }
        public string Plate { get => plate; set => plate = value; }
        public string Material { get => material; set => material = value; }
        public double Amount { get => amount; set => amount = value; }

        //private FirstWeight _selectedItem;
        public Command LoadGetSettings { get; }
        public Command LoadWBOptions { get; }
        public ObservableCollection<FirstWeight> Items { get; }
        public ObservableCollection<FinalReading> ConfirmPay { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<FirstWeight> ItemTapped { get; }
        public Command LoadGetWBReports { get; }
        public Command LoadSaveWB { get; }
        public Command LoadCancelWB { get; }
        public Command LoadTicket { get; }
        public Command SaveFirstWeight { get; }
        public Command LoadWBSales { get; }
        
        //readonly CsharpBindingPopupViewModel csharpBindingPopupViewModel;
        public Command SaveCommand1 { get; }
        public ListWeightViewModel()
        {
            //Title = "Browse";
            LoadSaveWB = new(GetLoadSaveWB);
            LoadCancelWB = new(GetLoadCancelWB);
            LoadWBOptions = new Command(GetLoadWBOptions);
            Items = new();
            ConfirmPay = new();
            upDateTicket();
            LoadWBSales = new(async () => await ExecuteLoadSalesCommand());
            LoadTicket = new(upDateTicket);
            SaveFirstWeight = new(OnSave);
            LoadGetWBReports = new Command(GetWBReports);
            LoadGetSettings = new Command(GetWBSettings);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Time = System.DateTime.Now.ToShortTimeString();
            //SaveCommand1 = new(GetPayPopup);
            //ItemTapped = new(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);

                
        }
        public Command RefreshPorts { get; }
        public Command Close { get; }
        public Command Open { get; }
        DataTable dt1 = new();
        DataTable dt2 = new();
        private async void upDateTicket()
        {
            IsBusy = true;
            try
            {
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select Ticket from FirstWeight ORDER BY Ticket DESC LIMIT 1";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt2.Clear();
                adapter.Fill(dt2);
                if (dt2.Rows.Count != 0)
                {
                    Ticket = Convert.ToInt32(dt2.Rows[0]["Ticket"].ToString()) + 1;
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

        MySqlConnection mcon;
        string connString = "server=localhost;uid=root;pwd=;database=roben;";
        private async void OnSave()
        {
            SecondWeight secondWeight = new()
            {
                FirstWeightCode = Firstcode,
                SWeight = SWeight,
                Ticket = Ticket
            };
            FirstWeight fWeight = new()
            {
                Ticket = Ticket,
                Weight = Weight,
                Driver = Driver,
                Phone = Phone,
                Plate = Plate,
                Material = Material,
                Amount = Amount,
                DateTime = DateTime,
                Time = Time

            };
            try
            {
                mcon = new(connString);
                mcon.Open();
                if (Firstcode == 1)
                {

                    string cmdText = "INSERT INTO FirstWeight (Ticket, Weight, Driver," +
                        "Phone, Plate, Material, Amount, DateTime, Time) " +
                        "VALUES (@Ticket, @Weight, @Driver," +
                        "@Phone,@Plate, @Material, @Amount, @DateTime, @Time)";
                    MySqlCommand cmd = new(cmdText, mcon);
                    //DbInsert insert = new();
                    cmd.Parameters.AddWithValue("@Ticket", fWeight.Ticket);
                    cmd.Parameters.AddWithValue("@Weight", fWeight.Weight);
                    cmd.Parameters.AddWithValue("@Driver", fWeight.Driver);
                    cmd.Parameters.AddWithValue("@Phone", fWeight.Phone);
                    cmd.Parameters.AddWithValue("@Plate", fWeight.Plate);
                    cmd.Parameters.AddWithValue("@Material", fWeight.Material);
                    cmd.Parameters.AddWithValue("@Amount", fWeight.Amount);
                    cmd.Parameters.AddWithValue("@DateTime", fWeight.DateTime);
                    cmd.Parameters.AddWithValue("@Time", fWeight.Time);

                    cmd.ExecuteNonQuery();


                    await Shell.Current.DisplayAlert("Success!!!", "Empty Truck Weight Recorded.", "Continue");
                }
                else if (Firstcode == 2)
                {
                    string cmdText = "INSERT INTO SecondWeight (SWeight, Ticket, FirstWeightCode)" +
                        "VALUES (@SWeight, @Ticket, @FirstWeightCode)";
                    MySqlCommand cmd = new(cmdText, mcon);
                    cmd.Parameters.AddWithValue("@Ticket", secondWeight.Ticket);
                    cmd.Parameters.AddWithValue("@SWeight", secondWeight.SWeight);
                    cmd.Parameters.AddWithValue("@FirstWeightCode", secondWeight.FirstWeightCode);
                    cmd.ExecuteNonQuery();
                    await Shell.Current.DisplayAlert("Success!!!", "Loaded Truck Weight Recorded.", "Continue");
                }
                mcon.Close();
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
                await Shell.Current.DisplayAlert("Ticket Exists.", ex.Message, "Continue");
            }

        }
 

        //end serialport

        private List<FirstWeight> firstWeights;
        
        //public ListWeightViewModel(IDeviceInfo deviceInfo, MultiplePopupViewModel multiplePopupViewModel, PopupSizeConstants popupSizeConstants, CsharpBindingPopupViewModel csharpBindingPopupViewModel)
        //{
        //    this.popupSizeConstants = popupSizeConstants;
        //    this.csharpBindingPopupViewModel = csharpBindingPopupViewModel;
        //}

        private async void GetLoadCancelWB(object obj)
        {
            try
            {


            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }
        private async void GetLoadSaveWB(object obj)
        {
            try
            {


            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }
        private async void GetWBReports(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(WBReports));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }

        private async void GetWBSettings(object obj)
        {
            try
            {

                await Shell.Current.GoToAsync(nameof(WBSettings));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }

        private async void GetLoadWBOptions(object obj)
        {
            try
            {

                await Shell.Current.GoToAsync(nameof(WBOptions));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }
        DataTable dt = new();
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from FirstWeight order by TICKET ASC LIMIT 20";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    firstWeights = new()
                    {
                        new FirstWeight { Code = Convert.ToInt32(dt.Rows[i]["id"].ToString()), Ticket = Convert.ToInt32(dt.Rows[i]["Ticket"].ToString()),
                        Plate = dt.Rows[i]["Plate"].ToString(), Driver = dt.Rows[i]["Driver"].ToString(),
                        Time = dt.Rows[i]["Time"].ToString(), Weight = Convert.ToDouble(dt.Rows[i]["Weight"].ToString()),
                        Phone = Convert.ToInt64(dt.Rows[i]["Phone"].ToString()), Material = dt.Rows[i]["Material"].ToString(),
                        Amount = Convert.ToDouble(dt.Rows[i]["Amount"].ToString())}
                    };
                    if (i < dt.Rows.Count)
                    {
                        foreach (var item in firstWeights)
                        {
                            Items.Add(item);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database Connection Error", ex.Message, "Continue");
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

        //public FirstWeight SelectedItem
        //{
        //    get => _selectedItem;
        //    set
        //    {
        //        SetProperty(ref _selectedItem, value);
        //        OnItemSelected(value);
        //    }
        //}

        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        public string Time { get => time; set => time = value; }
        public double SWeight { get => sWeight; set => sWeight = value; }
        public int Firstcode { get => firstcode; set => firstcode = value; }
        public double TonageRate { get => tonageRate; set => tonageRate = value; }
        public double Balanc { get => balanc; set => balanc = value; }
        public double Tonage { get => tonage; set => tonage = value; }
        public double TotalAmount { get => totalAmount; set => totalAmount = value; }
        public double InBank { get => inBank; set => inBank = value; }

        private void OnAddItem(object obj)
        {
            
        }
        private List<FinalReading> Finals;
        DataTable dt4 = new();
        //async void OnItemSelected(FirstWeight item)
        //{
        //    if (item == null)
        //        return;

        //    await Shell.Current.DisplayAlert("Testing", "good", "OK");
        //    // This will push the ItemDetailPage onto the navigation stack
        //    // await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        //}
        public void Success()
        {



        }
        async Task ExecuteLoadSalesCommand()
        {
            IsBusy = true;

            try
            {
                ConfirmPay.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from wbsaletb";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                dt4.Clear();
                adapter.Fill(dt4);
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dt4.Rows[i]["Deposit"].ToString()) && string.IsNullOrEmpty(dt4.Rows[i]["Balance"].ToString()))
                    {

                    }
                    else
                    {
						Finals = new()
					    {
						    new FinalReading{ Code = Convert.ToInt32(dt4.Rows[i]["id"].ToString()), DateTime = Convert.ToDateTime(dt4.Rows[i]["DateTime"].ToString()),
						    InBank = Convert.ToDouble(dt4.Rows[i]["Deposit"].ToString()), GrossWeight = Convert.ToDouble(dt4.Rows[i]["Weight"].ToString()),
						    NetWeight = 0, TareWeight = Convert.ToDouble(dt4.Rows[i]["SWeight"].ToString()),
						    Ticket = Convert.ToInt32(dt4.Rows[i]["Ticket"].ToString()), TotalAmount = Convert.ToDouble(dt4.Rows[i]["Amount"].ToString()) * (Convert.ToDouble(dt4.Rows[i]["SWeight"].ToString()) - Convert.ToInt32(dt4.Rows[i]["Weight"].ToString()))/1000,
						    Driver = dt4.Rows[i]["Driver"].ToString(), Material = dt4.Rows[i]["Material"].ToString(), Plate = dt4.Rows[i]["Plate"].ToString(),
						    TonageRate = Convert.ToDouble(dt4.Rows[i]["Amount"].ToString()), Phone = Convert.ToInt64(dt4.Rows[i]["Phone"].ToString()),
						    Tonage = (Convert.ToDouble(dt4.Rows[i]["SWeight"].ToString()) - Convert.ToDouble(dt4.Rows[i]["Weight"].ToString()))/1000,
						    Balanc = Convert.ToDouble(dt4.Rows[i]["Balance"].ToString())}
					    };
						if (i < dt4.Rows.Count)
						{
							foreach (var item in Finals)
							{
								ConfirmPay.Add(item);
							}
						}
                    }
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database Connection Error", ex.Message, "Continue");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
