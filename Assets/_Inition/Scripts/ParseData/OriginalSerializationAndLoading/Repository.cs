using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Honeywell.Battlefield.Model;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Configuration;

using UnityEngine;

namespace Honeywell.Battlefield
{
    public class Repository
    {
        private IEnumerable<Marker> markers;
        private Dictionary<string, Product> products = new Dictionary<string, Product>();

        private static Repository _instance;
        public static Repository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Repository(); 
                _instance.LoadData();
            }

            return _instance;
        }

        private Repository()
        {
            //LoadData();
        }

        private void LoadData()
        {
            XmlSerializer markersSerializer = new XmlSerializer(typeof(Marker[]));

            var markersPath = PathHelper.GetPath("Markers.xml");

            markers = markersSerializer.Deserialize(new XmlTextReader(markersPath)) as Marker[];

            foreach (var marker in markers)
            {
                marker.LoadProducts(ref products); 
            }
        }

        public Marker GetMarker(long id)
        {
            return markers.Where(m => m.Id == id).FirstOrDefault();
        }

        public IEnumerable<Marker> AllMarkers
        {
            get
            {
                return markers;
            }
        }

        public bool Exists(long id)
        {
            bool exists = false;
            foreach (Model.Marker marker in Repository.GetInstance().AllMarkers)
            {
                if (marker.Id == id)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        private List<Product> _products = new List<Product>();

        public IEnumerable<Product> AllProducts
        {
            get
            {
                return _products;
            }
        }

        internal void AddLoadedProduct(Product product)
        {
            if (_products.Count(p => p.Id == product.Id) == 0)
            {
                _products.Add(product);
            }
        }
    }
}
