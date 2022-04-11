using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inition.Utils.LoadImage
{
    public class LoadImageToUI : MonoBehaviour
    {
        public enum Mode { OnStart, CustomCall };
        public Mode mode = Mode.OnStart;

        RectTransform rT;
        [SerializeField]
        private string fileName;

        private Image img;

        public object pScale { get; private set; }

        void Start()
        { 
            img = GetComponent<Image>();
            img.enabled = false;
            if (mode == Mode.OnStart)
            {
                ChangeImage(fileName);
            }
        }

        public void ChangeImage(string _pathToFile)
        { 
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine("LoadSprite", Application.streamingAssetsPath + "/" + _pathToFile);
            }
        }

        private IEnumerator LoadSprite(string _absoluteImagePath)
        {
            string finalPath;
            WWW localFile;
            Texture2D texture;
            //Sprite sprite;

            finalPath = "file://" + _absoluteImagePath;
            localFile = new WWW(finalPath); 
            yield return localFile;
            texture = localFile.texture;
            texture.wrapMode = TextureWrapMode.Clamp;
            //RectTransform rectTF = img.transform.GetComponent<RectTransform>();
            //rectTF.sizeDelta = new Vector2(texture.width, texture.height);
            img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            img.enabled = true;
        }  
    }
}
