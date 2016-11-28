using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AiRTech.Core.DataHandling;
using AiRTech.Droid;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Xamarin.Forms;
using Environment = System.Environment;
using File = Java.IO.File;

[assembly: Dependency(typeof(FileHandler_Android))]
namespace AiRTech.Droid
{
    public class FileHandler_Android : IFileHandler
    {
        private const string Filename = "subjects_data.atdb";

        public void Init()
        {
            DbFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Filename);
            Create();
        }

        public void Create()
        {
            var file=new File(DbFilePath);
            if (!file.Exists())
            {
                file.CreateNewFile();
            }
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

        public string GetDatabaseFilePath()
        {
            return DbFilePath;
        }

        public string DbFilePath { get; set; }
    }
}