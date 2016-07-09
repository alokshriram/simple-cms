using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simple_cms.Common
{
    public class GenericResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string ResponseDetail { get; set; }
    }
}