using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml; 
using System.Xml.Serialization;

namespace Inition.HoneyWell.ParseData
{
    public class LoadAndParseData : Singleton <LoadAndParseData>
    {
        protected LoadAndParseData() { }

        //my attempt of parsing through the original xml files and ending up with one list that holds all info to be displayed...
        public List<DataObject> loadedDataObjects; 

        private void Start()
        {
            loadedDataObjects = new List<DataObject>();
            var repo = Honeywell.Battlefield.Repository.GetInstance();
            
            foreach (Honeywell.Battlefield.Model.Product product in repo.AllProducts)
            { 
                DataObject obj = new DataObject(product.Id, product.Title, product.Subtitle, product.Description);
                Debug.Log(product.Title);
                for(int i = 0; i < product.Content.Length; i++)
                {
                    obj.contents.Add(new DataObject_Content(product.Content[i].Title, product.Content[i].Body));
                }

                obj.video = new DataObject_VideoMain(product.Video.Title, product.Video.VideoPath, product.Video.ThumbnailPath); 

                for (int i = 0; i < product.Actions.Length; i++)
                {
                    switch(product.Actions[i].GetType().ToString())
                    {   
                        case "Honeywell.Battlefield.Model.VideoAction": 
                            Honeywell.Battlefield.Model.VideoAction vidAction = product.Actions[i] as Honeywell.Battlefield.Model.VideoAction;   
                            Actions actionVid = new Actions(Actions.ActionType.VideoAction, vidAction.Caption, vidAction.VideoPath);
                            obj.actions.Add(actionVid); 
                            break;
                        case "Honeywell.Battlefield.Model.GalleryAction":
                            Honeywell.Battlefield.Model.GalleryAction galleryAction = product.Actions[i] as Honeywell.Battlefield.Model.GalleryAction;
                            Actions actionGallery = new Actions(Actions.ActionType.GalleryAction, galleryAction.Caption, galleryAction.ImagePaths);
                            obj.actions.Add(actionGallery);
                            break;
                       case "Honeywell.Battlefield.Model.TechSpecsAction": 
                            Honeywell.Battlefield.Model.TechSpecsAction techSpecsAction = product.Actions[i] as Honeywell.Battlefield.Model.TechSpecsAction;
                            Actions techSpecsGallery = new Actions(Actions.ActionType.TechSpecsAction, techSpecsAction.Caption, techSpecsAction.ImagePaths);
                            obj.actions.Add(techSpecsGallery);
                            break;
                    } 
                }
                loadedDataObjects.Add(obj);
            } 
            //please rewrite to make independant of order...

            int index = 0;
            foreach (Honeywell.Battlefield.Model.Marker marker in repo.AllMarkers)
            { 
                string title = "";
                string body = "";
                try
                {
                    title = marker.PlatformContent.Title;
                    body = marker.PlatformContent.Body;
                }
                catch { }

                loadedDataObjects[index].SetMarkerInfo(marker.Id, marker.Name, title, body);
                index++;
            }  
        }

        public DataObject GetObjectViaMarkerID(int _id)
        {
            for(int i = 0; i < loadedDataObjects.Count; i++)
            {
                if(_id == loadedDataObjects[i].markerID)
                {
                    return loadedDataObjects[i];
                }
            }
            return null;
        }

        public DataObject GetObjectViaNameID(string _id)
        {
            for (int i = 0; i < loadedDataObjects.Count; i++)
            {
                if (_id == loadedDataObjects[i].id)
                {
                    return loadedDataObjects[i];
                }
            }
            return null;
        }
    }
}
