using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Inition.HoneyWell.ParseData;
using Inition.Markers;


public class MarkerController : MonoBehaviour 
{
    [SerializeField]
    private string markerTag;
    [SerializeField]
    private float timeout = 0.5f;
    [SerializeField]
    private Color color;
    [SerializeField]
    private float cameraDistance;

    [SerializeField]
    private int markerNumber;
    [SerializeField]
    private bool applyRotation = true;

    private GameObject markerObjectRef;  

    private bool visible;  // Should marker be visible?
    private bool hiding;   // Is the marker in the process of hiding. (Smoothes out any glitching on the table.)

    /// <summary>
    /// Store the tag associated with the marker. This will be all the tags in the Tags property including "TUIO, Object, ". (A lot simpler than trying to extract the unknown tag.)
    /// </summary>
    public string MarkerTag
    {
        get
        {
            return markerTag;
        }
        set
        {
            markerTag = value;
        }
    }

    public bool Visible
    {
        get
        {
            return visible;
        }

        set
        {
            visible = value;
            if (!visible && gameObject.activeSelf)
            {
                StartCoroutine(HideMarker(0.0f));
            }
        }
    }

    public float Timeout
    {
        get
        {
            return timeout;
        }

        set
        {
            timeout = value;
        }
    }

    /// <summary>
    /// Reposition marker to specified position and rotation.
    /// </summary>
    /// <param name="newPosition">The new position</param>
    /// <param name="newRotation">The new rotation</param>
    public void ApplyPositionRotation(Vector3 newPosition, Quaternion newRotation)
    {
        RectTransform t = GetComponent<RectTransform>();
        Canvas canvas = GetComponent<Canvas>();
        if (canvas == null || canvas.renderMode != RenderMode.WorldSpace)
        {
            canvas = GetComponentInParent<Canvas>();
        }
        //Debug.Log("Render mode =" + canvas.renderMode);
        //Debug.Log("New position = " + newPosition);
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            t.position = newPosition;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            t.anchoredPosition = newPosition;
        }
        if (canvas.renderMode == RenderMode.WorldSpace)
        {
            newPosition.z = 100.0f;
            t.position = Camera.main.ScreenToWorldPoint(newPosition);
        }
        //dial.rotation = newRotation;
        if (markerObjectRef != null)
            markerObjectRef.transform.rotation = newRotation;
    }

    public void AddRotation(float newRotation)
    {
        RectTransform t = GetComponent<RectTransform>();
        t.Rotate(new Vector3(0.0f, 0.0f, newRotation));
        //ApplyPositionRotation(GetComponent<RectTransform>().anchoredPosition, newQuaternion);
    }

    /// <summary>
    /// Show marker.
    /// </summary>
    public void ShowMarker()
    {
        gameObject.SetActive(true);
        // Stop the hide process, in case it was lost only for a moment.
        hiding = false;

        //instantiate and set marker...
        if(markerObjectRef == null)
        {
            markerObjectRef = Instantiate(Resources.Load("MarkerPrefab") as GameObject);
            markerObjectRef.transform.SetParent(transform);
            markerObjectRef.transform.localPosition = Vector3.zero;
            markerObjectRef.transform.localRotation = Quaternion.identity;
            markerObjectRef.transform.localScale = Vector3.one;
             
            markerObjectRef.GetComponent<SetMarkerData>().SetData(LoadAndParseData.Instance.GetObjectViaMarkerID(markerNumber));
        }

        visible = true;
    }

    /// <summary>
    /// Hide the marker after a short period.
    /// </summary>
    /// <param name="timeout">Time to wait before actualy removing the marker</param>
    /// <returns>IEnumerator</returns>
    public IEnumerator HideMarker(float timeout)
    {
        hiding = true;
        // Wait until delay is over or marker has been restored, whichever is sooner.
        float elapsedTime = 0.0f;
        while (elapsedTime < timeout && hiding)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // If marker is still not found, then hide the marker
        if (hiding)
        {
           
            gameObject.SetActive(false);
            hiding = false;
        }

        if(markerObjectRef != null)
        {
            markerObjectRef.GetComponent<SetMarkerData>().RemoveDraggableObjects(LoadAndParseData.Instance.GetObjectViaMarkerID(markerNumber));
            Destroy(markerObjectRef);
        }

        visible = false;
    }

}
