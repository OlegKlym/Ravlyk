using SQLite;

namespace Ravlyk.Entities
{
    [Table("Categories")]
    public class CategoryEntity
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id_Category { get; set; }

        public int Id_Shop { get; set; }

        public string Title { get; set; }
        public string ImagePath { get; set; }
       
    }
}



