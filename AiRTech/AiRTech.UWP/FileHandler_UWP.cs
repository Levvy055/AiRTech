using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using AiRTech.Core.DataHandling;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;
using AiRTech.UWP;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHandler_UWP))]
namespace AiRTech.UWP
{
    public class FileHandler_UWP : IFileHandler
    {
        private const string DirNameDefs = "Defs";

        public void Init()
        {
            DirDefs = Path.Combine(ApplicationData.Current.LocalFolder.Path, DirNameDefs);
            CreateDefaultFilesAndDirs();
        }

        public void CreateDefaultFilesAndDirs(bool overrideFiles = false)
        {
            if (!Directory.Exists(DirDefs))
            {
                Directory.CreateDirectory(DirDefs);
            }
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
            foreach (var def in list)
            {
                def.LinkDeserializedComponents(subjectType);
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

        private static string GetFileContent(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
                return "";
            }
            var content = File.ReadAllText(filePath);
            return content;
        }

        public string DirDefs { get; private set; }
    }
}
