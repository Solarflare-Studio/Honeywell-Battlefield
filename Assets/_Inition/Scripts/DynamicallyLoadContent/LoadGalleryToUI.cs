using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Inition.Utils.LoadImage
{
    public class LoadGalleryToUI : MonoBehaviour
    {
        public enum Mode { OnStart, CustomCall };
        public Mode mode = Mode.OnStart;

        RectTransform rT;
        RectTransform sT;
        [SerializeField]
        private string fileName;

        private Image img;
        //private BoxCollider2D collider;


        //public object pScale { get; private set; }

        private CanvasGroup canvasGroup;
         
        void Start()
        {  
            img = transform.GetChildFromName<Image>("BackgroundImage");
            canvasGroup = GetComponent<CanvasGroup>();

            img.enabled = false;

            if (mode == Mode.OnStart)
            {
                ChangeImage(fileName);
            }
            canvasGroup.alpha = 0f;
        }

        public void ChangeImage(string _pathToFile)
        { 
            StartCoroutine("LoadSprite", Application.streamingAssetsPath + "/" + _pathToFile);
        }

        private IEnumerator LoadSprite(string _absoluteImagePath)
        {
            string finalPath;
            WWW localFile;
            Texture2D texture; 

            finalPath = "file://" + _absoluteImagePath;
            localFile = new WWW(finalPath); 
            yield return localFile;

            texture = localFile.texture;
            Vector2 spriteSize = new Vector2(texture.width, texture.height);

            RectTransform rectTS = img.transform.GetComponent<RectTransform>();
            rectTS.sizeDelta = spriteSize; 

            RectTransform rectTF = transform.GetComponent<RectTransform>(); 
           
            img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            img.enabled = true;
            canvasGroup.alpha = 1f;
        }
    }
}
