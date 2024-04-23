using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Entities
{
    public class Banner : UploadedFile
    {
        public string SiteSettingId { get; set; }
        public SiteSetting SiteSetting { get; set; }
    }
}
