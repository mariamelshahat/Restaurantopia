﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantopia.Entities.Models
{
	public class Item
	{
		[Key]
		public int Id { get; set; }
		[StringLength ( 100 )]
		public string ItemTitle { get; set; }
		[StringLength ( 120 )]
		public string? ItemDescription { get; set; }
		[StringLength ( 300 )]
		public string ItemImage { get; set; }
		public decimal ItemPrice { get; set; }
		[StringLength ( 100 )]
		public string ItemStatus { get; set; }
		[StringLength ( 100 )]

		// Navigation property to Category
		[ForeignKey ( "Category" )]
		// Foreign key to Category
		public int CategoryId { get; set; }
		public Category Category { get; set; } // Many-to-One relationship with Category

		// Navigation property to Menu
		[ForeignKey ( "Menu" )]
		// Foreign key to Menu
		public int MenuId { get; set; } // Foreign key referencing Menu
		public Menu Menu { get; set; } // Many-to-One relationship with Menu

		// Navigation property to OrderDetails
		public ICollection<OrderDetails> OrderDetails { get; set; } // One-to-Many relationship with OrderDetails
	}
}