﻿using System;
using System.Collections.Generic;
using System.IO;
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
        bool Exists(Uri uri);
        bool Exists(string path);
        Stream GetFileStream(string path, bool readOnly = false);
        string RootAppPath();
        bool IsEmpty(Uri uri);
        void RemoveFile(Uri uri);
        void RemoveFile(string path);
    }
}
