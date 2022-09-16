using CommunityToolkit.Maui.Views;
using Construction_Ke.Model;
using Construction_Ke.Views.ProjectsDS.ProjePopupz;
using Construction_Ke.ViewModel.ProjectsVM;

namespace Construction_Ke.Views.ProjectsDS;

public partial class ProjectList : ContentPage
{
    string upName = "";
    ContractViewModel ListWeightView1;
    public ProjectList()
	{
		InitializeComponent();
		BindingContext = ListWeightView1 =  new ContractViewModel();
        if (string.IsNullOrEmpty(upName))
        {
            btnList.IsEnabled = false;
            btnMaterial.IsEnabled = false;
            btnLabor.IsEnabled = false;
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ListWeightView1.OnAppearing();
    }
    //partial void ChangedHandler(object sender, EventArgs e);

    //void OnHandlerChanged(object sender, EventArgs e) => ChangedHandler(sender, e);
    async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            var addProjectsToList = new AddProjectsToList();

            await this.ShowPopupAsync(addProjectsToList);
            ////await Shell.Current.GoToAsync(nameof(NewPage1));
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
        }
        
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection;
        if (current != null)
            UpdateSelectionData(e.PreviousSelection, e.CurrentSelection);
    }
    private async void UpdateSelectionData(IReadOnlyList<object> previousSelection, IReadOnlyList<object> currentSelection)
    {
        var selectedContact = currentSelection.FirstOrDefault() as ContractsModel;
        if (selectedContact == null)
            return;
        bool abc = await Shell.Current.DisplayAlert("Project Info is as follows", "Project Type: ".ToUpper() + selectedContact.ProjectType + "\n" +
            "Project name: ".ToUpper() + selectedContact.ProjectName + "\n" +
            "Starts: ".ToUpper() + selectedContact.StartD + "   " + "Ends: ".ToUpper() + selectedContact.ExpectedD + "\n" +
            "RE: ".ToUpper() + selectedContact.Resident + "   " + "Our Engineer: ".ToUpper() + selectedContact.Engineer + "\n" +
            "Surveyor: ".ToUpper() + " " + selectedContact.Surveyor, "Continue", "Cancel");
        if (!abc)
            return;
        txtCost.Text = selectedContact.Cost.ToString() + " KSh";
        txtDiistance.Text = selectedContact.Distance.ToString()+" KM";
        txtEnd.Text = selectedContact.ExpectedD.ToString();
        txtEngineer.Text = selectedContact.Engineer;
        txtName.Text = selectedContact.ProjectName;
        txtResident.Text = selectedContact.Resident;
        txtStart.Text = selectedContact.StartD.ToString();
        txtSurveyor.Text = selectedContact.Surveyor;
        txtType.Text = selectedContact.ProjectType;
        upName = selectedContact.Code.ToString();
        if (!string.IsNullOrEmpty(upName))
        {
            btnList.IsEnabled = true;
            btnMaterial.IsEnabled = true;
            btnLabor.IsEnabled = true;
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(upName))
            return;
        else
            btnList.IsEnabled = false;
        var addProjectsToList = new AddBoqDescription(upName);

        await this.ShowPopupAsync(addProjectsToList);
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(upName))
            return;
        else
            btnMaterial.IsEnabled = false;
        var addProjectsToList = new AddBoqMaterials();

        await this.ShowPopupAsync(addProjectsToList);
    }

    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(upName))
            return;
        else
            btnLabor.IsEnabled = false;
        var addProjectsToList = new AddBogLabor();

        await this.ShowPopupAsync(addProjectsToList);
    }
}