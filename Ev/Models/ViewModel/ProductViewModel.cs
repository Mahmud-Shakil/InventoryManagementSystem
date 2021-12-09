using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ev.Models.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
    }
}