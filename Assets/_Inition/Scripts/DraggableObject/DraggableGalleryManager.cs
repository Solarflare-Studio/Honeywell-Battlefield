using Inition.ContactForm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inition.Draggables.Photos
{
    public class DraggableGalleryManager : Singleton<DraggableGalleryManager>
    {
        private List<DraggableGalleryController> photos = new List<DraggableGalleryController>();
        
        public void AddDraggablePhotos(DraggableGalleryController _draggablegallery)
        {
            photos.Add(_draggablegallery);
        }

        public void RemoveDraggablePhotos(DraggableGalleryController _draggablegallery)
        {
            if (_draggablegallery != null)
            {
                photos.Remove(_draggablegallery);
                Destroy(_draggablegallery.gameObject);
            }
        }

        public bool DoesPhotoExist(string _path)
        {
            foreach (DraggableGalleryController photo in photos)
            {
                if (photo.GetPath() == _path)
                    return true;
            }
            return false;
        }

        public DraggableGalleryController GetPhoto(string _path)
        {
            foreach (DraggableGalleryController photo in photos)
            {
                if (photo.GetPath() == _path)
                    return photo;
            }
            return null;
        }


    }
}