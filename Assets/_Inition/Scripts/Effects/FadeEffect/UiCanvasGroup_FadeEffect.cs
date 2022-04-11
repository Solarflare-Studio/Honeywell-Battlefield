using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inition.Effects.FadeInOut
{ 
    public class UiCanvasGroup_FadeEffect : Base_FadeEffect
    { 
        public CanvasGroup canvasGroup;
        private bool initialized = false;

        [SerializeField]
        private bool notInteractableWhenFadeOut = false;

        private void Start()
        { 
            if(!initialized)
            {
                Initialized();
            } 
        }

        public void Initialized()
        {
            if(!initialized)
            { 
                initialized = true;
                canvasGroup = GetComponent<CanvasGroup>();
                ChangeAlpha(canvasGroup.alpha);

                base.Start();
                if (canvasGroup == null)
                {
                    Debug.LogWarning("Could not find component DisplayUGUI attached to game object " + gameObject.name + " !");
                }
                else
                {
                    OnAlphaChanged += AlphaChanged;
                }
            }
        }

        private void AlphaChanged(float _newAlpha)
        {
            canvasGroup.alpha = _newAlpha;
            if (notInteractableWhenFadeOut)
            {
                if (canvasGroup.blocksRaycasts && _newAlpha < 0.1f)
                {
                    canvasGroup.blocksRaycasts = false;
                }
                else if (!canvasGroup.blocksRaycasts && _newAlpha > 0.1f)
                {
                    canvasGroup.blocksRaycasts = true;
                }
            }
        } 
    }
}