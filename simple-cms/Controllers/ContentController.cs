namespace simple_cms.Controllers
{
    using simple_cms.Common;
    using simple_cms.Manager;
    using simple_cms.Models;
    using simple_cms.Repository;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Hosting;
    using System.Web.Http;

    public class ContentController : ApiController
    {
        private IContentManager manager;
        public ContentController(IContentManager manager)
        {
            this.manager = manager;
        }

        [Route("api/content/areas"), HttpGet]
        public HttpResponseMessage GetAllAreas()
        {
            List<string> allAreas = this.manager.GetAllAreas();
            ApiResponse<List<string>> response = new ApiResponse<List<string>> { Message = "Ok", Payload = allAreas };
            return Request.CreateResponse<ApiResponse<List<string>>>(HttpStatusCode.OK, response);
        }

        [Route("api/content/area/{areaName}"), HttpGet]
        public HttpResponseMessage GetArea(string areaName)
        {
            ContentType type;
            if (Enum.TryParse(areaName, true, out type))
            {
                List<string> allAreas = this.manager.GetUrlsForArea(type);
                ApiResponse<List<string>> response = new ApiResponse<List<string>> { Message = "Ok", Payload = allAreas };
                return Request.CreateResponse<ApiResponse<List<string>>>(HttpStatusCode.OK, response);
            }
            else
            {
                ApiResponse<List<string>> response = new ApiResponse<List<string>> { Message = "Area not found: "+areaName, Payload =null };
                return Request.CreateResponse<ApiResponse<List<string>>>(HttpStatusCode.NotFound, response);
            }
        }

        /// <summary>
        /// Sample
        /// api/content/area/{areaName}?imagePath=
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="imageType"></param>
        /// <returns></returns>
        [Route("api/content/{areaName}"), HttpPost]
        public HttpResponseMessage AddImage(string areaName, [FromUri] string imagePath)
        {
            ContentType type;
            if (Enum.TryParse(areaName, true, out type))
            {

                Stream actualStream = Request.Content.ReadAsStreamAsync().Result;
                string mediaType = Request.Content.Headers.ContentType.MediaType;
                Image image = Image.FromStream(actualStream);
                GenericResponse response=this.manager.AddImage(image, imagePath, mediaType);
                if (response.ResponseStatus == ResponseStatus.Success)
                {
                    return Request.CreateResponse<string>(HttpStatusCode.OK, response.ResponseDetail);
                }
                else
                {
                    return Request.CreateResponse<string>(HttpStatusCode.BadRequest, response.ResponseStatus.ToString());
                }
            }
            else
            {
                return Request.CreateResponse<ApiResponse<List<string>>>(HttpStatusCode.NotFound, null);
            }
        }

    }
}