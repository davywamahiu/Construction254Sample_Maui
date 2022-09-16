using Construction_Ke.Model;

using Newtonsoft.Json;

namespace Construction_Ke.ViewModel
{
    public class LoginViewModel : BaseViewModel, IloginInterface
    {
        private string username;
        private string password;
        readonly IloginInterface _loginInterface;
        public Command NewLogin { get; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

        public LoginViewModel()
        {
            NewLogin = new(OnNewLogin);
        }
        async void OnNewLogin(object obj)
        {
            try
            {
                SysLogin login = await _loginInterface.Login(Username, Password);
                if(login !=null)
                    await Shell.Current.DisplayAlert("Sucess", "Login Succeded", "Continue");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        public async Task<SysLogin> Login(string uname, string pwd)
        {
            var userinfo = new List<SysLogin>();
            var client = new HttpClient();
            string url = "server=localhost;uid=root;pwd=;database=roben;";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            IsBusy = true;
            try
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var content = responseMessage.Content.ReadAsStringAsync().Result;
                    userinfo = JsonConvert.DeserializeObject<List<SysLogin>>(content);
                    return await Task.FromResult(userinfo.FirstOrDefault());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
            finally { IsBusy = false; }
            return await Task.FromResult(userinfo.FirstOrDefault());
        }
    }
}
