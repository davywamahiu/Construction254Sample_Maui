using Construction_Ke.ViewModel;

namespace Construction_Ke.Views.AccountView;

public partial class AccReports : ContentPage
{
    ListWeightViewModel listWeight;
    public AccReports()
	{
		InitializeComponent();
        BindingContext = listWeight = new ListWeightViewModel();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        listWeight.OnAppearing();
    }
}