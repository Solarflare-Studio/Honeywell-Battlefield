using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inition.UI.DraggableObjects
{  
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TargetJoint2D))]
    public class DragObjectController : MonoBehaviour
    {
        //This component controls the dragging an object. Dragging physics happen via TargetJoint2D...
        private Rigidbody2D rbody;
        private TargetJoint2D targetJoint;
        public int touchID = -1;

        //[SerializeField]
        //private Vector3 testCentreOfMassChange;
         
        private DraggableObjectsOrderManager orderManager;

        private RectTransform rect;
        
        void Start()
        {
            rbody = GetComponent<Rigidbody2D>();
            targetJoint = GetComponent<TargetJoint2D>(); 
            rect = GetComponent<RectTransform>();
            orderManager = FindObjectOfType<DraggableObjectsOrderManager>();
            if(orderManager == null)
            {
                Debug.LogWarning("Could not find an order manager!!!");
            }
            //orderManager.AddObject(this);   
        }

        void OnDestroy()
        {
            //orderManager.RemoveObject(rect);
        }

        /*private void Update()
        {
            rbody.centerOfMass = testCentreOfMassChange;
        }*/

        public void DragObject(Vector3 _pos)
        {
            //orderManager.Reorder(rect);
            targetJoint.target = new Vector2(_pos.x, _pos.y);
        }

        public void StartDragging(Vector3 _pos, int _newTouchID)
        { 
            touchID = _newTouchID;
            targetJoint.enabled = true; 
            targetJoint.anchor = transform.InverseTransformPoint(_pos);
            targetJoint.target = new Vector2(_pos.x, _pos.y);
        }

        public void StopDragging()
        { 
            targetJoint.enabled = false; 
            touchID = -1;
        }
    }
}
