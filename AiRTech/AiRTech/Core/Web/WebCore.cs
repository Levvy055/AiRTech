using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using AiRTech.Core.DataHandling;
using AiRTech.Core.Misc;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;
using AiRTech.Views.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AiRTech.Core.Web
{
    public class WebCore
    {
        private readonly DbHandler _database;
        private bool _connected;
        #region Http Adresses
        private const string BaseUrl = "https://airtech.grmdev.eu/f_api/";
        //private const string BaseUrl = "http://localhost:8080/airtech_server/api/";
        private const string FnDefs = "defs_*.json";
        private const string FnFmls = "fmls_*.json";
        private const string FnLinker = "linker.json";
        private const string FnDefDir = "defs/";
        private const string FnFmlsDir = "fmls/";
        private const string FnImgDir = "images/";
        #endregion

        public WebCore(DbHandler database)
        {
            _database = database;
            Online();
        }

        public async Task<List<Definition>> GetDefinitionList(SubjectType subjectType)
        {
            var pathToDefs = FnDefs.Replace("*", subjectType.ToString().ToLower());
            var list = await GetData<List<Definition>>(pathToDefs);
            foreach (var def in list)
            {
                def.LinkDeserializedComponents(subjectType);
            }
            return list;
        }

        private async Task<T> GetData<T>(string path)
        {
            using (var client = NewClient)
            {
                try
                {
                    var resp = await client.GetAsync(path);
                    resp.EnsureSuccessStatusCode();
                    var respBody = await resp.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<T>(respBody);
                    return obj;
                }
                catch (Exception e)
                {
                    HandleWebException(e);
                }
            }
            return default(T);
        }

        public async Task<bool> Online()
        {
            if (_connected)
            {
                return _connected;
            }
            using (var client = NewClient)
            {
                try
                {
                    var r = await client.GetStringAsync("index.html");
                    _connected = !string.IsNullOrWhiteSpace(r) && r.StartsWith("<!DOCTYPE html>");
                }
                catch (Exception e)
                {
                    HandleWebException(e);
                }
            }
            return _connected;
        }

        public void HandleWebException(Exception e)
        {
            Debug.WriteLine(e);
            _connected = false;
        }

        private HttpClient NewClient
        {
            get
            {
                var httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(7),
                    BaseAddress = new Uri(BaseUrl)
                };
                httpClient.DefaultRequestHeaders.Clear();
                return httpClient;
            }
        }
    }
}
