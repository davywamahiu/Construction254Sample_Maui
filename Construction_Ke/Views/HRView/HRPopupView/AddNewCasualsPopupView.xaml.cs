namespace Construction_Ke.Views.HRView.HRPopupView;

public partial class AddNewCasualsPopupView : CommunityToolkit.Maui.Views.Popup
{
	public AddNewCasualsPopupView()
	{
		InitializeComponent();
		Size = new(600.5, 500.5);
		CanBeDismissedByTappingOutsideOfPopup = false;
		UpdateNewInfo();
	}
	void UpdateNewInfo()
    {
        List<string> strings = new();
        List<string> strings1 = new();
        List<string> strings2 = new();

		strings.Add("Civil Works");
        strings.Add("Earth Works");
        strings.Add("Survey");
        strings.Add("Production");
        strings.Add("Lab");
        strings.Add("Other");
        department.ItemsSource = strings;

        strings1.Add("Unavailabe");
        supervisor.ItemsSource = strings1;

        strings2.Add("Active");
        estatus.ItemsSource = strings2;
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}