using Construction_Ke.ViewModel.ProjectsVM;

namespace Construction_Ke.Views;

public partial class ProjectsDs : ContentPage
{
	public ProjectsDs()
	{
		InitializeComponent();
		BindingContext = new ContractViewModel();
    }
}