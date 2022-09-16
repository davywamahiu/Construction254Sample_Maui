using Construction_Ke.ViewModel;
namespace Construction_Ke.Views.WeightBridge;

public partial class WBSettings : CommunityToolkit.Maui.Views.Popup
{
    public WBSettings()
    {
        InitializeComponent();
        Size = new(650.0, 500.5);
        BindingContext = new ListWeightViewModel();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}