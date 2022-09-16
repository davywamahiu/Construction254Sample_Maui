using Construction_Ke.ViewModel.AssetsViewModel;
using CommunityToolkit.Maui.Views;
using Construction_Ke.Model;
using Construction_Ke.Views.AssetsView.AssetsPopupView;
namespace Construction_Ke.Views.AssetsView;


public partial class VehicleManager : ContentPage
{
    AssetViewModel ListWeightView;

    public VehicleManager()
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
        strings2.Add("Double Cub");
        strings2.Add("Saloon Car");
        strings2.Add("SUV");
        strings2.Add("Lorry");
        strings2.Add("Dump Truck");
        strings2.Add("Trucks");
        strings2.Add("Container");
        strings2.Add("Trailer");
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
            AddNewVehicle wB = new();
            this.ShowPopup(wB);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "ok");
        }
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection;
        if (current != null)
            UpdateSelectionData(e.PreviousSelection, e.CurrentSelection);
        //btnUpdate.IsEnabled = true;
    }
    private void UpdateSelectionData(IReadOnlyList<object> previousSelection, IReadOnlyList<object> currentSelection)
    {
        var selectedContact = currentSelection.FirstOrDefault() as RobenVehicles;
        if (selectedContact == null)
            return;
        vid.Text = selectedContact.VId.ToString();
        cost.Text = selectedContact.Cost.ToString();
        plate.Text = selectedContact.Plate;
        yearz.Text = selectedContact.Yearz.ToString();
    }
}