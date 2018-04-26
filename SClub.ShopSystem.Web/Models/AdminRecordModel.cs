using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SClub.ShopSystem.Web.Models
{
    public class AdminRecordModel
    {
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public string RecordName { get; set; }
        public String RecordTime { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> GoodsId { get; set; }
    }
}