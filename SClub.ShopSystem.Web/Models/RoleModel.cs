using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SClub.ShopSystem.Web.Models
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CreationTime { get; set; }
    }
}