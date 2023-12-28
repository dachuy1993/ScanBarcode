using CommunityToolkit.Mvvm.Input;
using MAUIApplication.Data;
using MAUIApplication.Models;
using Newtonsoft.Json;

namespace MAUIApplication.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        string userName;
        string password;
        public static string EmpIdLogin;
        public static string EmpNmLogin;

        private readonly IAPIService _apiservice = new APIService();
        public string UserName
        {
            get => this.userName;
            set => SetProperty(ref this.userName, value);
        }

        public string Password
        {
            get => this.password;
            set => SetProperty(ref this.password, value);
        }

        public LoginViewModel()
        {
            
        }
         
        [RelayCommand]
        public async Task Login()
        {
            try
            {
                HttpClient client = new HttpClient();
                string uri = "Login/PostUser";

                LoginUser Userlogin = new LoginUser();
                LoginJson Userlogin1 = new LoginJson();
                Userlogin.User = UserName;
                Userlogin.Pass = Password;
                Userlogin.EmpId = "";
                Userlogin.EmpNm = "";


                var response = await _apiservice.Connect("POST", uri, Userlogin);
                
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await Navigation.NavigateToAsync<AboutViewModel>(true);
                    var json = await response.Content.ReadAsStringAsync();
                    LoginJson listLog = JsonConvert.DeserializeObject<LoginJson>(json);
                    EmpIdLogin = listLog.EmpId;
                    EmpNmLogin = listLog.EmpNm;
                }  
                else
                {
                    await Shell.Current.DisplayAlert("Thông báo", "UserName và Passwork bị sai, mời bạn nhập lại", "OK");
                    
                }    
                
                //Password = String.Empty;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

            
        }
    }
}