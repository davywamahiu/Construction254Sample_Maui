using CommunityToolkit.Maui.Views;
using MySql.Data.MySqlClient;
using System.Data;
using Construction_Ke.ViewModel.ProjectsVM;
namespace Construction_Ke.Views.ProjectsDS.ProjePopupz;

public partial class AddBogLabor : Popup
{
    List<string> strings = new();
	public AddBogLabor()
	{
		InitializeComponent();
		BindingContext = new ContractViewModel();
        Size = new(900.4, 500.5);
        CanBeDismissedByTappingOutsideOfPopup = false;
		UpdateBoqLabo();
    }
    DataTable dt1 = new();
    MySqlConnection mcon;
    string connString = "server=localhost;uid=root;pwd=;database=roben;";
    void UpdateBoqLabo()
	{
        mcon = new(connString);
        mcon.Open();
        string cmdText = "Select * from Casuals order by id ASC LIMIT 100";
        MySqlCommand cmd = new(cmdText, mcon);
        MySqlDataAdapter adapter = new();
        adapter.SelectCommand = cmd;
        dt1.Clear();
        adapter.Fill(dt1);
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            strings.Add(dt1.Rows[i]["FirstName"].ToString() + " " + dt1.Rows[i]["MiddleName"].ToString() + " " + dt1.Rows[i]["Surname"].ToString());
        }
        labor.ItemsSource = strings;
    }

    private void Button_Clicked(object sender, EventArgs e)
	{
		Close();
	}

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        laborors.Text = "";
    }

    private void labor_SelectedIndexChanged(object sender, EventArgs e)
    {
        laborors.Text += labor.SelectedItem.ToString();
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if(btnlabour.IsChecked == true)
            txtlabour.IsVisible = true;
        else if(btnlabour.IsChecked == false)
            txtlabour.IsVisible = false;
    }
}