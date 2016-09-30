namespace Ravlyk.Models
{
    public class DishModel 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool Favourite { get; set; }    
    }
}
