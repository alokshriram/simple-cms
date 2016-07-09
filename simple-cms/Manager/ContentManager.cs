namespace simple_cms.Manager
{
    using simple_cms.Common;
    using simple_cms.Repository;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Web;

    public class ContentManager :IContentManager
    {
        private IContentRepository repository;
        private static Random random = new Random();

        public ContentManager(IContentRepository repository)
        {
            this.repository = repository;
        }

        public List<string> GetAllAreas()
        {
            List<string> areas=new List<string>();
            foreach(string val in  Enum.GetNames(typeof(ContentType)))
            {
                areas.Add(val);
            }
            return areas;
        }

        public List<string> GetUrlsForArea(ContentType type)
        {
            return this.repository.GetUrlsForArea(type);           
        }

        public GenericResponse AddImage(Image image,string imagePath,string mediaType)
        {
            string imageKey = this.randomString(15);
            switch (mediaType)
            {
                case "image/jpg":
                case "image/jpeg":
                    imageKey=imageKey+".jpeg";
                    break;
                case "image/png":
                    imageKey=imageKey+".png";
                    break;
                case "image/gif":
                    imageKey=imageKey+".gif";
                    break;
                default :
                    return new GenericResponse(){ ResponseStatus=ResponseStatus.MediaTypeNotSupported};
            }
            string url = this.repository.AddImage(image, ContentType.Image.ToString(),imagePath, imageKey,mediaType);
            return new GenericResponse { ResponseDetail = url, ResponseStatus = ResponseStatus.Success };
        }

        private string randomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}