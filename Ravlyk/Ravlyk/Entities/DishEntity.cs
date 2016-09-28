using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ravlyk.Entities
{
    [Table("Dishes")]
    public class DishEntity
    {
        [PrimaryKey, Column("_id")]
        public int Id_Dish { get; set; }

        public int Id_Category { get; set; }
        public int Id_Shop { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool Favourite { get; set; }
    }
}
