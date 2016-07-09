using simple_cms.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;


namespace simple_cms.Manager
{
    public interface IContentManager
    {
        List<string> GetAllAreas();

        List<string> GetUrlsForArea(ContentType type);

        GenericResponse AddImage(Image image,string imagePath,string mediaType);
    }
}