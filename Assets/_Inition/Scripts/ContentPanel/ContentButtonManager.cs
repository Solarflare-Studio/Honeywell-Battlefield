using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentButtonManager : MonoBehaviour
{
    [SerializeField]
    private string buttonTextName = "ContentButtonText";

    public Button button;

    public void Initialize(string _buttonName)
    {
        button = GetComponent<Button>();
        transform.GetChildFromName<Text>(buttonTextName).text = _buttonName.ToUpper(); 
    } 
}
