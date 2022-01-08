using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotcom.Models
{
    public class Product
    {
		public int Id { get; set; }
		public int UId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal Quantity { get; set; }
	}
}
