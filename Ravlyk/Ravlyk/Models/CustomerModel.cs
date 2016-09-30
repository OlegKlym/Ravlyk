using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ravlyk.Models
{
    public class CustomerModel
    {
        public int customer_group_id { get; set; }
        public string address_1 { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string telephone { get; set; }
        public string city { get; set; }

    }
}
