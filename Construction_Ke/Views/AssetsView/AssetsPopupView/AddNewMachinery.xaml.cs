using Construction_Ke.ViewModel.AssetsViewModel;
namespace Construction_Ke.Views.AssetsView.AssetsPopupView;

public partial class AddNewMachinery : CommunityToolkit.Maui.Views.Popup
{
	public AddNewMachinery()
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
    private void Button_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}