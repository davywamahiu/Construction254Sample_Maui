using Construction_Ke.Model;

using MySql.Data.MySqlClient;

namespace Construction_Ke.ViewModel.WeibridgeVM
{
    public class AddWBMaterialVM : BaseViewModel
    {
        MySqlConnection mcon;
        string connString = "server=localhost;uid=root;pwd=;database=roben;";
        private string material;
        private double cost;
        private string matSerial;

        private long driverId;
        private string driverName;
        private long phone;
        private string plate;
        public AddWBMaterialVM()
        {
            SaveNewDriver = new(OnSaveDriver);
            SaveNewMaterial = new(OnSaveMaterial);
        }
        private async void OnSaveDriver(object obj)
        {
            try
            {
                Drivers drivers = new()
                {
                    DriverId = DriverId,
                    DriverName = DriverName,
                    Phone = Phone,
                    Plate = Plate
                };
                mcon = new(connString);
                mcon.Open();
                string cmdText = "INSERT INTO drivers (DriverId, DRIVA, driverPhone, Plate)" +
                        "VALUES (@DriverId, @DriverName, @Phone, @Plate)";
                MySqlCommand cmd = new(cmdText, mcon);
                cmd.Parameters.AddWithValue("@DriverId", drivers.DriverId);
                cmd.Parameters.AddWithValue("@DriverName", drivers.DriverName);
                cmd.Parameters.AddWithValue("@Phone", drivers.Phone);
                cmd.Parameters.AddWithValue("@Plate", drivers.Plate);
                cmd.ExecuteNonQuery();
                await Shell.Current.DisplayAlert("Success!!!", "Driver has been added.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Driver Exists.", ex.Message, "Continue");
            }
        }
        private async void OnSaveMaterial(object obj)
        {
            MaterialModel material = new()
            {
                Material = Material,
                Cost = Cost,
                MatSerial = MatSerial
            };
            try
            {
                mcon = new(connString);
                mcon.Open();
                string cmdText = "INSERT INTO commodities (MaterialS, MaterialCost, MatSerial)" +
                        "VALUES (@Material, @Cost, @MatSerial)";
                MySqlCommand cmd = new(cmdText, mcon);
                cmd.Parameters.AddWithValue("@Material", material.Material);
                cmd.Parameters.AddWithValue("@Cost", material.Cost);
                cmd.Parameters.AddWithValue("@MatSerial", material.MatSerial);
                cmd.ExecuteNonQuery();
                await Shell.Current.DisplayAlert("Success!!!", "Material has been added.", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Material Exists.", ex.Message, "Continue");
            }
        }
        public Command SaveNewMaterial { get; }
        public Command SaveNewDriver { get; }
        public string Material { get => material; set => material = value; }
        public double Cost { get => cost; set => cost = value; }
        public string MatSerial { get => matSerial; set => matSerial = value; }
        public long DriverId { get => driverId; set => driverId = value; }
        public string DriverName { get => driverName; set => driverName = value; }
        public long Phone { get => phone; set => phone = value; }
        public string Plate { get => plate; set => plate = value; }
    }
}
