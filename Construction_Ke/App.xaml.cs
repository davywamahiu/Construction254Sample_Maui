using Construction_Ke.Services;

namespace Construction_Ke;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        DependencyService.Register<MockDataStore>();
        MainPage = new AppShell();
    }
}
