using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SClub.ShopSystem.Web.Models
{
    public class GoodsModel
    {
        public int GoodsId { get; set; }
        public string GoodsName { get; set; }
        public string GoodsIntro { get; set; }
        public int GoodsCount { get; set; }
        public int Price { get; set; }
        public int SalesCount { get; set; }
        public string SaleTime { get; set; }
        public bool IfSale { get; set; }
        public int PurchasePrice { get; set; }
        public string GoodsStyle { get; set; }
        public string Img { get; set; }
    }
}