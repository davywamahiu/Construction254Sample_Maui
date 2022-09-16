using Construction_Ke.Model;
using Construction_Ke.ViewModel;
using Construction_Ke.Views.WeightBridge;
using CommunityToolkit.Maui.Views;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO.Ports;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Maui.Markup;
using System.Text;

namespace Construction_Ke.Views;

public partial class Weighbridge : ContentPage
{
    MySqlConnection mcon;
    DataTable dt = new();
    DataTable dt1 = new();
    string connString = "server=localhost;uid=root;pwd=;database=roben;";
    ListWeightViewModel ListWeightView;
    public Weighbridge()
    {
        InitializeComponent();
        //BindingContext = new FirstWeightViewModel();
        SaveLoads.IsEnabled = false;
        upDateDriver();
        upDateMaterial();
        
        grb.IsChecked = true;
        btnGross.IsEnabled = true;
        btnTare.IsEnabled = false;
        BindingContext = ListWeightView = new ListWeightViewModel();
        //SerialPortSign1();
    }

    private async void SerialPortSign1()
    {
        await SerialPortProgram();
    }

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
                int Ticket = Convert.ToInt32(dt2.Rows[0]["Ticket"].ToString()) + 1;
                tickets.Text = Ticket.ToString();
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

    private async void upDateMaterial()
    {
        List<string> strings = new();
        IsBusy = true;
        try
        {
            mcon = new(connString);
            mcon.Open();
            string cmdText = "Select * from commodities";
            MySqlCommand cmd = new(cmdText, mcon);
            MySqlDataAdapter adapter = new();
            adapter.SelectCommand = cmd;
            dt1.Clear();
            adapter.Fill(dt1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                strings.Add(dt1.Rows[i]["materials"].ToString());
            }
            material.ItemsSource = strings;
            mcon.Close();
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

    private async void upDateDriver()
    {
        List<string> strings = new();
        IsBusy = true;
        try
        {
            mcon = new(connString);
            mcon.Open();
            string cmdText = "Select * from Drivers";
            MySqlCommand cmd = new(cmdText, mcon);
            MySqlDataAdapter adapter = new();
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strings.Add(dt.Rows[i]["Driva"].ToString());
            }
            driver.ItemsSource = strings;
            mcon.Close();
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
    
#pragma warning disable CA1416 // Validate platform compatibility
    private SerialPort port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
#pragma warning restore CA1416 // Validate platform compatibility

    async Task SerialPortProgram()
    {
        IsBusy = true;
        try
        {
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            port.Open();
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
        }
        finally { IsBusy = false; }
    }

    private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        // Show all the incoming data in the port's buffer
        string str = "";
        try
        {


            str = port.ReadExisting();
            // using (StreamWriter sw = File.AppendText(@"myfile.txt"))
            //{
            //    sw.WriteLine(st);                  
            //}	
            if (str.Length > 6 | (str.Length < 11 & str.Length > 5))
            {
                string[] tokens = str.Split('+');

                if (tokens.Length > 0)
                {

                    Int32 rest = Convert.ToInt32(tokens[1]);


                    reads.Text = rest.ToString();

                }

                tokens = str.Split('-');

                if (tokens.Length > 0)
                {

                    Int32 rest = Convert.ToInt32(tokens[1]);
                    reads.Text = rest.ToString();

                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ListWeightView.OnAppearing();
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            PayWB wB = new();
            this.ShowPopup(wB);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "ok");
        }
        
    }

    private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (Convert.ToInt32(grossw.Text) > 1000)
            {
                Guard.IsNotNull(grossw.Text, nameof(grossw.Text));
                Guard.IsNotNull(plate.Text, nameof(plate.Text));
                SaveLoads.IsEnabled = true;
            }
            else
                SaveLoads.IsEnabled = false;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
        }
        
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        WBSettings wB = new();
        this.ShowPopup(wB);
    }

    private void driver_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (driver == null)
            return;
        int abc = driver.SelectedIndex;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if(i == abc)
            {
                kampuni.Text = dt.Rows[i]["Driva"].ToString();
                phone.Text = dt.Rows[i]["driverPhone"].ToString();
                plate.Text = dt.Rows[i]["plate"].ToString();
            }
        }
    }

    private void material_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (material == null)
            return;
        int abc = material.SelectedIndex;
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            if (i == abc)
            {
                amount.Text = dt1.Rows[i]["MaterialCost"].ToString();
            }
        }
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        readss.Text = "1";
        readin.Text = "Gross";
        SaveLoads.Text = "Save Gross";
    }

    private void RadioButton_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
    {
        readss.Text = "2";
        readin.Text = "Tare";
        SaveLoads.Text = "Save Tare";
        btnGross.IsEnabled = false;
        btnTare.IsEnabled = true;
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        upDateTicket();
        grossw.Text = "0";
        netwei.Text = "0";
        sweigt.Text = "0";
        phone.Text = "";
        kampuni.Text = "";
        amount.Text = "";
        plate.Text = "";
        btnGross.IsEnabled = true;
        upDateDriver();
        upDateMaterial();
    }

    private void Entry_TextChanged_1(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(sweigt.Text))
            return;
        int swe = Convert.ToInt32(sweigt.Text);
        if (swe >= 1000)
            netwei.Text = (swe - Convert.ToInt32(grossw.Text)).ToString();
    }

    private void Button_Clicked_3(object sender, EventArgs e)
    {
        WBOptions wB = new();
        this.ShowPopup(wB);
    }

    private void btnGross_Clicked(object sender, EventArgs e)
    {
        grossw.Text = reads.Text;
        SaveLoads.IsEnabled = true;
    }

    private void btnTare_Clicked(object sender, EventArgs e)
    {
        //sweigt.Text = reads.Text;
        sweigt.Text = "12500";
        SaveLoads.IsEnabled = true;
        
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection;
        if (current != null)
            UpdateSelectionData(e.PreviousSelection, e.CurrentSelection);
    }
    FirstWeight firstWeight;
    private async void UpdateSelectionData(IReadOnlyList<object> previousSelection, IReadOnlyList<object> currentSelection)
    {
        var selectedContact = currentSelection.FirstOrDefault() as FirstWeight;
        if (selectedContact != null)
            firstWeight = new()
            {
                Amount = selectedContact.Amount,
                Driver = selectedContact.Driver,
                Material = selectedContact.Material,
                Phone = selectedContact.Phone,
                Plate = selectedContact.Plate,
                Ticket = selectedContact.Ticket,
                Time = selectedContact.Time,
                Weight = selectedContact.Weight
            };
        else
            return;
        try
        {
            tickets.Text = firstWeight.Ticket.ToString();
            time.Text = firstWeight.Time;
            time.IsEnabled = false;
            phone.Text = firstWeight.Phone.ToString();
            plate.Text = firstWeight.Plate;
            kampuni.Text = firstWeight.Driver;
            amount.Text = firstWeight.Amount.ToString();
            grossw.Text = firstWeight.Weight.ToString();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("First Weight!!!", "Details: " + ex.Message, "Continue");
        }
    }
}