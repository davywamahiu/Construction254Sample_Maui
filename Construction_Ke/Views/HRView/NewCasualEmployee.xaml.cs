using Construction_Ke.ViewModel.EmployeeViewModel;
using CommunityToolkit.Maui.Views;
using Construction_Ke.Model;
using Construction_Ke.Views.HRView.HRPopupView;
namespace Construction_Ke.Views.HRView;

public partial class NewCasualEmployee : ContentPage
{
    NewEmpViewModel ListWeightView;

    public NewCasualEmployee()
	{
		InitializeComponent();
		BindingContext = ListWeightView = new NewEmpViewModel();
        UpdateNewInfo();

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ListWeightView.OnAppearing();
    }
    void UpdateNewInfo()
    {
        List<string> strings = new();
        List<string> strings1 = new();
        List<string> strings2 = new();

        strings.Add("Civil Works");
        strings.Add("Earth Works");
        strings.Add("Survey");
        strings.Add("Production");
        strings.Add("Lab");
        strings.Add("Other");
        department.ItemsSource = strings;

        strings1.Add("Unavailabe");
        supervisor.ItemsSource = strings1;

        strings2.Add("Active");
        strings2.Add("Deactivate");
        estatus.ItemsSource = strings2;
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
		AddNewCasualsPopupView view = new();
		this.ShowPopup(view);
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
                Wages = selectedContact.Wages,
                OvertimeRates = selectedContact.OvertimeRates,
                NatID = selectedContact.NatID
            };
        else
            return;
        try
        {
            epin.Text = emp.EPin;
            phone.Text = emp.Phone.ToString();
            wages.Text = emp.Wages;
            overtimerates.Text = emp.OvertimeRates;
            natid.Text = emp.NatID.ToString();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
        }
    }
}