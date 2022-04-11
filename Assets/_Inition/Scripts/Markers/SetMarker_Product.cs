using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
using Inition.HoneyWell.ParseData;

namespace Inition.Markers
{
    [System.Serializable]
    public class SetMarker_Product : Base_SetMarker
    {
        [SerializeField]
        private string markerOverview = "MarkerOverview";
        [SerializeField]
        private string markerOverviewBorderClosed = "MarkerOverviewBorderClosed";
        [SerializeField]
        private string markerOverviewBorderOpen = "MarkerOverviewBorderOpen";
        [SerializeField]
        private string productButton_ProductName = "ProductButton_ProductName";
        [SerializeField]
        private string productButton_ProductInfo = "ProductButton_ProductInfo";
        [SerializeField]
        private string markerTitle = "MarkerTitle";
        [SerializeField]
        private string markerBody = "MarkerBody";
        [SerializeField]
        private string markerBodyScrollView = "MarkerBodyScrollView";
        [SerializeField]
        private string showMarkerDescriptionButton = "MarkerShowDetailButton";
        [SerializeField]
        private string hideMarkerDescriptionButton = "MarkerHideDetailButton";

        private Button showMarkerDescriptionButtonRef;
        private Button hideMarkerDescriptionButtonRef;
        private RectTransform overviewBorderClosedRef;
        private RectTransform overviewBorderOpenRef;

        private ScrollRect markerBodyScrollViewRef;

        public override void SetData(DataObject _obj, Transform _transform)
        {
            _transform.GetChildFromName<Text>(productButton_ProductName).text = _obj.title.ToUpper();
            _transform.GetChildFromName<Text>(productButton_ProductInfo).text = _obj.subtitle.ToUpper();

            if (!string.IsNullOrEmpty(_obj.markerTitle))
            {
                overviewBorderOpenRef = _transform.GetChildFromName<RectTransform>(markerOverviewBorderOpen);
                overviewBorderClosedRef = _transform.GetChildFromName<RectTransform>(markerOverviewBorderClosed);

                _transform.GetChildFromName<Text>(markerTitle).text = _obj.markerTitle.ToUpper();
                _transform.GetChildFromName<Text>(markerBody).text = _obj.markerBody.ToUpper();

                _transform.GetChildFromName<Transform>(markerOverview).gameObject.SetActive(true);

                markerBodyScrollViewRef = _transform.GetChildFromName<ScrollRect>(markerBodyScrollView);

                showMarkerDescriptionButtonRef = _transform.GetChildFromName<Button>(showMarkerDescriptionButton);
                showMarkerDescriptionButtonRef.onClick.AddListener(() => ShowMarkerDescription());

                hideMarkerDescriptionButtonRef = _transform.GetChildFromName<Button>(hideMarkerDescriptionButton);
                hideMarkerDescriptionButtonRef.onClick.AddListener(() => HideMarkerDescription());

                HideMarkerDescription();
            }
            else
            {
                _transform.GetChildFromName<Transform>(markerOverview).gameObject.SetActive(false);
            }
        }

        private void ShowMarkerDescription()
        {
            markerBodyScrollViewRef.gameObject.SetActive(true);
            hideMarkerDescriptionButtonRef.gameObject.SetActive(true);
            showMarkerDescriptionButtonRef.gameObject.SetActive(false);
            overviewBorderOpenRef.gameObject.SetActive(true);
            overviewBorderClosedRef.gameObject.SetActive(false);
            
        }

        private void HideMarkerDescription()
        {
            markerBodyScrollViewRef.gameObject.SetActive(false);
            hideMarkerDescriptionButtonRef.gameObject.SetActive(false);
            showMarkerDescriptionButtonRef.gameObject.SetActive(true);
            overviewBorderOpenRef.gameObject.SetActive(false);
            overviewBorderClosedRef.gameObject.SetActive(true);
        }

    }
}