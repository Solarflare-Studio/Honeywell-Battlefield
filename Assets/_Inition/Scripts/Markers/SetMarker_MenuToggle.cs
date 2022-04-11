using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
using Inition.HoneyWell.ParseData;
using Inition.Effects.FadeInOut;

namespace Inition.Markers
{
    [System.Serializable]
    public class SetMarker_MenuToggle : Base_SetMarker
    {
        [SerializeField]
        private string closedElementsHolderName = "ClosedMarkerElements";
        [SerializeField]
        private string openedElementsHolderName = "OpenedMarkerElements";
        [SerializeField]
        private string closeMarkerButtonName = "CloseToMainPage";
        [SerializeField]
        private string openMarkerButtonName = "ProductButton";

        private UiCanvasGroup_FadeEffect closedHolderFadeEffect;
        private UiCanvasGroup_FadeEffect openedHolderFadeEffect;
        private Button closeMarkerButtonRef;
        private Button openMarkerButtonRef;

        public override void SetData(DataObject _obj, Transform _transform)
        {
            closedHolderFadeEffect = _transform.GetChildFromName<UiCanvasGroup_FadeEffect>(closedElementsHolderName);
            openedHolderFadeEffect = _transform.GetChildFromName<UiCanvasGroup_FadeEffect>(openedElementsHolderName);

            closedHolderFadeEffect.Initialized();
            openedHolderFadeEffect.Initialized();

            closeMarkerButtonRef = _transform.GetChildFromName<Button>(closeMarkerButtonName);
            closeMarkerButtonRef.onClick.AddListener(() => ToggleMarkerElements(_obj, false));
            openMarkerButtonRef = _transform.GetChildFromName<Button>(openMarkerButtonName);
            openMarkerButtonRef.onClick.AddListener(() => ToggleMarkerElements(_obj, true));

            openedHolderFadeEffect.ForceFadeOut();
            openedHolderFadeEffect.canvasGroup.blocksRaycasts = false;
            closedHolderFadeEffect.canvasGroup.blocksRaycasts = true;
        }

        void ToggleMarkerElements(DataObject _obj, bool _val)
        {
            if (_val)
            {
                closedHolderFadeEffect.FadeOut();
                openedHolderFadeEffect.FadeIn();
            }
            else
            {
                closedHolderFadeEffect.FadeIn();
                openedHolderFadeEffect.FadeOut();
            }
            closedHolderFadeEffect.canvasGroup.blocksRaycasts = !_val;
            openedHolderFadeEffect.canvasGroup.blocksRaycasts = _val;
            openMarkerButtonRef.interactable = !_val;
            closeMarkerButtonRef.interactable = _val;

            RemoveAllDraggables(_obj);
        }

        public void RemoveAllDraggables(DataObject _obj)
        {
            RemoveVideo(_obj.video.thumbnailPath);

            switch (_obj.actions.Count)
            {
                case 2:
                    RemovePhotos(_obj.actions[0].imagePaths);
                    RemoveTechSpecs(_obj.actions[1].imagePaths[0]);
                    break;
                case 3:
                    RemoveVideo(_obj.actions[0].videoPath);
                    RemovePhotos(_obj.actions[1].imagePaths);
                    RemoveTechSpecs(_obj.actions[2].imagePaths[0]);
                    break;
                case 4:
                    RemoveVideo(_obj.actions[0].videoPath);
                    RemoveVideo(_obj.actions[1].videoPath);
                    RemovePhotos(_obj.actions[2].imagePaths);
                    RemoveTechSpecs(_obj.actions[3].imagePaths[0]);
                    break;
            }
        }
    }
}