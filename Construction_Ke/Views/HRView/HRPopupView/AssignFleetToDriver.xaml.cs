using Construction_Ke.ViewModel.EmployeeViewModel;
using System.Data;
using Construction_Ke.Model;
using MySql.Data.MySqlClient;
namespace Construction_Ke.Views.HRView.HRPopupView;

public partial class AssignFleetToDriver : CommunityToolkit.Maui.Views.Popup
{
    MySqlConnection mcon;
    string connString = "server=localhost;uid=root;pwd=;database=roben;";
    DataTable dt = new();DataTable dt1 = new();DataTable dt3 = new();
    public AssignFleetToDriver()
	{
		InitializeComponent();
		CanBeDismissedByTappingOutsideOfPopup = false;
		Size = new(500, 500.5);
		BindingContext = new NewEmpViewModel();
        epin.Text = "PIN";
        natid.Text = "National ID";
        phone.Text = "Contact";
        UpdateDriverNam();
    }
    List<string> Perma = new();List<string> Perma1 = new();
    List<string> Statuss = new(); List<string> Projectoz = new();
    private async void UpdateDriverNam()
    {
        try
        {
            mcon = new(connString);
            mcon.Open();
            string cmdText = "Select * from Employee order by id DESC LIMIT 100";
            MySqlCommand cmd = new(cmdText, mcon);
            MySqlDataAdapter adapter = new();
            adapter.SelectCommand = cmd;
            dt.Clear();
            
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Myname = dt.Rows[i]["Phone"].ToString();
                if (i < dt.Rows.Count)
                    Perma.Add(Myname);
            }
            ename.ItemsSource = Perma;
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
            ename.ItemsSource = Perma;
            plate.ItemsSource = Perma1;

            Statuss.Add("Active");
            Statuss.Add("Inactive");
            Statuss.Add("Deactivated");
            estatus.ItemsSource = Statuss;

            string cmdText3 = "Select * from Contracts order by id DESC LIMIT 100";
            MySqlCommand cmd3 = new(cmdText3, mcon);
            MySqlDataAdapter adapter3 = new();
            adapter3.SelectCommand = cmd3;
            dt3.Clear();

            adapter3.Fill(dt3);
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                string MyProfe = dt3.Rows[i]["ProjectName"].ToString();
                if (i < dt3.Rows.Count)
                    Projectoz.Add(MyProfe);
            }
            Projectoz.Add("Others");
            projects.ItemsSource = Projectoz;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
        }
    }
    private void Button_Clicked(object sender, EventArgs e)
	{
		Close();
	}

    private void ename_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (e == null)
            return;
        string myPhone = (string)ename.SelectedItem;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["Phone"].ToString() == myPhone)
            {
                epin.Text = dt.Rows[i]["EPin"].ToString();
                natid.Text = dt.Rows[i]["NatID"].ToString();
                phone.Text = myPhone;
                fname.Text = dt.Rows[i]["FirstName"].ToString();
                sname.Text = dt.Rows[i]["Surname"].ToString();
            }
        }
        //int num = 
    }
}