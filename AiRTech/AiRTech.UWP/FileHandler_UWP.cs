using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using AiRTech.Core.DataHandling;
using AiRTech.UWP;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHandler_UWP))]
namespace AiRTech.UWP
{
    public class FileHandler_UWP : IFileHandler
    {
        private const string Filename = "subjects_data.atdb";

        public void Init()
        {
            FilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, Filename);
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath);
            }
        }

        public string FilePath { get; set; }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public string Load()
        {
            throw new NotImplementedException();
        }

        public void Save(string data)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllData()
        {
            throw new NotImplementedException();
        }

        public SQLiteConnection GetDatabaseConnection()
        {
            throw new NotImplementedException();
        }

        SQLiteConnection IFileHandler.GetDatabaseConnection()
        {
            var conn = new SQLiteConnection(FilePath);
            return conn;
        }
    }
}
