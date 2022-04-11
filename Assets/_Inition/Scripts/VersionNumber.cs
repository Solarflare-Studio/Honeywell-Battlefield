using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

[assembly:AssemblyVersion("1.0.*")]
public class VersionNumber : MonoBehaviour {

    /// <summary>
    /// Ensure only one VersionNumber script is active.
    /// </summary>
    static VersionNumber instance;

    /// <summary>
    /// Application name displayed before version number.
    /// </summary>
    public string appName;

    /// <summary>
    /// Show version at start?
    /// </summary>
    public bool showAtStart = true;
    /// <summary>
    /// Show version with defined key press.
    /// </summary>
	public KeyCode showWithKey;
    /// <summary>
    /// Time to show version for.
    /// </summary>
    public float timeToShow = 20.0f;

    /// <summary>
    /// Version string
    /// </summary>
    private string version;
    /// <summary>
    /// Elapsed time since version was displayed.
    /// </summary>
    private float elapsedTime;
    /// <summary>
    /// Is version to be displayed now?
    /// </summary>
    private bool showVersion = false;

    /// <summary>
    /// Position of version text.
    /// </summary>
    Rect position;

    /// <summary>
    /// Gets the version.
    /// </summary>
    /// <value>The version.</value>
    public string Version
    {
        get
        {
            if (version == null)
            {
                Version ver = Assembly.GetExecutingAssembly().GetName().Version;
                version = ver.ToString() + " (" + new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * ver.Build + TimeSpan.TicksPerSecond * 2 * ver.Revision)).ToString("dd/MM/yyyy HH:mm") + ")";
            }
            return version;
        }

    }


    /// Use this for initialization
    void Start()
    {
        // Destroy this instance if a VerionNumber script already exists.
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        // Log current version in log file
        Debug.Log(string.Format("Currently running version is {0}", Version));

        if (showAtStart)
        {
            showVersion = true;
        }

        // Position version number at the bottom left of the screen.
        position = new Rect(10.0f, Screen.height - 20.0f, Screen.width - 20.0f, 20.0f);

    }

	void Update()
	{
        // Display version if defined key is pressed.
		if (Input.GetKeyDown (showWithKey))
		{
			TurnOnVersion ();
		}

        // Count time version has been displayed and hide after it hits the maximum time.
        if (showVersion)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > timeToShow)
            {
                showVersion = false;
            }

        }
	}

    /// <summary>
    /// Show version number and start timer.
    /// </summary>
    void TurnOnVersion()
    {
        showVersion = true;
        elapsedTime = 0.0f;
    }

    /// <summary>
    /// Turn off version number
    /// </summary>
    void TurnOffVersion()
	{
		showVersion = false;
	}

    /// <summary>
    /// Show version number if flag set.
    /// </summary>
    void OnGUI()
    {
        if (!showVersion)
        {
            return;
        }

        GUI.contentColor = Color.white;
        GUI.Label(position, String.Format("{0} v{1}", appName, Version));
    }
}
