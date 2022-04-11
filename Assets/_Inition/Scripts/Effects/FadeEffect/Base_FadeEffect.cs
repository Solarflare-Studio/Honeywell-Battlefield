using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inition.Effects.FadeInOut
{
    public class Base_FadeEffect : MonoBehaviour
    {
        /// <summary>
        /// Base class for the fade in - fade out effect. 
        /// You can also use it to toggle the fade state. If it's transparent it will fade in, if it's visible it will fade out.
        /// </summary>
        /// 
        [SerializeField]
        private float fadeInTime = 1f;
        [SerializeField]
        private float fadedInAlpha = 1f;
        [SerializeField]
        private float fadeOutTime = 1f;
        [SerializeField]
        private float fadedOutAlpha = 0f;

        public delegate void OnFadeInCompleteEvent();
        public OnFadeInCompleteEvent OnFadeInComplete;

        public delegate void OnFadeOutCompleteEvent();
        public OnFadeOutCompleteEvent OnFadeOutComplete;

        public delegate void FadeStateChangedEvent(bool _newState);
        public FadeStateChangedEvent OnFadeStateChanged;

        private bool currentState;
        private float currentAlpha = 0f;
        private float targetAlpha;

        public void Start()
        { 
            if(currentAlpha < (fadedInAlpha + fadedOutAlpha)/2f)//find if current alpha is closer to the fade in or fade out value
            {   //if it's closer to fadeOutAlpha
                currentState = false;
                currentAlpha = fadedOutAlpha;
            }
            else
            {
                //if it's closer to fadeInAlpha
                currentState = true;
                currentAlpha = fadedInAlpha;
            }
            targetAlpha = currentAlpha;
            ChangeAlpha(currentAlpha);
        }

        public void FadeIn(float _time)
        {
            fadeInTime = _time;
            FadeIn();
        }

        public void FadeIn()
        {
            StopCoroutine("ChangeAlpha");
            ChangeAlpha(targetAlpha);
            StartCoroutine("ChangeAlpha", true);
        }

        public void FadeOut(float _time)
        {
            fadeOutTime = _time;
            FadeOut();
        }

        public void FadeOut()
        {
            if (gameObject.activeInHierarchy)
            {
                StopCoroutine("ChangeAlpha");
                ChangeAlpha(targetAlpha);
                StartCoroutine("ChangeAlpha", false);
            }
        }

       /* public void ToggleFade(float _time)
        {

        }

        public void ToggleFade()
        {

        }*/

        public void ForceFadeOut( )
        {
            FadeOut(0f);
        }

        private void FadeComplete()
        {
            if (currentState)
            {
                if (OnFadeInComplete != null)
                    OnFadeInComplete();
            }
            else
            {
                if (OnFadeOutComplete != null)
                    OnFadeOutComplete();
            } 

            if (OnFadeStateChanged != null)
                OnFadeStateChanged(currentState); 
        }
         
        public delegate void ChangeAlphaEvent(float _newAlpha);
        public ChangeAlphaEvent OnAlphaChanged;
         
        private IEnumerator ChangeAlpha(bool _fadeIn)
        {  
            float time;
            if(_fadeIn)
            {
                targetAlpha = fadedInAlpha;
                time = fadeInTime;
            }
            else
            {
                targetAlpha = fadedOutAlpha;
                time = fadeOutTime;
            }

            if (currentAlpha == targetAlpha)
            {
                Debug.Log("Can't fade to target as it's already faded!");
                StopCoroutine("ChangeAlpha"); 
            }

            int tickRate = 60;
            float delay = 1f/(float)tickRate;
            int ticks = (int)(tickRate * time);
             
            float changeFactor = (currentAlpha - targetAlpha) / ticks; 
             
            int changes = 0;

            while (changes < ticks)
            {
                ChangeAlpha(currentAlpha - changeFactor);  
                changes++;
                yield return new WaitForSeconds(delay);
            }
            ChangeAlpha(targetAlpha);
            FadeComplete();
        }

        public void ChangeAlpha(float _newVal)
        {
            currentAlpha = _newVal;
            AlphaChanged();
        }

        private void AlphaChanged()
        {
            if (OnAlphaChanged != null)
            {
                OnAlphaChanged(currentAlpha);
            }
        }  
    } 
}

