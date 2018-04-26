using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service
{
    public class MenuList
    {
        public int menuId { get; set; }
        public string menuName { get; set; }
        public Nullable<int> menuFatherId { get; set; }
        public string code { get; set; }
        public string url { get; set; }
        public List<MenuList> children { get; set; } = new List<MenuList>();
    }
}