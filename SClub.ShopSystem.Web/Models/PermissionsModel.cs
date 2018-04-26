using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SClub.ShopSystem.Web.Models
{
    public class PermissionsModel
    {
        public int PermissionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public System.DateTime CreationTime { get; set; }
    }
}