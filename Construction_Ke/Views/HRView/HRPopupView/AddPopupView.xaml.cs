using Construction_Ke.ViewModel.EmployeeViewModel;
namespace Construction_Ke.Views.HRView.HRPopupView;

public partial class AddPopupView : CommunityToolkit.Maui.Views.Popup
{
	public AddPopupView()
	{
		InitializeComponent();
		Size = new(600.5, 500.5);
		CanBeDismissedByTappingOutsideOfPopup = false;
		BindingContext = new NewEmpViewModel();
		UploadNewInfo();
	}
	void UploadNewInfo()
    {
		List<string> strings = new();
		List<string> strings1 = new();
		List<string> strings2 = new();
		strings.Add("Manager");
		strings.Add("Gen Manager");
		strings.Add("Managing Director");
		strings.Add("Secretary");
		strings.Add("Store Manager");
		strings.Add("Drivers");
		strings.Add("Cooks");
		strings.Add("Watchman");
		strings.Add("Operators");
		strings.Add("Mechanic");
		strings.Add("Cleaners");
		strings.Add("Chef");
		strings.Add("Foreman");
		strings.Add("Surveyor");
		strings.Add("Engineer");
		strings.Add("Fuel Attendant");
		strings.Add("Weighbridge Operators");
		strings.Add("Others");
		role.ItemsSource = strings;

		strings1.Add("Civil Engineer");
		strings1.Add("Structural Engineer");
		strings1.Add("Quantity Surveyor");
		strings1.Add("Land Surveyor");
		strings1.Add("Lab Technician");
		strings1.Add("Plant Operator");
		strings1.Add("Accountant");
		strings1.Add("Technician");
		strings1.Add("Others");
		profession.ItemsSource = strings1;

		strings2.Add("GM");
        strings2.Add("MD");
        strings2.Add("Supervisor Civil");
        strings2.Add("Supervisor Surveyor");
		strings2.Add("Others");
		supervisor.ItemsSource = strings2;
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}