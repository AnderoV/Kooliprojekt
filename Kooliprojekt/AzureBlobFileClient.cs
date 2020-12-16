using Kooliprojekt;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class AzureBlobFileClient : IFileClient
{
    private CloudBlobClient _blobClient;

    public AzureBlobFileClient(string connectionString)
    {
        var account = CloudStorageAccount.Parse(connectionString);
        _blobClient = account.CreateCloudBlobClient();
    }

    public async Task DeleteFile(string storeName, string filePath)
    {
        var container = _blobClient.GetContainerReference(storeName);
        var blob = container.GetBlockBlobReference(filePath.ToLower());

        await blob.DeleteIfExistsAsync();
    }

    public async Task<bool> FileExists(string storeName, string filePath)
    {
        var container = _blobClient.GetContainerReference(storeName);
        var blob = container.GetBlockBlobReference(filePath.ToLower());

        return await blob.ExistsAsync();
    }

    public async Task<Stream> GetFile(string storeName, string filePath)
    {
        var container = _blobClient.GetContainerReference(storeName);
        var blob = container.GetBlockBlobReference(filePath.ToLower());

        var mem = new MemoryStream();
        await blob.DownloadToStreamAsync(mem);
        mem.Seek(0, SeekOrigin.Begin);

        return mem;
    }

    public async Task<string> GetFileUrl(string storeName, string filePath)
    {
        var container = _blobClient.GetContainerReference(storeName);
        var blob = container.GetBlockBlobReference(filePath.ToLower());
        string url = null;

        if (await blob.ExistsAsync())
        {
            url = blob.Uri.AbsoluteUri;
        }

        return url;
    }

    public async Task SaveFile(string storeName, string filePath, Stream fileStream, IDictionary<string, string> metadata)
    {
        var container = _blobClient.GetContainerReference(storeName);
        var blob = container.GetBlockBlobReference(filePath.ToLower());

        if (metadata != null)
        {
            foreach (var key in metadata)
            {
                if (!blob.Metadata.ContainsKey(key.Key))
                {
                    blob.Metadata.Add(key.Key, key.Value);
                }
                else
                {
                    blob.Metadata[key.Key] = key.Value;
                }
            }
        }

        await blob.UploadFromStreamAsync(fileStream);
        await blob.SetMetadataAsync();
    }

}