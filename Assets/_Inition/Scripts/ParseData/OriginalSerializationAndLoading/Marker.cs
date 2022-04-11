using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Honeywell.Battlefield.Model
{
    public class Marker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BackgroundPath { get; set; }
        public string[] ProductPaths { get; set; }

        
        public Content PlatformContent { get; set; }

        [XmlIgnore]
        public IEnumerable<Product> Products
        {
            get
            {
                return _products;
            }
        }

        private List<Product> _products = new List<Product>();

        internal void LoadProducts(ref Dictionary<string, Product> productCache) 
        {
            XmlSerializer productSerializer = new XmlSerializer(typeof(Product));

            foreach (var path in this.ProductPaths)
            {
                var productPath = PathHelper.GetPath(path);

                // look for it in the cache
                if (productCache.ContainsKey(path))
                {
                    _products.Add(productCache[path]);
                }
                else
                {
                    if (File.Exists(productPath))
                    {
                        var reader = new XmlTextReader(productPath);

                        try
                        {

                            var product = productSerializer.Deserialize(reader) as Product;

                            if (product != null)
                            {
                                productCache[path] = product;

                                _products.Add(product);

                                Repository.GetInstance().AddLoadedProduct(product);
                            }
                        }
                        catch (Exception)
                        {
                           
                        }
                    }
                }
            }


        }
    }
}
