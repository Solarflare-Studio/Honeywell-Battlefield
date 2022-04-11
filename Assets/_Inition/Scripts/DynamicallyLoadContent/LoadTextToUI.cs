using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace Inition.Utils.LoadText
{
    public class LoadTextToUI : MonoBehaviour
    {
        public enum Mode { OnStart, CustomCall };
        public Mode mode = Mode.OnStart;

        [SerializeField]
        private string fileName;

        [SerializeField]
        private bool enableTextOnLoad = false;

        private Text text;    

        void Start()
        {
            text = GetComponent<Text>();
            text.enabled = false;
            if (mode == Mode.OnStart)
            { 
                ChangeText(fileName);
            } 
        }

        public void ChangeText(string _pathToFile)
        {
            string path = Application.streamingAssetsPath + _pathToFile;
               
            if (File.Exists(path))
            {
                text.text = File.ReadAllText(path); 
            }

            if (enableTextOnLoad)
            {
                text.enabled = true;
            } 
        } 
    }
}
