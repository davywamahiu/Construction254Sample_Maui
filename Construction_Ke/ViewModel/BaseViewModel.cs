using Construction_Ke.Model;
using Construction_Ke.Services;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Construction_Ke.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }





        //public int Ticket { get => ticket; set => ticket = value; }
        //public double Tonage { get => weight; set => weight = value; }
        //public string Driver { get => driver; set => driver = value; }
        //public long Phone { get => phone; set => phone = value; }
        //public string Plate { get => plate; set => plate = value; }
        //public string Material { get => material; set => material = value; }
        //public double Amount { get => amount; set => amount = value; }
        public IDataStore<FirstWeight> DataStore => DependencyService.Get<IDataStore<FirstWeight>>();

        //public int Code { get => code; set => code = value; }
        //public double Total { get => total; set => total = value; }

        protected bool SetProperty<T>(ref T backingStore, T value,
        [CallerMemberName] string propertyName = "",
        Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
