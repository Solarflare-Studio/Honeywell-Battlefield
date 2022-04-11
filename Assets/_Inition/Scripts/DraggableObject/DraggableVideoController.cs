using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;
using Inition.UI.DraggableObjects;

namespace Inition.Draggables.VideoPlayer
{
    public class DraggableVideoController : DraggableObjectController
    {
        //public bool isFrozen; 
        //public float creationTime;
        public bool vidplaying = false;
        
        private string path;

        [SerializeField]
        private string playButtonName = "PlayButton"; 
        private Button playButtonRef;
        [SerializeField]
        private string pauseButtonName = "PauseButton"; 
        private Button pauseButtonRef;
        [SerializeField]
        private string rewindButtonName = "RewindButton";
        private Button rewindButtonRef;

        [SerializeField]
        private string closeButtonName = "CloseButton";
        private Button closeButtonRef;

        [SerializeField]
        private string backgroundVideoPlayerName = "BackgroundVideoPlayer";
        private MediaPlayer player;
         
        private DraggableVideosManager videosManager;

        [SerializeField]
        private bool testing = false;
        [SerializeField]
        private string testingVideoPath = "";

        public void Initialize(string _video)
        {
            videosManager = DraggableVideosManager.Instance;
            path = _video;
            //creationTime = Time.time;

            player = transform.GetChildFromName<MediaPlayer>(backgroundVideoPlayerName);
            playButtonRef = transform.GetChildFromName<Button>(playButtonName);
            pauseButtonRef = transform.GetChildFromName<Button>(pauseButtonName);
            rewindButtonRef = transform.GetChildFromName<Button>(rewindButtonName); 
            closeButtonRef = transform.GetChildFromName<Button>(closeButtonName);
            
            playButtonRef.onClick.AddListener(() => PlayVideo());
            pauseButtonRef.onClick.AddListener(() => PauseVideo());
            rewindButtonRef.onClick.AddListener(() => RewindVideo());
            closeButtonRef.onClick.AddListener(() => CloseButtonPressed());

            videosManager.AddDraggableVideo(this);

            ToggleButtonsPauseMode(false);

            LoadVideo(_video); 
        }

        private void OnDestroy()
        {
            videosManager.RemoveDraggableVideo(this);
        }

        public void LoadVideo(string _videPath)
        {
            //Debug.Log(MediaPlayer.FileLocation.RelativeToDataFolder);
            player.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, _videPath);
        }

        public void PlayVideo()
        {
            player.Play();  
            ToggleButtonsPauseMode(false);
        }

        public void PauseVideo()
        {
            player.Pause(); 
            ToggleButtonsPauseMode(true);
        }

        void ToggleButtonsPauseMode(bool _state)
        { 
            playButtonRef.gameObject.SetActive(_state); 
            pauseButtonRef.gameObject.SetActive(!_state); 
            rewindButtonRef.gameObject.SetActive(!_state);
        }

        public void RewindVideo()
        {
            player.Rewind(true);
            PlayVideo(); 
        }

        public void CloseButtonPressed()
        {
            player.Pause();
            Destroy(gameObject);
        }

        public void ChangeFreezeVideoState(bool _state)   //when another video is playing this video is "frozen". Some info appears to tell the user that they need to wait before they can see this video.
        {
            /*if(_state)
            {
                player.Pause();
            }
            else
            {

            } */
        }

        public string GetPath()
        {
            return path;
        }

        public override void BringToFront()
        {
            base.BringToFront();
            videosManager.PlayDraggableVideo(this);
        }
    }

}

