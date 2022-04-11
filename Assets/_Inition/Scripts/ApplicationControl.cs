using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ApplicationControl : MonoBehaviour 
{

    private void Start()
    {
#if !UNITY_EDITOR
        Cursor.visible = false;
#endif
    }

    void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
            Debug.Log("Quit application");
			Application.Quit();
		}
		if (Input.GetKeyDown(KeyCode.F12))
		{
            SceneManager.LoadScene(0);
		}
	}
}
