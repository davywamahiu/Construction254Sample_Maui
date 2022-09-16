using Construction_Ke.ViewModel;
using Construction_Ke.ViewModel.FuelViewModels;

namespace Construction_Ke.Views;

public partial class FuelPage : ContentPage
{
    public FuelPage()
    {
        InitializeComponent();
        BindingContext = new FuelViewModel();
    }

}