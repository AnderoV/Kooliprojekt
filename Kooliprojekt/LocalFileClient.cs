using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt
{
    public class LocalFileClient : IFileClient
    {
        private readonly string _rootPath;

        public LocalFileClient(string rootPath)
        {
            _rootPath = rootPath;
        }

        public async Task DeleteFile(string storeName, string path)
        {
            var filePath = GetPath(path);
            if (!File.Exists(filePath))
            {
                return;
            }
            File.Delete(filePath);
        }

        public async Task<bool> FileExists(string storeName, string path)
        {
            var exists = File.Exists(GetPath(path));
            return await Task.FromResult(exists);
        }

        public async Task<Stream> GetFile(string storeName, string path)
        {
            var filePath = GetPath(path);
            if (!File.Exists(filePath))
            {
                return null;
            }
            return File.OpenRead(filePath);
        }

        public Task<string> GetFileUrl(string storeName, string path)
        {
            return null;
        }

        public async Task SaveFile(string storeName ,string path, Stream fileStream, IDictionary<string, string> metadata)
        {
            var filePath = GetPath(path);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await fileStream.CopyToAsync(stream);
            }
        }

        private string GetPath(string filePath)
        {
            return Path.Combine(_rootPath, filePath);
        }
    }
}
// var path = Path.Combine(_fileRoot, storeName, filePath);