using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using System.Xml.Serialization;

namespace Inition.HoneyWell.ParseData
{
    //please look at the xml files

    [System.Serializable]
    public class DataObject
    { 
        public string id;
        public string title;
        public string subtitle;
        public string description;

        public List<DataObject_Content> contents = new List<DataObject_Content>();
        public DataObject_VideoMain video;
        public List<Actions> actions = new List<Actions>();

        public DataObject (string _id, string _title, string _subtitle, string _description)
        {
            id = _id;
            title = _title;
            subtitle = _subtitle;
            description = _description;
        }

        //marker data
        public int markerID;
        public string markerName;
        public string markerTitle;
        public string markerBody;
        //public string markerBackgroundPath;   //do we need this one???

        public void SetMarkerInfo(int _markerID, string _markerName, string _markerTitle, string _markerBody)
        {
            markerID = _markerID;
            markerName = _markerName;
            markerTitle = _markerTitle;
            markerBody = _markerBody;
        }
    }

    [System.Serializable]
    public class DataObject_Content
    { 
        public string title;
        public string body;

        public DataObject_Content(string _title, string _body)
        {
            title = _title;
            body = _body;
        }
    }  

    [System.Serializable]
    public class DataObject_VideoMain
    {
        public string title;
        public string thumbnailPath;
        public string videoPath;

        public DataObject_VideoMain(string _title, string _thumbnailPath, string _videoPath)
        {
            title = _title;
            thumbnailPath = _thumbnailPath;
            videoPath = _videoPath;
        } 
    }
     
    [System.Serializable]
    public class Actions
    {
        public enum ActionType { VideoAction, GalleryAction, TechSpecsAction }
        public ActionType type = ActionType.GalleryAction;
        public string caption;
        public string videoPath;
        public string[] imagePaths;

        public Actions(ActionType _type, string _caption, string _videoPath)
        {
            type = _type;
            caption = _caption;
            videoPath = _videoPath;
        }

        public Actions(ActionType _type, string _caption, string[] _imagePaths)
        {
            type = _type;
            caption = _caption; 
            imagePaths = _imagePaths;
        } 
    }
}