using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AiRTech.Core.DataHandling
{
    public interface IFileHandler
    {
        void Init();
        void Create();
        string Load();
        void Save(string data);
        void RemoveAllData();
        string GetDatabaseFilePath();
    }
}
