using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;
using System;
using System.IO;
using Inition.Utils.LoadImage;
using Inition.UI.DraggableObjects;

namespace Inition.Draggables.Photos
{
    public class DraggableGalleryController : DraggableObjectController
    { 
        private DraggableGalleryManager galleryManager;
        private string path;
        LoadImageToUI imageui;

        [SerializeField]
        private string closeButtonName = "CloseButton";
        private Button closeButtonRef;

        public void Initialize(string _gallerytitle)
        {
            closeButtonRef = transform.GetChildFromName<Button>(closeButtonName);
            closeButtonRef.onClick.AddListener(() => CloseButtonPressed());
                    
            path = _gallerytitle;
            galleryManager = DraggableGalleryManager.Instance;
            galleryManager.AddDraggablePhotos(this);
            transform.GetComponent<LoadGalleryToUI>().ChangeImage("Content/" + path); 
        }
        
        private void OnDestroy()
        {
            galleryManager.RemoveDraggablePhotos(this);
        }

        public void CloseButtonPressed()
        { 
            Destroy(gameObject);
        }

        internal string GetPath()
        {
            return path;
        } 
    } 
}

