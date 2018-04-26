using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SClub.ShopSystem.Web.Models
{
    public class CartModel
    {
        public int CartId { get; set; }
        public int GoodsId { get; set; }
        public int UserId { get; set; }
        public string GoodsName { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string SaleTime { get; set; }
        public string GoodsStyle { get; set; }
        public string Img { get; set; }

    }
}