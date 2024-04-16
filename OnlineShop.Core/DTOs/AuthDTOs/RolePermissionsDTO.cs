using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.DTOs.AuthDTOs
{
    public class RolePermissionsDTO
    {
        public string RoleId { get; set; }
        public List<string> Permissions { get; set; }
    }
}
