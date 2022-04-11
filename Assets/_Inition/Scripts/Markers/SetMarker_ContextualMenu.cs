using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inition.HoneyWell.ParseData;
using UnityEngine.UI;

namespace Inition.Markers
{
    [System.Serializable]
    public class SetMarker_ContextualMenu : Base_SetMarker
    {
        [SerializeField]
        private string twoButtonConfigurationName = "ContextualMenu_TwoButtonConfiguration";
        [SerializeField]
        private string threeButtonConfigurationName = "ContextualMenu_ThreeButtonConfiguration";
        [SerializeField]
        private string fourButtonConfigurationName = "ContextualMenu_FourButtonConfiguration";
        [SerializeField]
        private string imageGalleryButtonName = "ImageGalleryButton";
        [SerializeField]
        private string infoFormButtonName = "InfoFormButton";
        [SerializeField]
        private string techSpecsButtonName = "TechSpecsButton";
        [SerializeField]
        private string firstVideoButtonName = "FirstContextualVideo";
        [SerializeField]
        private string secondVideoButtonName = "SecondContextualVideo";

        private Transform[] configHolders = new Transform[3];   //because we have 3 possible configurations

        public override void SetData(DataObject _obj, Transform _transform)
        { 
            configHolders[0] = _transform.GetChildFromName<Transform>(twoButtonConfigurationName);
            configHolders[1] = _transform.GetChildFromName<Transform>(threeButtonConfigurationName);
            configHolders[2] = _transform.GetChildFromName<Transform>(fourButtonConfigurationName);

            int configHolderIndex = _obj.actions.Count - 2;
            ToggleProperConfigurationHolder(configHolderIndex); 

            switch (_obj.actions.Count - 1)
            {
                case 1: 
                    SetGallery(imageGalleryButtonName, _obj.actions[0].imagePaths, configHolders[configHolderIndex].GetChildFromName<Transform>(imageGalleryButtonName), _obj.actions[0].caption); 
                    break;
                case 2:
                    SetVideo(firstVideoButtonName, _obj.actions[0].videoPath, configHolders[configHolderIndex].GetChildFromName<Transform>(firstVideoButtonName), _obj.actions[0].caption);
                    SetGallery(imageGalleryButtonName, _obj.actions[1].imagePaths, configHolders[configHolderIndex].GetChildFromName<Transform>(imageGalleryButtonName), _obj.actions[1].caption);
                    break;
                case 3:
                    SetVideo(firstVideoButtonName, _obj.actions[0].videoPath, configHolders[configHolderIndex].GetChildFromName<Transform>(firstVideoButtonName), _obj.actions[0].caption);
                    SetVideo(secondVideoButtonName, _obj.actions[1].videoPath, configHolders[configHolderIndex].GetChildFromName<Transform>(secondVideoButtonName), _obj.actions[1].caption);
                    SetGallery(imageGalleryButtonName, _obj.actions[2].imagePaths, configHolders[configHolderIndex].GetChildFromName<Transform>(imageGalleryButtonName), _obj.actions[2].caption);
                    break;
            }
            Button infoFormButton = _transform.GetChildFromName<Button>(infoFormButtonName);
            infoFormButton.onClick.AddListener(() => InstantiateContactForm(infoFormButton.transform, _obj.title));

            Button techSpecsButton = _transform.GetChildFromName<Button>(techSpecsButtonName); 
            techSpecsButton.onClick.AddListener(() => InstantiateTechSpecs(_obj.title, _obj.actions[_obj.actions.Count-1].imagePaths, techSpecsButton.transform));
        }

        void ToggleProperConfigurationHolder(int _index)
        { 
            for(int i = 0; i < configHolders.Length; i++)
            {
                configHolders[i].gameObject.SetActive(false);    
            }
            configHolders[_index].gameObject.SetActive(true); 
        }

        void SetVideo(string _videoButtonName, string _videoPath, Transform _parent, string _buttonText)
        {
            Button videoButton = _parent.GetChildFromName<Button>(_videoButtonName);
            videoButton.transform.GetComponentInChildren<Text>().text = _buttonText.ToUpper();
            videoButton.onClick.AddListener(() => InstantiateVideo(_videoPath, _parent));
        }

        void SetGallery(string _galleryButtonName, string[] _imagePaths, Transform _parent, string _buttonText)
        {
            Button imageGalleryButton = _parent.GetChildFromName<Button>(_galleryButtonName);
            imageGalleryButton.transform.GetComponentInChildren<Text>().text = _buttonText.ToUpper();
            imageGalleryButton.onClick.AddListener(() => InstantiatePhotos(_imagePaths, _parent));
        }
    }
}