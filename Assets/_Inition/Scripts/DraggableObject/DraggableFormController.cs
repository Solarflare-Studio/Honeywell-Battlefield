using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;
using System;
using Inition.UI.DraggableObjects;

using Inition.ContactForm;
namespace Inition.Draggables.Contactform
{
    public class DraggableFormController : DraggableObjectController
    {
 
        private DraggableFormManager formManager;
        private string formTitle;

        [SerializeField]
        private string closeButtonName = "ButtonCloseInfoForm";
        private Button closeButtonRef;


        public string FormTitle
        {
            get
            {
                return formTitle;
            }
        }

        public void Initialize(string _formTitle)
        {
            formTitle = _formTitle;
            formManager = DraggableFormManager.Instance;
            formManager.AddDraggableForm(this);

            closeButtonRef = transform.GetChildFromName<Button>(closeButtonName);
            closeButtonRef.onClick.AddListener(() => CloseButtonPressed());

            GetComponentInChildren<ContactFormManager>().Initialize(_formTitle);
        }

        private void OnDestroy()
        {
            formManager.RemoveDraggableForm(this);
        }

        public void CloseButtonPressed()
        {
            Destroy(gameObject);
        }

        internal string GetPath()
        {
            throw new NotImplementedException();
        }
    }

}

