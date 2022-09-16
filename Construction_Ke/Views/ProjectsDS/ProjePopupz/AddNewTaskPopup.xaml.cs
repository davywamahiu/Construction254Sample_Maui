using CommunityToolkit.Maui.Views;
using System.Data;
using MySql.Data.MySqlClient;
using Construction_Ke.ViewModel.ProjectsVM;
namespace Construction_Ke.Views.ProjectsDS.ProjePopupz;

public partial class AddNewTaskPopup : Popup
{
	public AddNewTaskPopup()
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
        strings.Add("Borrow Area Identification.");
        strings.Add("OGL Bed Preparation.");
        strings.Add("Material and Spreading.");
        strings.Add("Water Application");
        strings.Add("Compaction.");
        strings.Add("Material and Spreading");
        strings.Add("Watering.");
        strings.Add("Compaction.");
        strings.Add("Toe Line Marking");
        strings.Add("Hauling and Dumping.");
        strings.Add("Spreading and Grading.");
        strings.Add("Watering");
        strings.Add("Compaction.");
        strings.Add("Preparation of WMM from plant.");
        strings.Add("Spreading/Paving of WMM");
        strings.Add("Compaction");
        strings.Add("Rectification");
        strings.Add("Surface Finish");
        strings.Add("Application of Prime Coat");
        strings.Add("Application of Tack Coat");
        strings.Add("Cleaning of Surface");
        strings.Add("Laying of DBM");
        strings.Add("Mixing and Transporting:");
        strings.Add("Compaction");
        strings.Add("Joints");
        strings.Add("Surface Finish");
        actTask.ItemsSource = strings;
        mcon = new(connString);
        mcon.Open();
        string cmdText = "Select * from Activitiza ORDER BY id ASC";
        MySqlCommand cmd = new(cmdText, mcon);
        MySqlDataAdapter adapter = new();
        adapter.SelectCommand = cmd;
        dt.Clear();

        adapter.Fill(dt);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string Myname = dt.Rows[i]["MyACtivitiz"].ToString() + dt.Rows[i]["ActName"].ToString();
            if (i < dt.Rows.Count)
                Perma.Add(Myname);
        }
        activitz.ItemsSource = Perma;
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