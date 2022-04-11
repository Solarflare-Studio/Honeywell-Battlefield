using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using Inition.HoneyWell.ParseData; 
using Inition.Utils.LoadImage; 

namespace Inition.Markers
{ 
    [System.Serializable]
    public class SetMarker_InteractableMenu : Base_SetMarker
    {
        [SerializeField]
        private string descriptionText = "DescriptionText";

        [SerializeField]
        private string tumbnailVideoButtonName = "TumbnailVideoButton";
        private Button tumbnailVideoButton;
        private GameObject Videogui;

        public override void SetData(DataObject _obj, Transform _transform)
        {
            _transform.GetChildFromName<Text>(descriptionText).text = "<size=22>" + _obj.title + "</size> \n\n";
            _transform.GetChildFromName<Text>(descriptionText).text += "<size=16>" + _obj.description.ToUpper() + "</size>";
            _transform.GetChildFromName<LoadImageToUI>(tumbnailVideoButtonName).ChangeImage("Content/" + _obj.video.videoPath);

            //Videogui = InstantiateVideo(_obj.video.thumbnailPath, tumbnailVideoButton.transform.position, tumbnailVideoButton.transform.rotation));
            tumbnailVideoButton = _transform.GetChildFromName<Button>(tumbnailVideoButtonName);
            tumbnailVideoButton.onClick.AddListener(() => InstantiateVideo(_obj.video.thumbnailPath, tumbnailVideoButton.transform));
            
        }

    }
}