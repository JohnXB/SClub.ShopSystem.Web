using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SClub.ShopSystem.Web.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string GoodsName { get; set; }
        public int Price { get; set; }
        public int SalesCount { get; set; }
        public String OrderTime { get; set; }
        public string OrderState { get; set; }
        public int GoodsId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Consignee { get; set; }
        public string DeliveryTime { get; set; }
        public string ReceiptGoodsTime { get; set; }
    }
}