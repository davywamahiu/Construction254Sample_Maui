using System.Data;

using Construction_Ke.ViewModel.FuelViewModels;

using MySql.Data.MySqlClient;
namespace Construction_Ke.Views.Fuelics;

public partial class Refuel : ContentPage
{
    AddNewFuelVM ListWeightView;
    MySqlConnection mcon;
    DataTable dt = new();
    DataTable dt1 = new();
    string connString = "server=localhost;uid=root;pwd=;database=roben;";
    public Refuel()
    {
        InitializeComponent();
        upDateFuel();
        upDateDriver();
        BindingContext = ListWeightView = new AddNewFuelVM();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ListWeightView.OnAppearing();
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
    private async void upDateDriver()
    {
        List<string> strings = new();
        List<string> strings1 = new();
        IsBusy = true;
        try
        {
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
                strings.Add(dt.Rows[i]["FirstName"].ToString()+" "+dt.Rows[i]["Surname"].ToString());
            }
            string cmdText1 = "Select * from Vehicles order by id ASC LIMIT 100";
            MySqlCommand cmd1 = new(cmdText1, mcon);
            MySqlDataAdapter adapter1 = new();
            adapter1.SelectCommand = cmd1;
            dt1.Clear();
            adapter1.Fill(dt1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                strings1.Add(dt1.Rows[i]["Plate"].ToString());
            }
            driver.ItemsSource = strings;
            plate.ItemsSource = strings1;
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

    private void FuelType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (FuelType.SelectedItem.ToString().Equals("Diesel"))
            ainaPetroli.Text = dieseli.Text;
        if (FuelType.SelectedItem.ToString().Equals("Super Unleaded"))
            ainaPetroli.Text = superpetroli.Text;
        if (FuelType.SelectedItem.ToString().Equals("Unleaded"))
            ainaPetroli.Text = petrili.Text;

    }
}