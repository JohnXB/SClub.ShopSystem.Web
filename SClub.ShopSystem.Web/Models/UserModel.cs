using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SClub.ShopSystem.Web.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string CreationTime { get; set; }
        public string UserImg { get; set; }
    }
}