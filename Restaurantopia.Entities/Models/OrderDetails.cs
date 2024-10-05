using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantopia.Entities.Models
{
	public class OrderDetails
	{
		public int Id { get; set; }
		public int Total { get; set; }
		public string Payment { get; set; }
		public string Status { get; set; }
		// Navigation property to Order
		[ForeignKey ( "Order" )]
		// Foreign key to Order
		public int OrderId { get; set; }
		public Order Order { get; set; } // Many-to-One relationship with Order

		// Navigation property to Item
		[ForeignKey ( "Item" )]
		// Foreign key to Item
		public int ItemId { get; set; }
		public Item Item { get; set; } // Many-to-One relationship with Item
	}
}
