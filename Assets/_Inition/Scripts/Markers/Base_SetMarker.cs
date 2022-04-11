using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inition.HoneyWell.ParseData;
using Inition.Draggables.VideoPlayer;
using Inition.ContactForm;
using Inition.Draggables.Contactform;
using Inition.Draggables.Photos;
using Inition.Draggables.TechSpecs;

namespace Inition.Markers
{
    [System.Serializable]
    public class Base_SetMarker
    {
        [SerializeField]
        private string draggableObjectsParentName = "AllDraggableObjects";

        private Transform draggableObjects;
        private float distributionAngle = 40f;

        public virtual void SetData(DataObject _obj, Transform _transform) { }

        public void InstantiateVideo(string _video, Transform _transform)
        {
            if (draggableObjects == null)
            {
                draggableObjects = GameObject.Find(draggableObjectsParentName).transform;
            }

            if (!DraggableVideosManager.Instance.DoesVideoExist(_video))    //only instantiate a video if another video of the same path doesn't exist
            {
                DraggableVideoController videoObject = (GameObject.Instantiate(Resources.Load("DraggablePrefabs/DraggablePrefabVideoNew")) as GameObject).GetComponent<DraggableVideoController>();
                videoObject.transform.SetParent(draggableObjects.transform);
                videoObject.transform.position = _transform.position;
                videoObject.transform.rotation = _transform.rotation;
                videoObject.Initialize(_video);
                videoObject.vidplaying = true;
            }
        }

        public void InstantiatePhotos(string[] _image, Transform _transform)
        {
            if (draggableObjects == null)
            {
                draggableObjects = GameObject.Find(draggableObjectsParentName).transform;
            }

            string photoPath;

            float segmentRotation = distributionAngle / _image.Length;
            float rotOffset = -distributionAngle / 2f;

            for (int i = 0; i < _image.Length; i++)
            {
                photoPath = _image[i];
                if (!DraggableGalleryManager.Instance.DoesPhotoExist(photoPath))
                {
                    DraggableGalleryController photoObject = (GameObject.Instantiate(Resources.Load("DraggablePrefabs/DraggablePrefabPhotosNew")) as GameObject).GetComponent<DraggableGalleryController>();
                    photoObject.transform.SetParent(draggableObjects.transform);
                    photoObject.transform.position = _transform.position;
                    photoObject.transform.rotation = Quaternion.Euler(_transform.eulerAngles.x, _transform.eulerAngles.y, _transform.eulerAngles.z + rotOffset);
                    photoObject.Initialize(photoPath);
                    rotOffset += segmentRotation;
                }
            }
        }

        public void InstantiateTechSpecs(string _name, string[] _path, Transform _transform)
        {
            if (draggableObjects == null)
            {
                draggableObjects = GameObject.Find(draggableObjectsParentName).transform;
            }

            DraggableTechSpecsController techSpecsObject = (GameObject.Instantiate(Resources.Load("DraggablePrefabs/DraggableTechSpecNew")) as GameObject).GetComponent<DraggableTechSpecsController>();
            techSpecsObject.transform.SetParent(draggableObjects.transform);
            techSpecsObject.transform.position = _transform.position;
            techSpecsObject.transform.rotation = _transform.rotation;
            techSpecsObject.Initialize(_path);
        }

        public void InstantiateContactForm(Transform _transform, string _productName)
        {
            Debug.Log(_productName);
            if (draggableObjects == null)
            {
                draggableObjects = GameObject.Find(draggableObjectsParentName).transform;
            }

            if (!DraggableFormManager.Instance.DoesFormExist(_productName))
            {
                DraggableFormController formObject = (GameObject.Instantiate(Resources.Load("DraggablePrefabs/DraggablePrefabInfoForm")) as GameObject).GetComponent<DraggableFormController>();
                formObject.transform.SetParent(draggableObjects.transform);
                formObject.transform.position = _transform.position;
                formObject.transform.rotation = _transform.rotation;
                formObject.Initialize(_productName);
            }
        }

        public virtual void RemoveVideo(string _video)
        {
            DraggableVideosManager.Instance.RemoveDraggableVideo(DraggableVideosManager.Instance.GetVideo(_video));
        }

        public virtual void RemovePhotos(string[] _images)
        {
            for (int i = 0; i < _images.Length; i++)
            {
                DraggableGalleryManager.Instance.RemoveDraggablePhotos(DraggableGalleryManager.Instance.GetPhoto(_images[i]));
            }
        }

        public virtual void RemoveTechSpecs(string _image)
        {
            DraggableTechSpecsManager.Instance.RemoveDraggablePhotos(DraggableTechSpecsManager.Instance.GetSpec(_image));
        }

    }
}