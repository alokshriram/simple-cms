using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_cms.Repository
{
    public interface ISettingRepository
    {
        string ConnectionString { get; set; }
    }
}
