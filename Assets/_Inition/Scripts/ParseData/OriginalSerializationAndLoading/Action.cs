using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Honeywell.Battlefield.Model
{
    public abstract class Action
    {
        public string Caption { get; set; }
        //public ActionType Type { get; set; }
    }

    public class VideoAction : Action
    {
        public string VideoPath { get; set; }

        [XmlIgnore]
        public Uri VideoUri
        {
            get
            {
                return PathHelper.GetUri(VideoPath);
            }
        }
    }

    public class GalleryAction : Action
    {
        public string[] ImagePaths { get; set; }
    }

    public class TechSpecsAction : Action
    {
        public string[] ImagePaths { get; set; }
    }

    public class NullAction : Action
    {
    }
}
