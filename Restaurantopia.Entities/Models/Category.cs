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
		[StringLength ( 50 )]
		public string CategoryName { get; set; }

		// Navigation property to Items
		public ICollection<Item> Items { get; set; } // One-to-Many relationship with Items
	}
}
