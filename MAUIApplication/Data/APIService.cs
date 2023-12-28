//using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApplication.Data
{
    public class APIService : IAPIService
    {
        private string apiUrl;
        private readonly IConnectivity _connectivity = Connectivity.Current;

        public APIService()
        {
            apiUrl = GetAPIUrl();
        }

        // Thực hiện kết nối tới API
        public async Task<HttpResponseMessage> Connect(string httpMethod, string url, dynamic data = null)
        {
            if (apiUrl == null)
            {
                await Shell.Current.DisplayAlert("Chưa có địa chỉ API", "Cài đặt địa chỉ API với Local Admin trước khi sử dụng", "OK");
                return null;
            }

            var networkAccess = Connectivity.Current.NetworkAccess;
            if (networkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Kết nối Internet thất bại!", "Kiểm tra lại kết nối Internet và thử lại", "OK");
                return null;
            }

            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.Token);
                HttpResponseMessage response = new();
                string requestStr = "";
                switch (httpMethod)
                {
                    case "GET":
                        response = await client.GetAsync($"{apiUrl}/{url}");
                        break;
                    case "POST":
                        requestStr = JsonConvert.SerializeObject(data);
                        response = await client.PostAsync($"{apiUrl}/{url}", new StringContent(requestStr, Encoding.UTF8, "application/json"));
                        break;
                    case "PUT":
                        requestStr = JsonConvert.SerializeObject(data);
                        response = await client.PutAsync($"{apiUrl}/{url}", new StringContent(requestStr, Encoding.UTF8, "application/json"));
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiUrl}/{url}");
                        break;
                }
                
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response;
                }
                else
                {
                    return response;
                }
                //else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                //{
                //    await TokenCheck();
                //}
                //else
                //{
                //    var toast = Toast.Make($"Error: {response.StatusCode}, {response.Content}", ToastDuration.Long);
                //    await toast.Show();
                //}
            }
            
        }

        // Lấy địa chỉ IP và Port của API từ page Setting
        public string GetAPIUrl()
        {
            return "http://10.0.2.2:5016/api";
            //string ipAddress = Preferences.Get("APIIpAddress", "");
            //string port = Preferences.Get("APIPort", "");
            //if (!string.IsNullOrEmpty(ipAddress) && !string.IsNullOrEmpty(port))
            //{
            //    return $"http://{ipAddress}:{port}/api";
            //}
            //else
            //{
            //    return null;
            //}
        }

        // Thực hiện kiểm tra thời hạn của Token mỗi lần kết nối API
        
    }
}
