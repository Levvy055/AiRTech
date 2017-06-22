using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AiRTech2.DataHandling
{
    public interface IFileHandler
    {
        void Init();
        void CreateDefaultFilesAndDirs(bool overrideFiles=false);
        bool Exists(Uri uri);
        bool Exists(string path);
        Stream GetFileStream(string path, bool readOnly = false);
        string RootAppPath();
        bool IsEmpty(Uri uri);
        void RemoveFile(Uri uri);
        void RemoveFile(string path);
    }
}
