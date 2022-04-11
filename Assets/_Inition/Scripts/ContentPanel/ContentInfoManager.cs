using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentInfoManager : MonoBehaviour
{
    public delegate void SizeSet(float _height); 
    public SizeSet OnSizeSet; 

    [SerializeField]
    private string infoTextName = "InfoText";
    [SerializeField]
    private string contentOutlineImageName = "ContentOutlineImage";

    private Text textRef;
    private RectTransform backgroundRTRef;
    private RectTransform outlineRTRef;

    public void Populate(string _infoText)
    {
        gameObject.SetActive(true);

        textRef = transform.GetChildFromName<Text>(infoTextName);
        backgroundRTRef = GetComponent<RectTransform>();
        outlineRTRef = transform.GetChildFromName<RectTransform>(contentOutlineImageName);

        textRef.text = _infoText.ToUpper();
        Canvas.ForceUpdateCanvases();
        SetSize();
        //Invoke("SetSize", 0.02f);//it needs a frame to refresh...
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }

    void SetSize()
    {
        //set them up, so they have the same height as the text, that will be dynamically set...
        backgroundRTRef.sizeDelta = new Vector2(backgroundRTRef.sizeDelta.x, textRef.rectTransform.rect.height + 20f);
        outlineRTRef.sizeDelta = new Vector2(backgroundRTRef.sizeDelta.x, textRef.rectTransform.rect.height + 20f);
        //transform.SetSiblingIndex(index + delta);

        if(OnSizeSet != null)
        {
            OnSizeSet(textRef.rectTransform.rect.height);
        }
    }
}
