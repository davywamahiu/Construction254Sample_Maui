using Construction_Ke.ViewModel.AssetsViewModel;
using CommunityToolkit.Maui.Views;
using Construction_Ke.Model;
using Construction_Ke.Views.AssetsView.AssetsPopupView;
namespace Construction_Ke.Views.AssetsView;

public partial class MachineryManager : ContentPage
{
    AssetViewModel ListWeightView;

    public MachineryManager()
	{
		InitializeComponent();
		BindingContext = ListWeightView = new AssetViewModel();
        ReloadSrc();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ListWeightView.OnAppearing();
    }
    private void ReloadSrc()
    {
        List<string> strings = new();
        strings.Add("Yes");
        strings.Add("No");
        logbook.ItemsSource = strings;
        List<string> strings1 = new();
        strings1.Add("New");
        strings1.Add("Used");
        condt.ItemsSource = strings1;
        List<string> strings2 = new();
        strings2.Add("Excavator");
        strings2.Add("Roller");
        strings2.Add("Grader");
        strings2.Add("Shovel");
        strings2.Add("Marine");
        strings2.Add("Paver");
        strings2.Add("Fork Lift");
        strings2.Add("Washer");
        strings2.Add("Crasher");
        strings2.Add("Generator");
        vtyp.ItemsSource = strings2;
        List<string> strings3 = new();
        strings3.Add("Active");
        strings3.Add("Sold");
        strings3.Add("Decommissioned");
        useds.ItemsSource = strings3;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            AddNewMachinery wB = new();
            this.ShowPopup(wB);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "ok");
        }
    }

    private void machineryies_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection;
        if (current != null)
            UpdateSelectionData(e.PreviousSelection, e.CurrentSelection);
        //btnUpdate.IsEnabled = true;
    }
    private void UpdateSelectionData(IReadOnlyList<object> previousSelection, IReadOnlyList<object> currentSelection)
    {
        var selectedContact = currentSelection.FirstOrDefault() as RobenMachinery;
        if (selectedContact == null)
            return;
        mdi.Text = selectedContact.MId.ToString();
        cost.Text = selectedContact.Cost.ToString();
        regno.Text = selectedContact.RegNo;
        yearz.Text = selectedContact.Yearz.ToString();
    }
}