using Construction_Ke.ViewModel.EmployeeViewModel;
namespace Construction_Ke.Views;

public partial class HumanResourcePage : ContentPage
{
	public HumanResourcePage()
	{
		InitializeComponent();
		BindingContext = new NewEmpViewModel();
	}
}