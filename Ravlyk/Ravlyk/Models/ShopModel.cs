using System.Collections.Generic;

namespace Ravlyk.Models
{

    public class ShopModel
    {
        public int Id { get; set; }
       
        public string Title { get; set; }
        public string Address { get; set; }
        public string WorkTime { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}
