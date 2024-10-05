﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantopia.Entities.Models
{
	public class Customer
	{
		[Key]
		public int Id { get; set; }
		[StringLength ( 50 )]
		public string Name { get; set; }
		[StringLength ( 50 )]
		public string Email { get; set; }
		[StringLength ( 120 )]
		public string Password { get; set; }
		[StringLength ( 10 )]
		public string PhoneNumber { get; set; }
		[StringLength ( 150 )]
		public string Address { get; set; }
		// Navigation property to Orders
		public ICollection<Order> Orders { get; set; } // One-to-Many relationship with Orders
													   // Navigation property to Reservation
		public ICollection<Reservation> Reservations { get; set; } // One-to-Many relationship with Reservation

	}
}