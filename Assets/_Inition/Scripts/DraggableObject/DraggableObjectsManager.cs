using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TouchScript;

namespace Inition.UI.DraggableObjects
{

    public class DraggableObjectsManager : MonoBehaviour
    {
        [SerializeField]
        private string[] touchTags = new string[] { "TUIO, Touch", "Mouse" };

        private DraggableObjectsOrderManager orderManager;
        private List<DraggableObjectController> draggedObjects;
         
        private void Start()
        {
            draggedObjects = new List<DraggableObjectController>();
            orderManager = GetComponentInChildren<DraggableObjectsOrderManager>();
        }

        private void OnEnable()
        {
            if (TouchManager.Instance != null)
            {
                TouchManager.Instance.TouchesBegan += OnTouchBegin;
                TouchManager.Instance.TouchesMoved += OnTouchMoved;
                TouchManager.Instance.TouchesEnded += OnTouchEnded;
            }
        }

        private void OnDisable()
        {
            if (TouchManager.Instance != null)
            {
                TouchManager.Instance.TouchesBegan -= OnTouchBegin;
                TouchManager.Instance.TouchesMoved -= OnTouchMoved;
                TouchManager.Instance.TouchesEnded -= OnTouchEnded;
            }
        }

        private void Update()
        {
        }

        private void OnTouchMoved(object sender, TouchEventArgs e)
        {
            //if (draggedObjects.Count > 0)
            //{
            //    for (int i = 0; i < draggedObjects.Count; i++)
            //    {
            //        bool found = false;
            //        for (int j = 0; j < e.Touches.Count; j++)
            //        {
            //            if (draggedObjects[i].touchID == e.Touches[j].Id)
            //            {
            //                //draggedObjects[i].DragObject(e.Touches[j].Position);
            //                found = true;
            //            }
            //        }

            //        if (!found)
            //        {
            //            StopDrag(draggedObjects[i]);
            //        }

            //    }
            //} 
        }
         
        private void OnTouchBegin(object sender, TouchEventArgs e)
        {
            DebugPanel.Current.ClearMessage();

            foreach (TouchPoint touch in e.Touches)
            {
                //Debug.Log("OnTouchBegin: " + touch.Id + " " + touch.Tags);
                for(int touchIndex = 0; touchIndex < touchTags.Length; touchIndex++)
                {
                    string touchTag = touchTags[touchIndex];
                    if (touch.Tags.ToString().Contains(touchTag))
                    {
                        //Debug.Log("Evaluating: " + touchTag);
                        PointerEventData pointerData = new PointerEventData(EventSystem.current);
                        pointerData.position = touch.Position;
                        List<RaycastResult> results = new List<RaycastResult>();
                        EventSystem.current.RaycastAll(pointerData, results);

                        int? markerLayer = null;
                        int? draggableLayer = null;
                        bool keyboard = false;

                       // Debug.Log("Layers hit: " + results.Count);
                        for (int hitIndex = 0; hitIndex < results.Count; hitIndex++)
                        {
                            //Debug.Log("Layer " + hitIndex + ": " + LayerMask.LayerToName(results[hitIndex].gameObject.layer));
                            if (results[hitIndex].gameObject.layer == LayerMask.NameToLayer("DraggableUI") && draggableLayer == null)
                            {
                                draggableLayer = hitIndex;
                                //Debug.Log("Found draggable object");
                            }
                            if (results[hitIndex].gameObject.layer == LayerMask.NameToLayer("Marker") && markerLayer == null)
                            {
                                markerLayer = hitIndex;
                                //Debug.Log("Found marker");
                            }
                            if (results[hitIndex].gameObject.name == "VirtualKeyboard(Clone)")
                            {
                                keyboard = true;
                            }
                        }

                        if (draggableLayer != null && markerLayer == null && !keyboard)
                        {
                            GameObject selectedObject = results[(int)draggableLayer].gameObject;
                            //Debug.Log("Starting drag");
                            DebugPanel.Current.DebugMessage(selectedObject.name);
                            DraggableObjectController dragController = selectedObject.GetComponentInParent<DraggableObjectController>();
                            if (dragController != null)
                            {
                                dragController.BringToFront();
                            }
                        }

                        results.Clear();
                    }
                }
            }
        }

        private void OnTouchEnded(object sender, TouchEventArgs e)
        {
            // Nothing happens here yet
        }

        //void StartDrag(DraggableObjectController _dragged, Vector2 _touchPos, int _touchID)
        //{
        //    if(!draggedObjects.Contains(_dragged))
        //    {   
        //        StopDrag(_dragged);  
        //    } 
        //    //_dragged.StartDragging(_touchPos, _touchID);
        //    draggedObjects.Add(_dragged); 
        //}

        //void StopDrag(DraggableObjectController _dragged)
        //{
        //    //_dragged.StopDragging();
        //    if (draggedObjects.Contains(_dragged))
        //    {
        //        draggedObjects.Remove(_dragged);
        //    }  
        //}
    }

}
