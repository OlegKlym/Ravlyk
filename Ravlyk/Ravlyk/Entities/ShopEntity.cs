using SQLite;
using System.Collections.Generic;

namespace Ravlyk.Models
{
    [Table("Shops")]
    public class ShopEntity
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Address { get; set; }
        public string WorkTime { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
