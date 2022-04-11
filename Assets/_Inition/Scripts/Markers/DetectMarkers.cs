using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TouchScript;

public class DetectMarkers : MonoBehaviour 
{
    [SerializeField]
    private Transform markerParent;

    private MarkerController[] markers;
    private Dictionary<string, MarkerController> markerDictionary;
     
    public int VisibleCount {
        get
        {
            int count = 0;
            foreach(MarkerController marker in markers)
            {
                if (marker.Visible)
                {
                    count++;
                }
            }
            return count;
        }
    }

    void Start()
	{
        
        // Get all available markers in the scene
        markers = markerParent.GetComponentsInChildren<MarkerController>(true);
        // Create dictionary entries to link marker tag to the marker GameObject
        markerDictionary = new Dictionary<string, MarkerController>();
        foreach(MarkerController marker in markers)
        {
            markerDictionary.Add(marker.MarkerTag, marker);
            StartCoroutine(marker.HideMarker(0.0f));
        }
    }

    private void Update()
    {
        int marker = SelectedTestMarker();
        if (marker > -1)
        {
            if (markers[marker].Visible)
            {
                float rot = Input.GetAxis("Mouse X");
                if (rot != 0.0f)
                {
                    markers[marker].AddRotation(rot);
                }
            }
        }
    }

    /// <summary>
    /// Enable touch events.
    /// </summary>
    private void OnEnable()
	{
		if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan += touchBeganHandler; 
			TouchManager.Instance.TouchesMoved += touchMovedHandler;
			TouchManager.Instance.TouchesEnded += touchEndedHandler;
		}
	}

    /// <summary>
    /// Disable touch events.
    /// </summary>
	private void OnDisable()
	{
		if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan -= touchBeganHandler;
			TouchManager.Instance.TouchesMoved -= touchMovedHandler;
			TouchManager.Instance.TouchesEnded -= touchEndedHandler;
		}
	}


    /// <summary>
    /// Touch detected or marker has added to game area.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	private void touchBeganHandler(object sender, TouchEventArgs e)
    {
        int testMarker = SelectedTestMarker();

        foreach (TouchPoint touch in e.Touches)
        {
            // Use tag to identify marker and activate it.
            string markerTags = touch.Tags.ToString();
            //Debug.Log("Marker Tags=" + markerTags);
            if (markerDictionary.ContainsKey(markerTags))
            {
                //Debug.Log("Activating marker");
                ActivateMarker(markerDictionary[markerTags], touch);
            }

            // Used to test markers
            if (markerTags == "Mouse")
            {
                int markerIndex = SelectedTestMarker();
                if (markerIndex > -1)
                {
                    ActivateMarker(markers[markerIndex], touch);
                }
            }
        }
    }

    /// <summary>
    /// Turn on detected marker.
    /// </summary>
    /// <param name="marker">Marker to be dusplayed.</param>
    /// <param name="touch">Position and rotaton of marker</param>
	void ActivateMarker(MarkerController marker, TouchPoint touch)
	{
        if (!marker.gameObject.activeSelf)
        {
            // Place marker image as position of physical marker
            Debug.Log("Applying new position");
            marker.ApplyPositionRotation(touch.Position, Quaternion.Euler(0.0f, 0.0f, -GetAngle(touch)));
            Debug.Log("Showing marker");
            marker.ShowMarker();
        }
	}

    /// <summary>
    /// Touch or marker moved.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void touchMovedHandler(object sender, TouchEventArgs e)
    {
        foreach(TouchPoint touch in e.Touches)
        {

            // Use tag to identify marker and update position.
            string markerTags = touch.Tags.ToString();
            //Debug.Log("Marker " + markerTags + " = " + touch.Position);
            if (markerDictionary.ContainsKey(markerTags))
            {
                MarkerController marker = markerDictionary[markerTags];
                marker.ApplyPositionRotation(touch.Position, Quaternion.Euler(0.0f, 0.0f, -GetAngle(touch)));
            }

            if (markerTags == "Mouse")
            {
                int markerIndex = SelectedTestMarker();
                if (markerIndex > -1)
                {
                    markers[markerIndex].ApplyPositionRotation(touch.Position, Quaternion.Euler(0.0f, 0.0f, -GetAngle(touch)));
                }
            }
        }
    }

    /// <summary>
    /// Touch ended or marker has been removed from game area.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	private void touchEndedHandler(object sender, TouchEventArgs e)
	{
		foreach(TouchPoint touch in e.Touches)
        {
            // Use tag to identify marker and deactivate it.
            string markerTags = touch.Tags.ToString();
            if (markerDictionary.ContainsKey(markerTags))
            {
                DeactivateMarker(markerDictionary[markerTags]);
            }
            if (markerTags == "Mouse")
            {
                int markerIndex = SelectedTestMarker();
                if (markerIndex > -1)
                {
                    DeactivateMarker(markers[markerIndex]);
                }
            }
        }
    }

    /// <summary>
    /// Turn off removed marker.
    /// </summary>
    /// <param name="marker">Maker to be hidden.</param>
    void DeactivateMarker(MarkerController marker)
	{
        // Remove marker after a short delay. This will stop the marker from glitching if the table is unable to read it for a moment.
        StartCoroutine(marker.HideMarker(marker.Timeout));
	}

    /// <summary>
    /// Read angle from TouchPoint and return in degrees.
    /// </summary>
    /// <param name="touch">TouchPoint object</param>
    /// <returns>Angle in degrees</returns>
    float GetAngle(TouchPoint touch)
    {
        float angle = 0.0f;
        if (touch.Properties.ContainsKey("Angle"))
             angle = Mathf.Rad2Deg * float.Parse(touch.Properties["Angle"].ToString());
        return angle;
    }

    private int SelectedTestMarker()
    {
        int marker = -1;

        if (Input.GetKey(KeyCode.Alpha1))
        {
            marker = 0;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            marker = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            marker = 2;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            marker = 3;
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            marker = 4;
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            marker = 5;
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            marker = 6;
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            marker = 7;
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            marker = 8;
        }
        else if (Input.GetKey(KeyCode.Alpha0))
        {
            marker = 9;
        }
        else if (Input.GetKey(KeyCode.Minus))
        {
            marker = 10;
        }
        else if (Input.GetKey(KeyCode.Equals))
        {
            marker = 11;
        }
        return marker;
    }
}
