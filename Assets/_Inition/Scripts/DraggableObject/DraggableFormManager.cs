using Inition.ContactForm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inition.Draggables.Contactform
{
    public class DraggableFormManager : Singleton<DraggableFormManager>
    {
        private List<DraggableFormController> forms = new List<DraggableFormController>();



        public void AddDraggableForm(DraggableFormController _draggableform)
        {
            forms.Add(_draggableform);
        }

        public void RemoveDraggableForm(DraggableFormController _draggableform)
        {
            forms.Remove(_draggableform);
        }

        public bool DoesFormExist(string _formTitle)
        {
            foreach(DraggableFormController form in forms)
            {
                if (form.FormTitle == _formTitle)
                {
                    return true;
                }
            }
            return false;
        }

    }
}