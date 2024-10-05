using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantopia.Entities.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;

		public int Customer_Id { get; set; }

		public Review Review { get; set; } //(1->1)

		// Navigation property to Customer
		[ForeignKey ( "Customer" )] // Many-to-One
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		// Navigation property to the associated OrderDetail
		public OrderDetails OrderDetail { get; set; } // One-to-One relationship

	}
}
