using Construction_Ke.Views.HRView.HRPopupView;
using CommunityToolkit.Maui.Views;
using System.Data;
using Construction_Ke.Model;
using MySql.Data.MySqlClient;
using Construction_Ke.ViewModel.EmployeeViewModel;
namespace Construction_Ke.Views.HRView;

public partial class FleetManagerView : ContentPage
{
    NewEmpViewModel ListWeightView;
    MySqlConnection mcon;
    string connString = "server=localhost;uid=root;pwd=;database=roben;";
    DataTable dt = new(); DataTable dt1 = new();
    public FleetManagerView()
	{
		InitializeComponent();
		BindingContext = ListWeightView = new NewEmpViewModel();
		epin.Text = "PIN";
		natid.Text = "National ID";
		phone.Text = "Contact";
        UpdateDriverNam();
        btnUpdate.IsEnabled = false;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ListWeightView.OnAppearing();
    }
    List<string> Perma = new(); List<string> Perma1 = new();
    List<string> Statuss = new(); List<string> Projectoz = new(); List<string> Asss = new();
    private async void UpdateDriverNam()
    {
        try
        {
            mcon = new(connString);
            mcon.Open();
            //vehicles
            string cmdText1 = "Select * from Vehicles order by id DESC LIMIT 100";
            MySqlCommand cmd1 = new(cmdText1, mcon);
            MySqlDataAdapter adapter1 = new();
            adapter1.SelectCommand = cmd1;
            dt1.Clear();

            adapter1.Fill(dt1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string MyProfe = dt1.Rows[i]["Plate"].ToString();
                if (i < dt1.Rows.Count)
                    Perma1.Add(MyProfe);
            }
            plate.ItemsSource = Perma1;

            Statuss.Add("Active");
            Statuss.Add("Inactive");
            Statuss.Add("Deactivated");
            estatus.ItemsSource = Statuss;

            string cmdText = "Select * from Contracts order by id DESC LIMIT 100";
            MySqlCommand cmd = new(cmdText, mcon);
            MySqlDataAdapter adapter = new();
            adapter.SelectCommand = cmd;
            dt.Clear();

            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string MyProfe = dt.Rows[i]["ProjectName"].ToString();
                if (i < dt.Rows.Count)
                    Projectoz.Add(MyProfe);
            }
            Projectoz.Add("Others");
            projectz.ItemsSource = Projectoz;

            Asss.Add("Foreman");
            Asss.Add("Managers");
            Asss.Add("Accounts");
            Asss.Add("Lab");
            Asss.Add("Engineer");
            Asss.Add("Resident Engineer");
            Asss.Add("Survey");
            Asss.Add("Others");
            asss.ItemsSource = Asss;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
        }
    }
    private void Button_Clicked(object sender, EventArgs e)
	{
		AssignFleetToDriver assignFleet = new();
		this.ShowPopup(assignFleet);
	}

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var previous = e.PreviousSelection;
        var current = e.CurrentSelection;
        //string tikiti = e.
        try
        {
            if (current != null)
                UpdateSelectionData(e.PreviousSelection, e.CurrentSelection);
            btnUpdate.IsEnabled = true;

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Item Slection Error", ex.Message, "OK");
        }
    }
    private async void UpdateSelectionData(IReadOnlyList<object> previousSelection, IReadOnlyList<object> currentSelection)
    {
        var selectedContact = currentSelection.FirstOrDefault() as FleetAssigned;
        try
        {
            if (selectedContact == null)
                return;
            txtDriver.Text = selectedContact.FullName;
            epin.Text = selectedContact.EPin;
            natid.Text = selectedContact.NatID.ToString();
            vtype.Text = selectedContact.VehiclType;
            try
            {
                mcon = new(connString);
                mcon.Open();
                //vehicles
                string cmdText1 = "Select Phone from employee where Natid='"+ selectedContact.NatID + "' order by id DESC LIMIT 100";
                MySqlCommand cmd1 = new(cmdText1, mcon);
                MySqlDataAdapter adapter1 = new();
                adapter1.SelectCommand = cmd1;
                dt.Clear();

                adapter1.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    phone.Text = dt.Rows[i]["Phone"].ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Test", ex.Message, "OK");
        }
    }
}