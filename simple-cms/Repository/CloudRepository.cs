using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace simple_cms.Repository
{
    public class CloudRepository : IContentRepository
    {
        private ISettingRepository settingRepository;
        private CloudStorageAccount cloudStorage;
        private CloudBlobClient cloudBlob;

        public CloudRepository(ISettingRepository settingRepository)
        {
            this.settingRepository = settingRepository;
            this.cloudStorage=CloudStorageAccount.Parse(settingRepository.ConnectionString);
            this.cloudBlob=this.cloudStorage.CreateCloudBlobClient();
        }

        public bool AreaExists(string areaName)
        {
            CloudBlobContainer container=this.cloudBlob.GetContainerReference(areaName.ToLower());
            return container.Exists();
        }

        public string AddNewFile(string areaName,string file)
        {
            CloudBlobContainer container = this.cloudBlob.GetContainerReference(areaName);
            string blobName = "tempblob";
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            if(!blockBlob.Exists())
            {
                using(FileStream fs= System.IO.File.OpenRead(file))
                {
                    blockBlob.UploadFromStream(fs);
                }
            }
            return blockBlob.Uri.ToString();
        }

        public string AddImage(Image image, string sectionName,string path,string fileName,string mediaType)
        {
            CloudBlobContainer container = this.CreateNewArea(sectionName);
            string blobName = path + "/" + fileName;
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            if (!blockBlob.Exists())
            {
                    byte[] imageArray=this.ImageToByteArray(image);
                    blockBlob.Properties.ContentType = mediaType;
                    blockBlob.UploadFromByteArray(imageArray, 0, imageArray.Length);
            }
            return blockBlob.Uri.ToString();
        }

        public List<string> GetAllAreas()
        {
            IEnumerable<string> container = this.cloudBlob.ListContainers().Select(a => a.Name);
            return container.ToList();
        }

        public List<string> GetUrlsForArea(ContentType contentType)
        {
            List<string> allUrls=new List<string>();
            allUrls= GetUrlsForSubPath(contentType.ToString().ToLower());
            return allUrls;            
        }

        private List<string> GetUrlsForSubPath(string path)
        {
            List<string> allUrls = new List<string>();
            IEnumerable<CloudBlobContainer> containers = this.cloudBlob.ListContainers(path);
            foreach(CloudBlobContainer container in containers)
            {
                allUrls.AddRange(container.ListBlobs(useFlatBlobListing: true).Select(a => a.Uri.ToString()));
                //allUrls.AddRange(container.ListBlobs().Select(a => a.Uri.ToString()));
            }
            return allUrls;
        }

        private byte[] ImageToByteArray(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            return ms.ToArray();
        }

        /// <summary>
        /// An area is a logical group of some pieces of content
        /// </summary>
        /// <param name="areaName"></param>
        private CloudBlobContainer CreateNewArea(string areaName)
        {
            CloudBlobContainer container = this.cloudBlob.GetContainerReference(areaName.ToLower());
            if (!container.Exists())
            {
                container.CreateIfNotExists();
            }
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            return container;
        }

    }
}