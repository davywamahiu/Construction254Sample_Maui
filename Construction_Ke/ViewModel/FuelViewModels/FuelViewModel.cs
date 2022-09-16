
using Construction_Ke.Model;
using Construction_Ke.Views.Fuelics;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace Construction_Ke.ViewModel.FuelViewModels
{
    public class FuelViewModel : BaseViewModel
    {
        
        //MySqlConnection mcon;
        //string connString = "server=localhost;uid=root;pwd=;database=roben;";
        public Command LoadFuelRequisition { get; }
        public Command LoadRepairs { get; }
        public Command LoadFuelReport { get; }
        public Command LoadRefuel { get; }
        public Command LoadReceiveFuel { get; }
        

        public FuelViewModel()
        {
            LoadFuelRequisition = new Command(GetLoadFuelRequisition);
            LoadRepairs = new Command(GetLoadRepairs);
            
            LoadFuelReport = new Command(GetFuelReport);
            LoadReceiveFuel = new Command(GetFuelRecieve);
            LoadRefuel = new Command(GetRefuel);
        }
        
        public void OnAppearing()
        {
            IsBusy = true;
            //SelectedItem = null;
        }
        private async void GetLoadFuelRequisition(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(FuelRequesition));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }

        private async void GetLoadRepairs(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(RepairsDamages));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }

        private async void GetFuelReport(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(FuelReportManager));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }

        private async void GetRefuel(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(Refuel));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }

        private async void GetFuelRecieve(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ReceiveFuel));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message.ToString(), "OK");
            }

        }


    }
}
