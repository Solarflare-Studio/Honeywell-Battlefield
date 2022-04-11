using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Honeywell.Battlefield.Model
{
    public class Content
    {
        public string Title { get; set; }
        public string Body { get; set; }

        internal bool IsEmpty()
        {
            return string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Body);
        }
    }
}
