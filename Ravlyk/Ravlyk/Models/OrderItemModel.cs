using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ravlyk.Models
{
    public class OrderItemModel
    {
        public DishModel Dish { get; set; }
        public int Count { get; set; }
    }
}
