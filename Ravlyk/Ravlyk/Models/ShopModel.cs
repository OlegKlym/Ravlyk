using Ravlyk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ravlyk.Models
{
    public class ShopModel
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string WorkTime { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<CategoryViewModel> Categories
        {
            get; set;
        }
    }
}
