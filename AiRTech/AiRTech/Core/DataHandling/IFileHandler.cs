﻿namespace AiRTech.Core.DataHandling
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
