﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt
{
    public interface IFileClient
    {
        Task DeleteFile(string storeName, string filePath);
        Task<bool> FileExists(string storeName, string filePath);
        Task<Stream> GetFile(string storeName, string filePath);
        Task<string> GetFileUrl(string storeName, string filePath);
        Task SaveFile(string storeName, string filePath, Stream fileStream, IDictionary<string, string> metadata = null);
    }
}
