using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SClub.ShopSystem.Web.Models
{
    public class CarouselModel
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public int GoodsId { get; set; }
    }
}