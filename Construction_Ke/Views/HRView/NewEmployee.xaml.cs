using Construction_Ke.ViewModel.EmployeeViewModel;
using CommunityToolkit.Maui.Views;
using Construction_Ke.Model;
using Construction_Ke.Views.HRView.HRPopupView;
namespace Construction_Ke.Views.HRView;

public partial class NewEmployee : ContentPage
{
    NewEmpViewModel ListWeightView;

    public NewEmployee()
	{
		InitializeComponent();
		BindingContext = ListWeightView = new NewEmpViewModel();
        UploadNewInfo();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ListWeightView.OnAppearing();
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
		AddPopupView employee = new();
		this.ShowPopup(employee);
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection;
        if (current != null)
            UpdateSelectionData(e.PreviousSelection, e.CurrentSelection);
    }
    Employee emp;
    private async void UpdateSelectionData(IReadOnlyList<object> previousSelection, IReadOnlyList<object> currentSelection)
    {
        var selectedContact = currentSelection.FirstOrDefault() as Employee;
        if (selectedContact != null)
            emp = new()
            {
                EPin = selectedContact.EPin,
                Phone = selectedContact.Phone,
                KRAPin = selectedContact.KRAPin,
                Salary = selectedContact.Salary,
                NatID = selectedContact.NatID
            };
        else
            return;
        try
        {
            epin.Text = emp.EPin;
            phone.Text = emp.Phone.ToString();
            krapin.Text = emp.KRAPin;
            salary.Text = emp.Salary.ToString();
            natid.Text = emp.NatID.ToString();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
        }
    }
}