using System;
using System.Collections.Generic;
using System.Drawing;
namespace simple_cms.Repository
{
    public interface IContentRepository
    {
        string AddNewFile(string areaName, string file);
        bool AreaExists(string areaName);
        List<string> GetAllAreas();
        List<string> GetUrlsForArea(ContentType type);
        string AddImage(Image image, string sectionName,string path, string fileName,string mediaType);
    }
}