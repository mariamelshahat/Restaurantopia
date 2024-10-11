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

		public Review Review { get; set; }

		[ForeignKey ( "Customer" )]
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		public OrderDetails OrderDetail { get; set; }

	}
}
