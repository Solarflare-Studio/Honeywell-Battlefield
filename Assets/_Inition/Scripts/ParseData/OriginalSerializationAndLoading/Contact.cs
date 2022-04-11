using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Honeywell.Battlefield.Model
{
    public class Contact
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string JobTitle { get; set; }
        public RequestedProduct[] RequestedProducts { get; set; }
    }

    public class RequestedProduct
    {
        public string Name { get; set; }
        public bool IsRequested { get; set; }
    }
}
