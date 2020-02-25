using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    // internal; aynı namespace içerisinde erişilebilir anlamına gelmektedir.
    // protected; inherit edilen yerde kullanılabilir demektir.
    // private; sadece tanımlandığı yerde erişilebilir anlamına gelmektedir.
    public class Product : IEntity // public verildi. Bu nedenle diğer tarflardan da erişilebilecektir.
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }



    }
}
