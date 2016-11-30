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
            var c = IsConnected();
            c.Wait(1000);
            if (c.IsCompleted && c.Result)
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

        public async Task<bool> IsConnected()
        {
            try
            {
                using (var client = GetNewClient())
                {
                    var r = await client.GetStringAsync("status");
                    return !string.IsNullOrWhiteSpace(r) && r.Equals("Working");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        public HttpClient GetNewClient()
        {
            var client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(7),
                BaseAddress = new Uri(BaseUrl)
            };
            return client;
        }
    }
}
