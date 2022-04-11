using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inition.HoneyWell.ParseData;

namespace Inition.Markers
{ 
    public class SetMarkerData : MonoBehaviour
    {
        private bool initialized = false;   //this is used to test in the editor..
        public bool testStart = false;
        public int testindex = 20;

        [SerializeField]
        private SetMarker_MenuToggle menuToggle = new SetMarker_MenuToggle();
        [SerializeField]
        private SetMarker_Contents contentsMenu = new SetMarker_Contents();
        [SerializeField]
        private SetMarker_Product mainPage = new SetMarker_Product();
        [SerializeField]
        private SetMarker_InteractableMenu secondaryPage = new SetMarker_InteractableMenu(); 
        [SerializeField]
        private SetMarker_ContextualMenu contextualMenu = new SetMarker_ContextualMenu();
        
        public void SetData(DataObject _obj)
        {
            initialized = true;

            mainPage.SetData(_obj, transform);          //set the main page elements 
            secondaryPage.SetData(_obj, transform);     //set the secondary page elements
            contentsMenu.SetData(_obj, transform);      //set the secondary page content menu
            menuToggle.SetData(_obj, transform);        //this one sets up the toggle between the main and the secondary menus 
            contextualMenu.SetData(_obj, transform);    //this is the bottom right menu. It's different depending on what sort of data we have available.
        }

        public void RemoveDraggableObjects(DataObject _obj)
        {
            menuToggle.RemoveAllDraggables(_obj);
        }

        private void Start()
        {
            if(testStart && !initialized)
            {
                Invoke("DelayedTestStart", 0.1f);
            }
        }

        void DelayedTestStart()
        { 
            SetData(LoadAndParseData.Instance.GetObjectViaMarkerID(testindex));
        }
    } 
}
