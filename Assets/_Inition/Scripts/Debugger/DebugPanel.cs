using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour {

    public static DebugPanel Current
    { get
        {
            return current;
        }
    }

    private static DebugPanel current;
    

    [SerializeField]
    private Text debugPanelText;

	// Use this for initialization
	void Start () {
		if (current == null)
        {
            current = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DebugMessage(string message)
    {
        debugPanelText.text = message;
    }

    public void ClearMessage()
    {
        debugPanelText.text = string.Empty;
    }
}
