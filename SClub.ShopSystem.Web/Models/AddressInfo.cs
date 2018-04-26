using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SClub.ShopSystem.Web.Models
{
    public class AddressInfo
    {
        public int InfoId { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Consignee { get; set; }
    }
}