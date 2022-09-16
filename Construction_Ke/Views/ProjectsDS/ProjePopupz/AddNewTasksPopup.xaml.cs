using Construction_Ke.ViewModel.ProjectsVM;
using CommunityToolkit.Maui.Views;
using System.Data;
using MySql.Data.MySqlClient;
namespace Construction_Ke.Views.ProjectsDS.ProjePopupz;

public partial class AddNewTasksPopup : Popup
{
	public AddNewTasksPopup()
	{
		InitializeComponent();
		BindingContext = new ContractViewModel();
		Size = new(430.7, 500.4);
		CanBeDismissedByTappingOutsideOfPopup = false;
		UpdateActivd();
	}
    MySqlConnection mcon;
    string connString = "server=localhost;uid=root;pwd=;database=roben;";
    DataTable dt = new();
    List<string> Perma = new();
    private void UpdateActivd()
	{
		List<string> strings = new();
		strings.Add("Site Preparation.");
		strings.Add("Cleaning and gravelling.");
		strings.Add("Overall Ground Level.");
		strings.Add("Sub Gravel");
		strings.Add("Granular Sub-Base.");
		strings.Add("Wet-Mix Macadam");
		strings.Add("Dense-Mix Macadam.");
		strings.Add("Bitumen Concrete Mix.");
		activitz.ItemsSource = strings;

        mcon = new(connString);
        mcon.Open();
        string cmdText = "Select * from Contracts ORDER BY id ASC LIMIT 100";
        MySqlCommand cmd = new(cmdText, mcon);
        MySqlDataAdapter adapter = new();
        adapter.SelectCommand = cmd;
        dt.Clear();

        adapter.Fill(dt);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string Myname = dt.Rows[i]["ProjectName"].ToString();
            if (i < dt.Rows.Count)
                Perma.Add(Myname);
        }
		acproj.ItemsSource = Perma;
    }

	private void Button_Clicked(object sender, EventArgs e)
	{
		Close();
	}

	private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (activitaz.IsChecked == true)
			newactivitaz.IsVisible = true;
		else if(activitaz.IsChecked==false)
			newactivitaz.IsVisible = false;
	}
}