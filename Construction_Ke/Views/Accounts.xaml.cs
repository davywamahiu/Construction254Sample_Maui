using Construction_Ke.ViewModel.AccountsVM;

namespace Construction_Ke.Views;

public partial class AccountPage : ContentPage
{
    public AccountPage()
    {
        InitializeComponent();
        BindingContext = new AccountsViewModel();
        App.Current.MainPage.DisplayAlert("Enjoy", "Hello it's me again.", "OK");
    }
}