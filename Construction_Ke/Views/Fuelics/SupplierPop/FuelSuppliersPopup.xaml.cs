using Construction_Ke.ViewModel.FuelViewModels;
using CommunityToolkit.Maui.Views;
namespace Construction_Ke.Views.Fuelics.SupplierPop;

public partial class FuelSuppliersPopup : Popup
{
	public FuelSuppliersPopup()
	{
		InitializeComponent();
        BindingContext = new AddNewFuelVM();
		Size = new(450.4, 500.5);
		CanBeDismissedByTappingOutsideOfPopup = false;
    }

	private void Button_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}