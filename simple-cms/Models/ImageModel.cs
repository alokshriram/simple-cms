using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simple_cms.Models
{
    public class ImageModel
    {
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public byte[] Image { get; set;}
    }
}