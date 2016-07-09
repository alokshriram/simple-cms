using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simple_cms.Common
{
    public enum ResponseStatus
    {
        Success = 1,
        Failure = 2,
        AlreadyExists = 3,
        MediaTypeNotSupported = 4
    }
}