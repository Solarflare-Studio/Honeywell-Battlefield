using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObjectLimiter : MonoBehaviour
{
    [SerializeField]
    private bool dontGoOffScreen = true;

    [SerializeField]
    private bool scaleLimit = true;

    [SerializeField]
    private Vector2 scaleMinMax = new Vector2(0.5f, 1.5f);
    
    private float limitXMin, limitXMax, limitYMin, limitYMax;

    public bool debugShow = false;

    private void Start()
    {
        limitXMin = 0f;
        limitXMax = Screen.width;
        limitYMin = 0f;
        limitYMax = Screen.height; 
    }

    private void Update()
    { 
        if (scaleLimit)
        {
            transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, scaleMinMax.x, scaleMinMax.y), Mathf.Clamp(transform.localScale.y, scaleMinMax.x, scaleMinMax.y), Mathf.Clamp(transform.localScale.z, scaleMinMax.x, scaleMinMax.y));
        }
        if (dontGoOffScreen)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, limitXMin, limitXMax), Mathf.Clamp(transform.position.y, limitYMin, limitYMax), transform.position.z);// Mathf.Clamp(transform.localPosition.y, limitYMin, limitYMax), transform.localPosition.z);
        }
    }
}
