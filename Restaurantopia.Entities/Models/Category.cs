using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantopia.Entities.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		public string CategoryName { get; set; }
		public string CategoryDescription { get; set; }
		// Navigation property to list of items in this category h
		public ICollection<Item> Items { get; set; } // One-to-Many relationship with Item
	}
}
