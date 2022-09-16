using CommunityToolkit.Maui.Views;
using Construction_Ke.Model;
using Construction_Ke.ViewModel.AccountsVM;

namespace Construction_Ke.Views.AccountView;

public partial class WBAccount : ContentPage
{
    WBAccViewMoodels ListWeightView;
    public WBAccount()
	{
		InitializeComponent();
		BindingContext = ListWeightView = new WBAccViewMoodels();

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ListWeightView.OnAppearing();
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
		WBAccountPop.WBPaymentPopup popup = new();
		this.ShowPopup(popup);
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection;
        if (current != null)
            UpdateSelectionData(e.PreviousSelection, e.CurrentSelection);
    }
    WBCustomerDeposit WBCustomer;
    private void UpdateSelectionData(IReadOnlyList<object> previousSelection, IReadOnlyList<object> currentSelection)
    {
        var selectedContact = currentSelection.FirstOrDefault() as WBCustomerDeposit;
        if (selectedContact != null)
        {
            customer.Text = selectedContact.Driver;
            accounti.Text = selectedContact.AccountNo;
            mpesa.Text = selectedContact.MpesaUid;
            depositi.Text = selectedContact.Deposit.ToString();
            material.Text = selectedContact.Material;
            uzito.Text = selectedContact.Tonage.ToString();
        }
        else
            return;
    }
}