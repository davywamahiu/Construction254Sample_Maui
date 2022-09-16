using Construction_Ke.Views;
using Construction_Ke.Views.AccountView;
using Construction_Ke.Views.AccountView.WBAccountPop;
using Construction_Ke.Views.Fuelics;
using Construction_Ke.Views.ProjectsDS;
using Construction_Ke.Views.WeightBridge;
using Construction_Ke.Views.AssetsView;
using Construction_Ke.Views.HRView;
using Construction_Ke.Views.HRView.HRPopupView;
using Construction_Ke.Views.AssetsView.AssetsPopupView;
using Construction_Ke.Views.ProjectsDS.ProjePopupz;

namespace Construction_Ke;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(FuelPage), typeof(FuelPage));
        Routing.RegisterRoute(nameof(Weighbridge), typeof(Weighbridge));
        Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
        Routing.RegisterRoute(nameof(Procurement), typeof(Procurement));
        Routing.RegisterRoute(nameof(ReceiveFuel), typeof(ReceiveFuel));
        Routing.RegisterRoute(nameof(Refuel), typeof(Refuel));
        Routing.RegisterRoute(nameof(FuelReportManager), typeof(FuelReportManager));
        Routing.RegisterRoute(nameof(RepairsDamages), typeof(RepairsDamages));
        Routing.RegisterRoute(nameof(FuelRequesition), typeof(FuelRequesition));
        Routing.RegisterRoute(nameof(WBOptions), typeof(WBOptions));
        Routing.RegisterRoute(nameof(WBSettings), typeof(WBSettings));
        Routing.RegisterRoute(nameof(WBReports), typeof(WBReports));
        Routing.RegisterRoute(nameof(AccountCompany), typeof(AccountCompany));
        Routing.RegisterRoute(nameof(AccDimension), typeof(AccDimension));
        Routing.RegisterRoute(nameof(PaymentTerm), typeof(PaymentTerm));
        Routing.RegisterRoute(nameof(AccountSettingz), typeof(AccountSettingz));
        Routing.RegisterRoute(nameof(AccPeriod), typeof(AccPeriod));
        Routing.RegisterRoute(nameof(FinanceBooks), typeof(FinanceBooks));
        Routing.RegisterRoute(nameof(FiscalYears), typeof(FiscalYears));
        Routing.RegisterRoute(nameof(ProjectsDs), typeof(ProjectsDs));
        Routing.RegisterRoute(nameof(AccReports), typeof(AccReports));
        Routing.RegisterRoute(nameof(PayWB), typeof(PayWB));
        Routing.RegisterRoute(nameof(ProjectList), typeof(ProjectList));
        Routing.RegisterRoute(nameof(AddProjectsToList), typeof(AddProjectsToList));
        Routing.RegisterRoute(nameof(WBAccount), typeof(WBAccount));
        Routing.RegisterRoute(nameof(WBPaymentPopup), typeof(WBPaymentPopup));
        Routing.RegisterRoute(nameof(AssetConstrunctionManager), typeof(AssetConstrunctionManager));
        Routing.RegisterRoute(nameof(VehicleManager), typeof(VehicleManager));
        Routing.RegisterRoute(nameof(MachineryManager), typeof(MachineryManager));
        Routing.RegisterRoute(nameof(AddNewVehicle), typeof(AddNewVehicle));
        Routing.RegisterRoute(nameof(AddNewMachinery), typeof(AddNewMachinery));
        Routing.RegisterRoute(nameof(HumanResourcePage), typeof(HumanResourcePage));
        Routing.RegisterRoute(nameof(NewEmployee), typeof(NewEmployee));
        Routing.RegisterRoute(nameof(NewCasualEmployee), typeof(NewCasualEmployee));
        Routing.RegisterRoute(nameof(HumanResourcePage), typeof(HumanResourcePage));
        Routing.RegisterRoute(nameof(AddNewCasualsPopupView), typeof(AddNewCasualsPopupView));
        Routing.RegisterRoute(nameof(AddPopupView), typeof(AddPopupView));
        Routing.RegisterRoute(nameof(FleetManagerView), typeof(FleetManagerView));
        Routing.RegisterRoute(nameof(AssignFleetToDriver), typeof(AssignFleetToDriver));
        Routing.RegisterRoute(nameof(TaskListPage), typeof(TaskListPage));
        Routing.RegisterRoute(nameof(AddNewTasksPopup), typeof(AddNewTasksPopup));
        Routing.RegisterRoute(nameof(AddNewSubTask), typeof(AddNewSubTask));
        Routing.RegisterRoute(nameof(AddBogLabor), typeof(AddBogLabor));
        Routing.RegisterRoute(nameof(AddBoqDescription), typeof(AddBoqDescription));
        Routing.RegisterRoute(nameof(AddBoqMaterials), typeof(AddBoqMaterials));
    }
}
