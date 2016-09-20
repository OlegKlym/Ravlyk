using System.Collections.Generic;

namespace Ravlyk.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public List<DishModel> Dishes { get; set; }
    }
}
