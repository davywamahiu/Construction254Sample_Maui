using Construction_Ke.ViewModel.AssetsViewModel;
namespace Construction_Ke.Views;

public partial class AssetConstrunctionManager : ContentPage
{
	public AssetConstrunctionManager()
	{
		InitializeComponent();
		BindingContext = new AssetViewModel();
	}
}