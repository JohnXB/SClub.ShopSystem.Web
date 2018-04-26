using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SClub.ShopSystem.Web.Models
{
    public class Permission
    { 
        public int id { set; get; }
        public int pId { set; get; }
        public string name { set; get; }
        public bool open { set; get; }
        public bool @checked { set; get; }

    }
}