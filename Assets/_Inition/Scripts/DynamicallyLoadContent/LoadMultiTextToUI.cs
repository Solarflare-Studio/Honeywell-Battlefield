using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace Inition.Utils.LoadText
{
    public class LoadMultiTextToUI : MonoBehaviour
    {
        public enum Mode { OnStart, CustomCall };
        public Mode mode = Mode.OnStart;

        [SerializeField]
        private string filesPath;
        [SerializeField]
        private string filesPrefix;
        [SerializeField]
        private int filesCount;
        [SerializeField]
        private string filesSuffix;

        private Text text;    

        void Start()
        {
            text = GetComponent<Text>();
            text.enabled = false;
            if (mode == Mode.OnStart)
            { 
                CleanText();
                for(int i = 1; i <= filesCount; i++)
                {
                    ChangeText(filesPath + filesPrefix + i + filesSuffix);
                }
            }
        }

        public void CleanText()
        {
            text.text = "";
        }

        public void ChangeText(string _pathToFile)
        {
            string path = Application.streamingAssetsPath + _pathToFile;
               
            if (File.Exists(path))
            {
                text.text += File.ReadAllText(path);
            } 
        } 
    }
}
