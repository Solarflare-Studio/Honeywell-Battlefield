using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using Inition.Effects.FadeInOut;

namespace Inition.UI.Background
{ 
    public class BackgroundVideoManager : MonoBehaviour
    {   
        [SerializeField]
        private string standByVideoObjectName = "StandByVideo";
        [SerializeField]
        private string backgroundVideoObjectName = "BackgroundVideo";

        private DetectMarkers detector;

        private Base_FadeEffect standByUI;
        private GameObject backgroundObject;
        private bool currentStateIsMarkerActive = false;

        private void Start()
        {
            detector = GameObject.Find("Marker Detector").GetComponent<DetectMarkers>();

            standByUI = GameObject.Find(standByVideoObjectName).GetComponent<Base_FadeEffect>();
            backgroundObject = GameObject.Find(backgroundVideoObjectName); 
        }

        private void Update()
        {
            if (currentStateIsMarkerActive)
            {
                if (detector.VisibleCount == 0)
                {
                    standByUI.FadeIn();
                    currentStateIsMarkerActive = false;
                }
            }
            else
            {
                if (detector.VisibleCount > 0)
                {
                    standByUI.FadeOut();
                    currentStateIsMarkerActive = true;
                }
            }

        }

        void ToggleVideos(bool _activeMarker)
        {
            if (_activeMarker != currentStateIsMarkerActive)
            {
                currentStateIsMarkerActive = _activeMarker;
                if (currentStateIsMarkerActive)
                { 
                    //there's a marker on the table
                    standByUI.FadeOut();
                }
                else
                {
                    //there's no marker on the table
                    standByUI.FadeIn();
                }
            }
        }

    }

}