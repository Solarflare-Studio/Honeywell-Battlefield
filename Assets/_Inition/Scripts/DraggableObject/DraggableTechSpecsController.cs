using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inition.UI.DraggableObjects;
using UnityEngine.UI;
using System.IO;

namespace Inition.Draggables.TechSpecs
{
    public class DraggableTechSpecsController : DraggableObjectController
    { 
        [SerializeField]
        private Sprite[] textures;
        private int current = 0;
        private DraggableTechSpecsManager techSpecsManager;

        [SerializeField]
        private string imageName = "ButtonBack";
        private Image image;

        [SerializeField]
        private string backButtonName = "ButtonBack";
        private Button backButton;

        [SerializeField]
        private string forwardButtonName = "ButtonForward";
        private Button forwardButton;

        [SerializeField]
        private string closeButtonName = "CloseButton";
        private Button closeButton;

        [SerializeField]
        private float maximumStartWidth = 1200.0f;
        [SerializeField]
        private float maximumStartHeight = 800.0f;

        private string path;



        public void Initialize(string[] _path)
        {
            // Use the first image path to identify the object
            path = _path[0]; 

            techSpecsManager = DraggableTechSpecsManager.Instance;
            techSpecsManager.AddDraggableTechSpecs(this);

            backButton = transform.GetChildFromName<Button>(backButtonName);
            backButton.onClick.AddListener(() => ChangeSlide(-1));

            forwardButton = transform.GetChildFromName<Button>(forwardButtonName);
            forwardButton.onClick.AddListener(() => ChangeSlide(1));

            closeButton = transform.GetChildFromName<Button>(closeButtonName);
            closeButton.onClick.AddListener(() => CloseButtonPressed());

            image = transform.GetChildFromName<Image>(imageName);
             
            StartCoroutine("LoadAllSprite", _path);
        } 

        private IEnumerator LoadAllSprite(string[] _absoluteFolderPath)
        {
            //find how many files are there and make textures =  Sprite[file count]...
            //create a loop to load all files 
            textures = new Sprite[_absoluteFolderPath.Length];
            int index = 0;
            for (int i = 0; i < _absoluteFolderPath.Length; i++)
            {
                WWW localFile;
                localFile = new WWW("file://" + Application.streamingAssetsPath + "/Content/" + _absoluteFolderPath[i]);
                yield return localFile;

                textures[index] = Sprite.Create(localFile.texture, new Rect(0, 0, localFile.texture.width, localFile.texture.height), Vector2.zero);
                index++; 
            }

            current = 0;
            UpdateImage(current);
            SetButtonStatus(current);

            GetComponent<CanvasGroup>().alpha = 1f;
        }

        private void ChangeSlide(int _addToCurrent)
        { 
            if(current >-1 && current < textures.Length)
            {
                current += _addToCurrent;
                UpdateImage(current);
                SetButtonStatus(current);
            } 
        }

        void UpdateImage(int _current)
        {
            image.sprite = textures[current];
            float width = textures[current].texture.width;
            float height = textures[current].texture.height;
            float aspectRatio = width / height;
            if (width > maximumStartWidth)
            {
                width = maximumStartWidth;
                height = width / aspectRatio;
            }
            if (height > maximumStartHeight)
            {
                height = maximumStartHeight;
                width = height * aspectRatio;
            }
            image.rectTransform.sizeDelta = new Vector2(width, height);
        }

        private void SetButtonStatus(int _current)
        {
            if(_current <= 0)
                backButton.interactable = false;
            else
                backButton.interactable = true;

            if (_current >= textures.Length - 1)
                forwardButton.interactable = false;
            else
                forwardButton.interactable = true;
        }

        public void CloseButtonPressed()
        {
            Destroy(gameObject);
        }
         
        public override void BringToFront()
        {
            base.BringToFront(); 
        }

        public string GetPath()
        {
            return path;
        }

    }
}