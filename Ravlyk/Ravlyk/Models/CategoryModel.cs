using Ravlyk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ravlyk.Models
{
    public class CategoryModel
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public List<DishViewModel> Dishes { get; set; }
    }
}
