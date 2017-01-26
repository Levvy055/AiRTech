using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AiRTech.Core.DataHandling;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;
using AiRTech.Core.Subjects.Formul;
using AiRTech.Droid;
using Newtonsoft.Json;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(FileHandler_Android))]
namespace AiRTech.Droid
{
    public class FileHandler_Android : IFileHandler
    {
        private const string DirNameDefs = "defs";
        private const string DirNameFmls = "fmls";

        public void Init()
        {
            DirDefs = Path.Combine(RootAppPath(), DirNameDefs);
            DirFmls = Path.Combine(RootAppPath(), DirNameFmls);
            CreateDefaultFilesAndDirs();
        }

        public void CreateDefaultFilesAndDirs(bool overrideFiles = false)
        {
            if (!Directory.Exists(DirDefs))
            {
                Directory.CreateDirectory(DirDefs);
            }
            if (!Directory.Exists(DirFmls))
            {
                Directory.CreateDirectory(DirFmls);
            }
            var dirImgd = Path.Combine(DirDefs, "images");
            if (!Directory.Exists(dirImgd))
            {
                Directory.CreateDirectory(dirImgd);
            }
            var dirImgf = Path.Combine(DirFmls, "images");
            if (!Directory.Exists(dirImgf))
            {
                Directory.CreateDirectory(dirImgf);
            }
        }

        public string RootAppPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public async Task<IEnumerable<Definition>> GetDefinitions(SubjectType subjectType)
        {
            var filename = Path.Combine(DirDefs, subjectType + ".json");
            var fc = GetFileContent(filename);
            if (string.IsNullOrWhiteSpace(fc))
            {
                return null;
            }
            var list = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<Definition>>(fc));
            if (list == null)
            {
                return null;
            }
            return list;
        }

        public async Task<IEnumerable<Formula>> GetFormulas(SubjectType subjectType)
        {
            var filename = Path.Combine(DirFmls, subjectType + ".json");
            var fc = GetFileContent(filename);
            if (string.IsNullOrWhiteSpace(fc))
            {
                return null;
            }
            var list = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<Formula>>(fc));
            if (list == null)
            {
                return null;
            }
            foreach (var f in list)
            {
                f.LinkDeserializedComponents(subjectType);
            }
            return list;
        }

        public void UpdateDefinitions(List<Definition> list, SubjectType subjectType)
        {
            var filename = Path.Combine(DirDefs, subjectType + ".json");
            GetFileContent(filename);
            var sList = JsonConvert.SerializeObject(list);
            File.WriteAllText(filename, sList);
        }

        public void UpdateFormulas(List<Formula> list, SubjectType subjectType)
        {
            var filename = Path.Combine(DirFmls, subjectType + ".json");
            GetFileContent(filename);
            var sList = JsonConvert.SerializeObject(list);
            File.WriteAllText(filename, sList);
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
            File.Delete(path);
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

        public string DirDefs { get; private set; }
        public string DirFmls { get; private set; }
    }
}