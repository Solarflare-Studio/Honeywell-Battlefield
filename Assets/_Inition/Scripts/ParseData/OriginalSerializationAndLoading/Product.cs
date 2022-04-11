using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Honeywell.Battlefield.Model
{
    [XmlInclude(typeof(VideoAction))]
    [XmlInclude(typeof(GalleryAction))]
    [XmlInclude(typeof(TechSpecsAction))]
    public class Product
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }

        public Content[] Content { get; set; }

        public Video Video { get; set; }

        public Action[] Actions { get; set; }
    }

    public class ProductList : List<Product>
    {

    }
}
