using Caliburn.Micro;
namespace Ravlyk.Models
{
    public class OrderModel : PropertyChangedBase
    {
        public DishModel Dish { get; set; }
        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                NotifyOfPropertyChange(() => Count);
            }
        }       
    }
}