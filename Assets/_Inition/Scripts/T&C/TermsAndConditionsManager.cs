using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inition.Effects.FadeInOut;

public class TermsAndConditionsManager : MonoBehaviour
{
    [SerializeField]
    private string termsAndConditionsPanelName = "TermsAndConditionsPanel";
    private UiCanvasGroup_FadeEffect panelCanvasGroupRef;

    [SerializeField]
    private string enableTandCButtonName = "TermsAndConditionsButton";
    private UiCanvasGroup_FadeEffect tandCButtonCanvasGroupRef;
    private Button tcShowButtonRef;

    [SerializeField]
    private string closeTandCPanelButtonName = "T&CCloseButton";
    private Button closeButtonRef;

    [SerializeField]
    private string termsContentButtonName = "T&CContentButton";
    private Button termsContentButton;
    private Text termsBtnText;

    [SerializeField]
    private string privacyPolicyButtonName = "PrivacyPolicyContentButton";
    private Button privacyPolicyButton;
    private Text privacyBtnText;

    [SerializeField]
    private string termsAndConditionsHolder = "TermsAndConditionsScrollView"; 
    private GameObject tandcHolderRef;

    [SerializeField]
    private string privacyPolicyHolder = "PrivacyPolicyScrollView"; 
    private GameObject ppHolderRef;

    private bool firstStartSetup = true;
   
    private void Start()
    {
        panelCanvasGroupRef = transform.GetChildFromName<UiCanvasGroup_FadeEffect>(termsAndConditionsPanelName);
        tandCButtonCanvasGroupRef = transform.GetChildFromName<UiCanvasGroup_FadeEffect>(enableTandCButtonName);
        tcShowButtonRef = transform.GetChildFromName<Button>(enableTandCButtonName);
        tcShowButtonRef.onClick.AddListener(() => ShowTAndC());

        closeButtonRef = transform.GetChildFromName<Button>(closeTandCPanelButtonName);
        closeButtonRef.onClick.AddListener(() => HideTAndC());
        closeButtonRef.interactable = false;

        tandcHolderRef = transform.GetChildFromName<Transform>(termsAndConditionsHolder).gameObject; 
        ppHolderRef = transform.GetChildFromName<Transform>(privacyPolicyHolder).gameObject;
        ppHolderRef.SetActive(false);

        termsContentButton = transform.GetChildFromName<Button>(termsContentButtonName);
        termsContentButton.onClick.AddListener(() => SwitchMode(true));
        
        termsBtnText = termsContentButton.GetComponentInChildren<Text>();

        privacyPolicyButton = transform.GetChildFromName<Button>(privacyPolicyButtonName);
        privacyPolicyButton.onClick.AddListener(() => SwitchMode(false));
      
        privacyBtnText = privacyPolicyButton.GetComponentInChildren<Text>();

        SwitchMode(true); 
    }

    private void ShowTAndC()
    {
        tcShowButtonRef.interactable = false;
        closeButtonRef.interactable = true;
        panelCanvasGroupRef.FadeIn();
        tandCButtonCanvasGroupRef.FadeOut();

        if(firstStartSetup)
        { 
            Invoke("DelayedInvoke", 0.1f);
            firstStartSetup = false;
        }
    }

    void DelayedInvoke()//fixes weird bug with the table - on first play the text is not visible unless you press on the button. This doesn't happen in the editor :(
    {
        privacyPolicyButton.onClick.Invoke();
        termsContentButton.onClick.Invoke();
    }

    private void HideTAndC()
    { 
        tcShowButtonRef.interactable = true;
        closeButtonRef.interactable = false;
        panelCanvasGroupRef.FadeOut(); 
        tandCButtonCanvasGroupRef.FadeIn();
        Invoke("DelayedInvoke", 0.25f);  //this swould force switch back to the first menu...
    }

    void SwitchMode(bool _tandEnabled)
    { 
        tandcHolderRef.SetActive(_tandEnabled);
        ppHolderRef.SetActive(!_tandEnabled); 

        if(_tandEnabled)
        {
            termsBtnText.color = new Color(0.62f, 0.14f, 0.125f); 
            privacyBtnText.color = Color.grey;
        }
        else
        {
            termsBtnText.color = Color.grey;
            privacyBtnText.color = new Color(0.62f, 0.14f, 0.125f);
        } 
    } 
}
