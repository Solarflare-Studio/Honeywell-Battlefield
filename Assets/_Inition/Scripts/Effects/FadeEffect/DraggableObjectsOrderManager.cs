using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inition.UI.DraggableObjects
{   //this class takes care of the draggable objects order. An object that's dragged last should be drawn last to appear first on the screen...
    public class DraggableObjectsOrderManager : MonoBehaviour
    {
        [SerializeField]
        private List<DraggableObjectController> allDraggableObjects = new List<DraggableObjectController>();

        public void Reorder(DraggableObjectController _lastDragged)
        {
            //RectTransform toReorder = allDraggableObjects.Find(_lastDragged.Equals);//.SetAsLastSibling(); 
            //if(toReorder.tag != "DraggableHolder")//this is for the video. Since the video player UI implementation from AVPro is not flexible I had to use a holder that holds a video and the draggable object. Instead of reordering that object I need to reorder it's parent...
            //{
            //    toReorder = toReorder.parent.GetComponent<RectTransform>();
            //}
            _lastDragged.BringToFront();
        }

        public void AddObject(DraggableObjectController _obj)
        {
            allDraggableObjects.Add(_obj);
            Reorder(_obj);
        }

        public void RemoveObject(RectTransform _obj)
        {
            //allDraggableObjects.Remove(DraggableObjectController);
        }
    } 
}

