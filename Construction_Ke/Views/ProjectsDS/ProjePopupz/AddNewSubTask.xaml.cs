using CommunityToolkit.Maui.Views;
using MySql.Data.MySqlClient;
using System.Data;
using Construction_Ke.ViewModel.ProjectsVM;

namespace Construction_Ke.Views.ProjectsDS.ProjePopupz;

public partial class AddNewSubTask : Popup
{
	DataTable dt = new();
    MySqlConnection mcon;
    string connString = "server=localhost;uid=root;pwd=;database=roben;";
    public List<string> AddTask { get; set; } = new();
    public AddNewSubTask()
	{
		InitializeComponent();
		BindingContext = new ContractViewModel();
		Size = new(450.5, 500.3);
		CanBeDismissedByTappingOutsideOfPopup = false;
		UpdateSubTs();
	}

	private void UpdateSubTs()
	{
        List<string> strings = new();
        strings.Add("Choose best Borrow area.");
        strings.Add("Dig up 25*25*1.5 meter pits.");
        strings.Add("Collect Samples for testing for MDD, FSQ, LL, PI, OMC, CBR.");
        strings.Add("Prepare TOE Line");
        strings.Add("Road Side vegetation clearance. Girth of 300m.");
        strings.Add("Clear and Grub to a dept of 150mm");
        strings.Add("Prepare checklist on vegetation removed.");
        strings.Add("Prepare TOE Line of the EMBARKMENT.");
        strings.Add("Level original ground, mix with water and compact to 95%");
        strings.Add("Bring material from borrow and spread.");
        strings.Add("construct embarkment having slope.");
        strings.Add("Compact with steel drums");
        actTask.ItemsSource = strings;
        mcon = new(connString);
        mcon.Open();
        string cmdText = "Select * from Taskas ORDER BY id ASC";
        MySqlCommand cmd = new(cmdText, mcon);
        MySqlDataAdapter adapter = new();
        adapter.SelectCommand = cmd;
        dt.Clear();

        adapter.Fill(dt);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string Myname = dt.Rows[i]["Activitiz"].ToString();
            if (i < dt.Rows.Count)
                AddTask.Add(Myname);
        }
        activitz.ItemsSource = AddTask;
    }

	private void Button_Clicked(object sender, EventArgs e)
	{
		Close();
	}

    private void activitaz_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (activitaz.IsChecked == true)
            newactivitaz.IsVisible = true;
        else if (activitaz.IsChecked == false)
            newactivitaz.IsVisible = false;
    }
}