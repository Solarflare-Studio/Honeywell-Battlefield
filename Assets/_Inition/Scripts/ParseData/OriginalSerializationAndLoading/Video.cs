using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Honeywell.Battlefield.Model
{
    public class Video
    {
        public string Title { get; set; }
        public string ThumbnailPath { get; set; }
        public string VideoPath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
/*
        [XmlIgnore]
        public BitmapImage Thumbnail
        {
            get
            {
                var uri = PathHelper.GetUri(ThumbnailPath);

                if (uri != null)
                {
                    return new BitmapImage(uri);
                }

                return null;
            }
        }
        */
        [XmlIgnore]
        public Uri VideoUri
        {
            get
            {
                return PathHelper.GetUri(VideoPath);
            }
        }
    }
}
