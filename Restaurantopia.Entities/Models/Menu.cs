using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantopia.Entities.Models
{
	public class Menu
	{
		[Key]
		public int Id { get; set; }
		[StringLength ( 50 )]
		public string Description { get; set; }
		[StringLength ( 300 )]
		public string Image { get; set; }
		[StringLength ( 50 )]

		public string MenuName { get; set; }

		// Navigation property to Items
		public ICollection<Item> Items { get; set; } // One-to-Many relationship with Items
	}
}
