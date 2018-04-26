using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SClub.ShopSystem.Web.Models
{
    public class OrderDetails
    {
        
        public List<AddressInfo> AddressList { get; set;} =new List<AddressInfo>();
        public int GoodsId { get; set; }
        public string GoodsName { get; set; }
        public int Price { get; set; }
        public string Img { get; set; }
        public int BuyCount { get; set; }
        public int UserId { get; set; }
    }
}