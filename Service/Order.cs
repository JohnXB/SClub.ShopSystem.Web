//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Service
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.AdminRecord = new HashSet<AdminRecord>();
        }
    
        public int OrderId { get; set; }
        public string GoodsName { get; set; }
        public int Price { get; set; }
        public int SalesCount { get; set; }
        public Nullable<System.DateTime> OrderTime { get; set; }
        public string OrderState { get; set; }
        public int GoodsId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Consignee { get; set; }
        public Nullable<System.DateTime> DeliveryTime { get; set; }
        public Nullable<System.DateTime> ReceiptGoodsTime { get; set; }
    
        public virtual Goods Goods { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdminRecord> AdminRecord { get; set; }
    }
}
