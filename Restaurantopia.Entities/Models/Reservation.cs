using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantopia.Entities.Models
{
	public class Reservation
	{
		[Key]
		public int Reservation_Id { get; set; }
		public int Customer_Id { get; set; }
		public int NofGuest { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
		[StringLength ( 30 )]
		public string Status { get; set; }

		// Navigation property to Customer
		[ForeignKey ( "Customer" )]
		// Foreign key to Customer
		public int CustomerId { get; set; }
		public Customer Customer { get; set; } // Many-to-One relationship with Customer
	}
}
