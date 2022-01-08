using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotcom.Models
{
    public class CartProduct
    {
        public int Id { get; set; }
        public int UId { get; set; }
        public int PId { get; set; }
        public decimal Quantity { get; set; }
    }
}
