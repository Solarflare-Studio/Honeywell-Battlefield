using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inition.Draggables.VideoPlayer
{
    public class DraggableVideosManager : Singleton<DraggableVideosManager>
    {
        private List<DraggableVideoController> videos = new List<DraggableVideoController>();
        private int currentlyPlayingVideo = -1;
        
        public void AddDraggableVideo(DraggableVideoController _draggableVideo)
        {
            videos.Add(_draggableVideo);
            PlayDraggableVideo(_draggableVideo);
        }

        public void RemoveDraggableVideo(DraggableVideoController _draggableVideo)
        {
            if (_draggableVideo != null)
            {
                videos.Remove(_draggableVideo);
                Destroy(_draggableVideo.gameObject);
            }
        }

        public void PlayDraggableVideo(DraggableVideoController _draggableVideo)
        {
            foreach(DraggableVideoController video in videos)
            {
                if (video == _draggableVideo)
                {
                    video.PlayVideo();
                }
                else
                {
                    video.PauseVideo();
                }
            }
        }

        public bool IsNobodyPlaying()
        {
            if (currentlyPlayingVideo < 0)
                return true;
            else return false;
        }

        public bool DoesVideoExist(string _path)
        {
            foreach (DraggableVideoController video in videos)
            {
                if (video.GetPath() == _path)
                    return true;
            }
            return false;
        }

        public DraggableVideoController GetVideo(string _path)
        {
            foreach (DraggableVideoController video in videos)
            {
                if (video.GetPath() == _path)
                    return video;
            }
            return null;
        }

        void PlayNextVideo()
        {

        }
    }
}