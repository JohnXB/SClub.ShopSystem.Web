using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SClub.ShopSystem.Web.Models
{
    public class UserManageModel
    {
        public List<RoleModel> AllRoles { get; set; } = new List<RoleModel>();
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string CreationTime { get; set; }
    }
}