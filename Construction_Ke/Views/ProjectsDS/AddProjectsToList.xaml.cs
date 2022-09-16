using Construction_Ke.Model;
using Construction_Ke.ViewModel.ProjectsVM;
namespace Construction_Ke.Views.ProjectsDS;

public partial class AddProjectsToList : CommunityToolkit.Maui.Views.Popup
{
    //readonly PopupSizeConstants popupSize;
    
    public AddProjectsToList()
	{
		
        InitializeComponent();
        Size = new(650.0, 520.5);
        CanBeDismissedByTappingOutsideOfPopup = false;
        BindingContext = new ContractViewModel();
        UpdateViewz();
    }
    private void UpdateViewz()
    {
        List<string> strings = new();
        strings.Add("Cost-Plus Construction");
        strings.Add("Design and Build");
        strings.Add("Guaranteed Maximum Price");
        strings.Add("Incentive Construction");
        strings.Add("Integrated Project Delivery");
        strings.Add("Lump-sum");
        strings.Add("Time and Materials");
        strings.Add("unit price");
        txtContract.ItemsSource = strings;

        List<string> strings1 = new();
        strings1.Add("Roben Aberdare");
        strings1.Add("Joint Contract");
        strings1.Add("Subcontrated");
        txtContractor.ItemsSource = strings1;

        List<string> strings2 = new();
        strings2.Add("KENHA");
        strings2.Add("KURA");
        strings2.Add("KERRA");
        strings2.Add("COUNTY");
        strings2.Add("OTHERS");
        txtClient.ItemsSource = strings2;
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}