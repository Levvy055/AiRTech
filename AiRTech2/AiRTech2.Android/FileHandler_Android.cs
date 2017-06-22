using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AiRTech2.DataHandling;
using AiRTech2.Droid;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(FileHandler_Android))]
namespace AiRTech2.Droid
{
    public class FileHandler_Android : IFileHandler
    {

        public void Init()
        {
            CreateDefaultFilesAndDirs();
        }

        public void CreateDefaultFilesAndDirs(bool overrideFiles = false)
        {
        }

        public string RootAppPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
        
        public bool Exists(Uri uri)
        {
            var u = FileHelper.GetAboslutePath(uri);
            return Exists(u);
        }

        public bool Exists(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }
            return File.Exists(path);
        }

        public bool IsEmpty(Uri uri)
        {
            var fileInfo = new FileInfo(FileHelper.GetAboslutePath(uri));
            return fileInfo.Length == 0;
        }

        public void RemoveFile(Uri uri)
        {
            RemoveFile(FileHelper.GetAboslutePath(uri));
        }

        public void RemoveFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public Stream GetFileStream(string path, bool readOnly = false)
        {
            var fileAccess = readOnly ? FileAccess.Read : FileAccess.ReadWrite;
            path = Path.Combine(RootAppPath(), path);
            return File.Open(path, FileMode.OpenOrCreate, fileAccess);
        }

        private string GetFileContent(string filePath)
        {
            if (!Exists(filePath))
            {
                using (File.Create(filePath))
                {
                }
                return "";
            }
            var content = File.ReadAllText(filePath);
            return content;
        }
    }
}