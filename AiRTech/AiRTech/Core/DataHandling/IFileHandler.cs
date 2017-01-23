using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;
using AiRTech.Core.Subjects.Formul;

namespace AiRTech.Core.DataHandling
{
    public interface IFileHandler
    {
        void Init();
        void CreateDefaultFilesAndDirs(bool overrideFiles=false);
        Task<IEnumerable<Definition>> GetDefinitions(SubjectType subjectType);
        Task<IEnumerable<Formula>> GetFormulas(SubjectType subjectType);
        void UpdateDefinitions(List<Definition> list, SubjectType subjectType);
        void UpdateFormulas(List<Formula> list, SubjectType subjectType);
        bool Exists(Uri uri);
        bool Exists(string path);
        Stream GetFileStream(string path, bool readOnly = false);
        string RootAppPath();
        bool IsEmpty(Uri uri);
        void RemoveFile(Uri uri);
        void RemoveFile(string path);
    }
}
