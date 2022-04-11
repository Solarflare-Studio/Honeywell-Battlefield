using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inition.Effects.FadeInOut
{
    public class DisplayUGUI_FadeEffect : Base_FadeEffect
    {
        private RenderHeads.Media.AVProVideo.DisplayUGUI uiRef;

        private void Start()
        { 
            uiRef = GetComponent<RenderHeads.Media.AVProVideo.DisplayUGUI>();
            ChangeAlpha(uiRef.color.a); 
            base.Start();
            if (uiRef == null)
            {
                Debug.LogWarning("Could not find component DisplayUGUI attached to game object " + gameObject.name + " !"); 
            }
            else
            { 
                OnAlphaChanged += AlphaChanged;
            }
        }

        private void AlphaChanged(float _newAlpha)
        {
            uiRef.color = new Color(uiRef.color.r, uiRef.color.g, uiRef.color.b, _newAlpha);
        }
        
        /*private void Update()
        { 
            if (Input.GetKeyDown(KeyCode.UpArrow))
            { 
                FadeIn();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                FadeOut();
            } 
        } */
    } 
}
