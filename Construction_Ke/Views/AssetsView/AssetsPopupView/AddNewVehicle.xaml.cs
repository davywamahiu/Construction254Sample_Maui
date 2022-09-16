using Construction_Ke.ViewModel.AssetsViewModel;
namespace Construction_Ke.Views.AssetsView.AssetsPopupView;

public partial class AddNewVehicle : CommunityToolkit.Maui.Views.Popup
{
	public AddNewVehicle()
	{
		InitializeComponent();
		BindingContext = new AssetViewModel();
		Size = new(600.5, 500.5);
		CanBeDismissedByTappingOutsideOfPopup = false;
        ReloadSrc();

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
    private void Button_Clicked(object sender, EventArgs e)
    {
		Close();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {

    }
}