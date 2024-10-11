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
        public DateTime Date { get; set; } = DateTime.Now;
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [NotMapped]
        public List<Item> itemList { get; set; }
    }
}
