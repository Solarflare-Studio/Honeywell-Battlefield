using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour {

    TargetJoint2D joint;
    bool mouseDown;
    Image touch;

	// Use this for initialization
	void Start () {
        joint = GetComponent<TargetJoint2D>();
        joint.enabled = false;
        if (touch != null)
        {
            touch = transform.GetChild(0).GetComponent<Image>();
            touch.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0) && !mouseDown)
        {
            mouseDown = true;
            joint.enabled = true;
            joint.anchor = transform.InverseTransformPoint(Input.mousePosition);
            if (touch != null)
            {
                touch.enabled = true;
                touch.transform.localPosition = joint.anchor;
            }
        }

        if (mouseDown)
        {
            joint.target = Input.mousePosition;
        }

        if (!Input.GetMouseButton(0) && mouseDown)
        {
            mouseDown = false;
            joint.enabled = false;
            if (touch != null)
            {
                touch.enabled = false;
            }
        }
	}
}
