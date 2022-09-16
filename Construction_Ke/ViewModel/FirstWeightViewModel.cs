using CommunityToolkit.Diagnostics;

using Construction_Ke.Model;
using Construction_Ke.Views.Fuelics;

namespace Construction_Ke.ViewModel
{
    public class FirstWeightViewModel : BaseViewModel
    {
        private int code;
        private double weight;
        private string driver;
        private long phone;
        private string plate;
        private string material;
        private double amount;
        private DateTime dateTime;
        private TimeOnly time;
        private int ticket;


        
        public FirstWeightViewModel()
        {

            
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }





        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(Driver)
                && !string.IsNullOrWhiteSpace(Material)
                && !string.IsNullOrWhiteSpace(Plate)
                && !string.IsNullOrWhiteSpace(Ticket.ToString())
                && !string.IsNullOrWhiteSpace(Weight.ToString());
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public int Ticket { get => ticket; set => ticket = value; }
        public double Weight { get => weight; set => weight = value; }
        public string Driver { get => driver; set => driver = value; }
        public long Phone { get => phone; set => phone = value; }
        public string Plate { get => plate; set => plate = value; }
        public string Material { get => material; set => material = value; }
        public double Amount { get => amount; set => amount = value; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            
            FirstWeight newItem = new FirstWeight()
            {
                Ticket = Ticket,
                Weight = Weight,
                Driver = Driver,
                Phone = Phone,
                Plate = Plate,
                Material = Material,
                Amount = Amount
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

    }
}
