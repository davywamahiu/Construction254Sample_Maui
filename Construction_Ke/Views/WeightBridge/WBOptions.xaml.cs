using Construction_Ke.ViewModel.WeibridgeVM;
namespace Construction_Ke.Views.WeightBridge;

public partial class WBOptions : CommunityToolkit.Maui.Views.Popup
{
    public WBOptions()
    {
        InitializeComponent();
        Size = new(900.0, 500.5);
        CanBeDismissedByTappingOutsideOfPopup = false;
        BindingContext = new AddWBMaterialVM();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}