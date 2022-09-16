using Construction_Ke.Model;
using System.Data;
using CommunityToolkit.Maui.Views;
using Construction_Ke.ViewModel.FuelViewModels;

using MySql.Data.MySqlClient;
using Construction_Ke.Views.Fuelics.SupplierPop;

namespace Construction_Ke.Views.Fuelics;

public partial class ReceiveFuel : ContentPage
{
    AddNewFuelVM ListWeightView;
    MySqlConnection mcon;
    DataTable dt = new();
    DataTable dt1 = new();
    string connString = "server=localhost;uid=root;pwd=;database=roben;";
    public RecieveFuelModel recieveFuel { get; set; }
    public ReceiveFuel()
    {
        InitializeComponent();
        BindingContext = ListWeightView = new AddNewFuelVM();
        upDateFuel();
        upDateDriver();
    }
    private async void upDateDriver()
    {
        List<string> strings = new();
        List<string> strings1 = new();
        List<string> strings2 = new();
        IsBusy = true;
        try
        {
            string cmdText1 = "Select * from mysuppliers order by id ASC LIMIT 100";
            MySqlCommand cmd1 = new(cmdText1, mcon);
            MySqlDataAdapter adapter1 = new();
            adapter1.SelectCommand = cmd1;
            dt.Clear();
            adapter1.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strings.Add(dt.Rows[i]["Driver"].ToString());
                strings1.Add(dt.Rows[i]["Plate"].ToString());
                strings2.Add(dt.Rows[i]["Supplier"].ToString());
            }
            driver.ItemsSource = strings;
            plate.ItemsSource = strings1;
            supplier.ItemsSource = strings2;
            mcon.Close();
        }
        catch (Exception ex)
        {
            mcon.Close();
            await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
        }
        finally
        {
            IsBusy = false;
        }
    }
    private async void upDateFuel()
    {
        List<string> strings = new();
        IsBusy = true;
        try
        {
            mcon = new(connString);
            mcon.Open();
            string cmdText = "Select * from Fuel";
            MySqlCommand cmd = new(cmdText, mcon);
            MySqlDataAdapter adapter = new();
            adapter.SelectCommand = cmd;
            adapter.Fill(dt1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                strings.Add(dt1.Rows[i]["Fuel"].ToString());
            }
            FuelType.ItemsSource = strings;
            mcon.Close();
        }
        catch (Exception ex)
        {
            mcon.Close();
            await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
        }
        finally
        {
            IsBusy = false;
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ListWeightView.OnAppearing();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        FuelSuppliersPopup fuel = new();
        this.ShowPopup(fuel);
    }

    private void driver_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (driver.SelectedItem == null)
            return;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if(driver.SelectedItem == dt.Rows[i]["Driver"].ToString())
                sphone.Text = dt.Rows[i]["SupplierPhone"].ToString();
        }
    }
}