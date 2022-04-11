using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollVertical : MonoBehaviour {

    public float posX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<UnityEngine.UI.Image>().rectTransform.anchoredPosition = new Vector2(posX, this.GetComponent<UnityEngine.UI.Image>().rectTransform.anchoredPosition.y);
	}
}
