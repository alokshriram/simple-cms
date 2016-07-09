using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simple_cms.Repository
{
    public class CloudSettings :ISettingRepository
    {
        public string ConnectionString
        {
            get
            {
                return CloudConfigurationManager.GetSetting("StorageConnectionString");
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}