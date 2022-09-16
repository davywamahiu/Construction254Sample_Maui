using CommunityToolkit.Maui.Views;

using Construction_Ke.ViewModel.ProjectsVM;
namespace Construction_Ke.Views.ProjectsDS.ProjePopupz;

public partial class AddBoqDescription : Popup
{
	public AddBoqDescription(string upName)
	{
		InitializeComponent();
		BindingContext = new ContractViewModel();
        Size = new(900.4, 500.5);
        CanBeDismissedByTappingOutsideOfPopup = false;
		txtProject.Text = upName;
    }

	private void Button_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}