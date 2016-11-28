using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.DataHandling;
using AiRTech.Views.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AiRTech.Core.Web
{
    public class WebCore
    {
        private readonly IDbHandler _database;
        //private const string BaseUrl = "https://airtech.grmdev.eu/api/";
        private const string BaseUrl = "http://localhost:8080/airtech_server/api/";

        public WebCore(IDbHandler database)
        {
            _database = database;
            if (IsConnected())
            {
                var r = GetNewDataAsync();
            }
        }

        private Dictionary<Type, SubjectDataViewModel> GetNewDataAsync()
        {
            using (var client = GetNewClient())
            {
                try
                {
                    var resp = client.GetStringAsync("version").Result;
                    dynamic obj = JObject.Parse(resp);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            return null;
        }

        public bool IsConnected()
        {
            using (var client = GetNewClient())
            {
                var r = client.GetStringAsync("status").Result;
                return !string.IsNullOrWhiteSpace(r) && r.Equals("Working");
            }
        }

        public HttpClient GetNewClient()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };
            return client;
        }
    }
}
