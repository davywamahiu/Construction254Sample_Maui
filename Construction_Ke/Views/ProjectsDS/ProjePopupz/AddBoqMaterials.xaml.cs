using CommunityToolkit.Maui.Views;

using Construction_Ke.ViewModel.ProjectsVM;
namespace Construction_Ke.Views.ProjectsDS.ProjePopupz;

public partial class AddBoqMaterials : Popup
{
	List<string> strings { get; set; } = new();
	public AddBoqMaterials()
	{
		InitializeComponent();
		BindingContext = new ContractViewModel();
		Size = new(900.4, 500.5);
		CanBeDismissedByTappingOutsideOfPopup = false;
		UpdateMaterials();
    }
	void UpdateMaterials()
	{
		strings.Add("Cement");
		strings.Add("Steel");
		strings.Add("Ballast");
		strings.Add("614");
		material.ItemsSource = strings;
	}

    private void Button_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}