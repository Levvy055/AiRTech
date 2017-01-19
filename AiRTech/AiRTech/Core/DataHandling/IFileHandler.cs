using System.Collections.Generic;
using System.Threading.Tasks;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;

namespace AiRTech.Core.DataHandling
{
    public interface IFileHandler
    {
        void Init();
        void CreateDefaultFilesAndDirs(bool overrideFiles=false);
        Task<IEnumerable<Definition>> GetDefinitions(SubjectType subjectType);
        void UpdateDefinitions(List<Definition> newDefList, SubjectType subjectType);
    }
}
