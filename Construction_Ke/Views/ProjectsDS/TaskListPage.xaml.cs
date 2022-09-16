using Construction_Ke.ViewModel.ProjectsVM;
using CommunityToolkit.Maui.Views;
using Construction_Ke.Views.ProjectsDS.ProjePopupz;
using System.Data;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using Construction_Ke.Model;
namespace Construction_Ke.Views.ProjectsDS;

public partial class TaskListPage : ContentPage
{
    ContractViewModel ListWeightView;
    public ObservableCollection<Taskis> Taskiz { get; } = new();
    public ObservableCollection<TaskasList> TaskasLists { get; } = new();
    public TaskListPage()
	{
		InitializeComponent();
        BindingContext = ListWeightView = new ContractViewModel();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ListWeightView.OnAppearing();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        AddNewTasksPopup addNewTasksPopup = new AddNewTasksPopup ();
        this.ShowPopup(addNewTasksPopup);
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        AddNewTaskPopup addNewTaskPopup = new AddNewTaskPopup();
        this.ShowPopup(addNewTaskPopup);
    }
    DataTable dt3 = new();
    DataTable dt = new();
    List<TaskasList> Taskas = new();
    MySqlConnection mcon;
    string connString = "server=localhost;uid=root;pwd=;database=roben;";
    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection;
        if (current != null)
            UpdateSelectionData(e.PreviousSelection, e.CurrentSelection);
    }
    private async void UpdateSelectionData(IReadOnlyList<object> previousSelection, IReadOnlyList<object> currentSelection)
    {
        var selectedContact = currentSelection.FirstOrDefault() as ActAndTask;
        if (selectedContact == null)
            return;
        try
        {
            TaskasLists.Clear();
            mcon = new(connString);
            mcon.Open();
            string cmdText = "Select * from Taskas where ActId ='" + selectedContact.Code + "' ORDER BY id ASC";
            MySqlCommand cmd = new(cmdText, mcon);
            MySqlDataAdapter adapter = new();
            adapter.SelectCommand = cmd;
            dt3.Clear();
            adapter.Fill(dt3);
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                Taskas = new()
                    {
                        new TaskasList
                        {
                            Code=i+1,
                            Activitiz = dt3.Rows[i]["Activitiz"].ToString(),
                            TasCompCreteria = dt3.Rows[i]["TasCompCreteria"].ToString(),
                            TasDependsOn = dt3.Rows[i]["TasDependsOn"].ToString(),
                            TasEndDate = Convert.ToDateTime(dt3.Rows[i]["TasEndDate"].ToString()),
                            TasStartDate = Convert.ToDateTime(dt3.Rows[i]["TasStartDate"].ToString())
                        }
                    };
                if (i < dt3.Rows.Count)
                    foreach (var item in Taskas)
                        TaskasLists.Add(item);
            }
            TaskaList.ItemsSource = TaskasLists;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
        }
    }

    private void TaskaList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection;
        if (current != null)
            UpdateSelectionData1(e.PreviousSelection, e.CurrentSelection);
    }
    List<Taskis> SubTusk = new();
    private async void UpdateSelectionData1(IReadOnlyList<object> previousSelection, IReadOnlyList<object> currentSelection)
    {
        var selectedContact = currentSelection.FirstOrDefault() as TaskasList;
        if (selectedContact == null)
            return;
        try
        {
            Taskiz.Clear();
            mcon = new(connString);// where ActId='" + item + "' 
            mcon.Open();
            string cmdText1 = "Select id from Taskas where Activitiz='" + selectedContact.Activitiz + "' ORDER BY id ASC";
            MySqlCommand cmd1 = new(cmdText1, mcon);
            MySqlDataAdapter adapter1 = new();
            adapter1.SelectCommand = cmd1;
            dt.Clear();
            adapter1.Fill(dt);
            int Code1 = 0;
            if (dt.Rows.Count > 0)
                Code1 = Convert.ToInt32(dt.Rows[0]["id"].ToString());
            string cmdText = "Select * from SubTaska where ActId='" + Code1 + "' ORDER BY id ASC";
            MySqlCommand cmd = new(cmdText, mcon);
            MySqlDataAdapter adapter = new();
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (i % 2 == 0)
                //    Subtaskus.BackgroundColor = Color.FromArgb("FFDCDCDC");
                //else
                //    Subtaskus.BackgroundColor = Color.FromArgb("FFFFFFFF");
                SubTusk = new()
                    {
                        new Taskis{ Code = i+1,
                         Casuals = dt.Rows[i]["Casuals"].ToString(),
                         Machines = dt.Rows[i]["Machines"].ToString(),
                         Materials = dt.Rows[i]["Materials"].ToString(),
                         Description = dt.Rows[i]["Description"].ToString(),
                         SubTaski= dt.Rows[i]["SubTaski"].ToString(),
                         RdSection = dt.Rows[i]["RdSection"].ToString(),
                         Trucks = dt.Rows[i]["Trucks"].ToString()}
                    };
                if (i < dt.Rows.Count)
                    foreach (var itemz in SubTusk)
                    {
                        Taskiz.Add(itemz);
                    }
            }
            Subtaskus.ItemsSource = Taskiz;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
        }
    }

    private void btnDesc_Clicked(object sender, EventArgs e)
    {
        AddNewSubTask addNewSubTask = new AddNewSubTask();
        this.ShowPopup(addNewSubTask);
    }
}