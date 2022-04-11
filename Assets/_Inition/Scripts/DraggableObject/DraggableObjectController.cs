using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inition.UI.DraggableObjects
{

    public class DraggableObjectController : MonoBehaviour
    {
        private RectTransform rect;

        // Use this for initialization
        void Start()
        {
            rect = GetComponent<RectTransform>(); 
        }

        public virtual void BringToFront()
        {
            if (rect != null)
            {
                rect.SetAsLastSibling();
            }
        }

    }
}


