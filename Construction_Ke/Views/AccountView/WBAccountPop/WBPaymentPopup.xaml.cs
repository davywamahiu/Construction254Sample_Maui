using Construction_Ke.ViewModel.AccountsVM;

namespace Construction_Ke.Views.AccountView.WBAccountPop;

public partial class WBPaymentPopup : CommunityToolkit.Maui.Views.Popup
{
	public WBPaymentPopup()
	{
		InitializeComponent();
        BindingContext = new WBAccViewMoodels();
        Size = new(650.0, 500.5);
        CanBeDismissedByTappingOutsideOfPopup = false;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}