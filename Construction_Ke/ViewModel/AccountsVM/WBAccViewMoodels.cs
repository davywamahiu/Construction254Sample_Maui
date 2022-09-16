using Construction_Ke.Model;

using MySql.Data.MySqlClient;

using System.Collections.ObjectModel;
using System.Data;

namespace Construction_Ke.ViewModel.AccountsVM
{
    public class WBAccViewMoodels : BaseViewModel
    {
        private string driver;
        private long phone;
        private string plate;
        private string accountNo;
        private string mpesaUid;
        private double deposit;
        private double spentAmount;
        private double balance;
        private string paidOn;
        private string material;
        private double tonage;
        private int customer;
        private double payment;
        private int newCustomers;
        private double inBank;
        public WBAccViewMoodels()
        {
            WBCustomerDepo = new();
            SaveBridgePayment = new(GetSaveBridgePayment);
            LoadWBPayments = new(async () => await ExecuteLoadItemsCommand());
            PaidOn = DateTime.Now.ToShortDateString();
            UpdateCusParameters();
        }

        private async void UpdateCusParameters()
        {
            try
            {
                WBCustomerDepo.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from wbcustomerdeposit";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;

                dt.Clear();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    WBdeposit = new()
                    {
                        new WBCustomerDeposit { AccountNo = dt.Rows[i]["AccountNo"].ToString(), Balance = Convert.ToDouble(dt.Rows[i]["Balance"].ToString()),
                        Code = Convert.ToInt32(dt.Rows[i]["id"].ToString()), Deposit = Convert.ToDouble(dt.Rows[i]["Deposit"].ToString()),
                        Driver =  dt.Rows[i]["Driva"].ToString(), MpesaUid =  dt.Rows[i]["MpesaUid"].ToString(),
                        Phone = Convert.ToInt64(dt.Rows[i]["Phone"].ToString()), Plate = dt.Rows[i]["Plate"].ToString(),
                        SpentAmount = Convert.ToDouble(dt.Rows[i]["SpentAmount"].ToString()), PaidOn = dt.Rows[i]["PaidOn"].ToString(),
                        Material = dt.Rows[i]["Material"].ToString(), Tonage = Convert.ToDouble(dt.Rows[i]["Tonage"].ToString())
                        }
                    };
                }
                Customer = dt.Rows.Count;
                if (dt.Rows.Count != 0)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Payment += Convert.ToDouble(dt.Rows[i]["Deposit"].ToString());
                        InBank += Convert.ToDouble(dt.Rows[i]["Deposit"].ToString());
                        if (dt.Rows[i]["PaidOn"].ToString().Equals(DateTime.Now.ToShortDateString()))
                        {
                            NewCustomers++;
                        }
                    }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database Connection Error", ex.Message, "Continue");
            }
        }

        List<WBCustomerDeposit> WBdeposit;
        DataTable dt = new();
        MySqlConnection mcon;
        string connString = "server=localhost;uid=root;pwd=;database=roben;";
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                WBCustomerDepo.Clear();
                mcon = new(connString);
                mcon.Open();
                string cmdText = "Select * from wbcustomerdeposit";
                MySqlCommand cmd = new(cmdText, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                
                dt.Clear();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    WBdeposit = new()
                    {
                        new WBCustomerDeposit { AccountNo = dt.Rows[i]["AccountNo"].ToString(), Balance = Convert.ToDouble(dt.Rows[i]["Balance"].ToString()),
                        Code = Convert.ToInt32(dt.Rows[i]["id"].ToString()), Deposit = Convert.ToDouble(dt.Rows[i]["Deposit"].ToString()),
                        Driver =  dt.Rows[i]["Driva"].ToString(), MpesaUid =  dt.Rows[i]["MpesaUid"].ToString(),
                        Phone = Convert.ToInt64(dt.Rows[i]["Phone"].ToString()), Plate = dt.Rows[i]["Plate"].ToString(),
                        SpentAmount = Convert.ToDouble(dt.Rows[i]["SpentAmount"].ToString()), PaidOn = dt.Rows[i]["PaidOn"].ToString(), 
                        Material = dt.Rows[i]["Material"].ToString(), Tonage = Convert.ToDouble(dt.Rows[i]["Tonage"].ToString())
                        }
                    };
                    if (i < dt.Rows.Count)
                    {
                        foreach (var item in WBdeposit)
                        {
                            WBCustomerDepo.Add(item);
                        }
                    }
                }
                Customer = dt.Rows.Count;
                if(dt.Rows.Count != 0)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Payment += Convert.ToDouble(dt.Rows[i]["Deposit"].ToString());
                        InBank += Convert.ToDouble(dt.Rows[i]["Deposit"].ToString());
                        if (dt.Rows[i]["PaidOn"].ToString().Equals(DateTime.Now.ToShortDateString()))
                        {
                            NewCustomers++;
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
        private async void GetSaveBridgePayment(object obj)
        {
            WBCustomerDeposit wBCustomer = new()
            {
                AccountNo = AccountNo,
                Balance = 0,
                Deposit =Deposit,
                Driver = Driver,
                MpesaUid = MpesaUid,
                Phone = Phone,
                SpentAmount = 0,
                Plate = Plate,
                PaidOn = PaidOn,
                Material= Material, 
                Tonage = Tonage
            };
            try
            {
                mcon = new(connString);
                mcon.Open();
                string cmdText = "INSERT INTO wbcustomerdeposit (AccountNo, Balance, Deposit," +
                        "Driva, MpesaUid, Phone, SpentAmount, Plate, PaidOn, Material, Tonage) " +
                        "VALUES (@AccountNo, @Balance, @Deposit," +
                        "@Driver,@MpesaUid, @Phone, @SpentAmount, @Plate, @PaidOn," +
                        "@Material, @Tonage)";
                MySqlCommand cmd = new(cmdText, mcon);
                //DbInsert insert = new();
                cmd.Parameters.AddWithValue("@AccountNo", wBCustomer.AccountNo);
                cmd.Parameters.AddWithValue("@Balance", wBCustomer.Balance);
                cmd.Parameters.AddWithValue("@Deposit", wBCustomer.Deposit);
                cmd.Parameters.AddWithValue("@Driver", wBCustomer.Driver);
                cmd.Parameters.AddWithValue("@MpesaUid", wBCustomer.MpesaUid);
                cmd.Parameters.AddWithValue("@Phone", wBCustomer.Phone);
                cmd.Parameters.AddWithValue("@SpentAmount", wBCustomer.SpentAmount);
                cmd.Parameters.AddWithValue("@Plate", wBCustomer.Plate);
                cmd.Parameters.AddWithValue("@PaidOn", wBCustomer.PaidOn);
                cmd.Parameters.AddWithValue("@Material", wBCustomer.Material);
                cmd.Parameters.AddWithValue("@Tonage", wBCustomer.Tonage);
                cmd.ExecuteNonQuery();


                await Shell.Current.DisplayAlert("Success!!!", "Customer Deposit Has Been added.", "Continue");
            }
            catch (Exception ex)
            {
                mcon.Close();
                await Shell.Current.DisplayAlert("Failed To Save Payment", ex.Message, "Continue");
            }
        }
        public void OnAppearing()
        {
            IsBusy = true;
            //SelectedItem = null;
        }
        public Command SaveBridgePayment { get; }
        public Command LoadWBPayments { get; }
        public ObservableCollection<WBCustomerDeposit> WBCustomerDepo { get; }
        public string Driver { get => driver; set => driver = value; }
        public long Phone { get => phone; set => phone = value; }
        public string Plate { get => plate; set => plate = value; }
        public string AccountNo { get => accountNo; set => accountNo = value; }
        public string MpesaUid { get => mpesaUid; set => mpesaUid = value; }
        public double Deposit { get => deposit; set => deposit = value; }
        public double SpentAmount { get => spentAmount; set => spentAmount = value; }
        public double Balance { get => balance; set => balance = value; }
        public string PaidOn { get => paidOn; set => paidOn = value; }
        public int Customer { get => customer; set => customer = value; }
        public double Payment { get => payment; set => payment = value; }
        public int NewCustomers { get => newCustomers; set => newCustomers = value; }
        public double InBank { get => inBank; set => inBank = value; }
        public string Material { get => material; set => material = value; }
        public double Tonage { get => tonage; set => tonage = value; }
    }
}
