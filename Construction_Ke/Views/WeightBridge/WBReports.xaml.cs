using Construction_Ke.ViewModel;


namespace Construction_Ke.Views.WeightBridge;

public partial class WBReports : ContentPage
{
    ListWeightViewModel listWeight;
    public WBReports()
    {
        InitializeComponent();
        BindingContext = listWeight = new ListWeightViewModel();
    }
    private void ViewReport()
    {
        
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        listWeight.OnAppearing();
    }
}