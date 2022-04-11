using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using Inition.HoneyWell.ParseData;

public class ContentManager : MonoBehaviour
{
    [SerializeField]
    private string contentsButtonResourceName = "ContentButton";
    [SerializeField]
    private string contentsInfoResourceName = "ContentsInfoPanelPrefab";
    private ContentButtonManager[] contentButtonManagers; 
    private DataObject_Content[] contentsData;

    private ContentInfoManager contentInfoManagerForm;
    private int lastSelectedIndex = -1;
    private int Lastindex;

    IList<ContentInfoManager> list = new List<ContentInfoManager>();

    public void Initialize(DataObject_Content[] _contents)
    { 
        contentsData = _contents;

        contentButtonManagers = new ContentButtonManager[contentsData.Length];

        for (int i = 0; i < contentsData.Length; i++)
        {
            contentButtonManagers[i] = Instantiate(Resources.Load(contentsButtonResourceName) as GameObject).GetComponent<ContentButtonManager>();
            contentButtonManagers[i].transform.SetParent(transform);
            contentButtonManagers[i].transform.localPosition = Vector3.zero;
            contentButtonManagers[i].transform.localRotation = Quaternion.identity;
            contentButtonManagers[i].Initialize(contentsData[i].title);
            int buttonIndexHelper = i;

            contentButtonManagers[i].button.onClick.AddListener(() => ContentButtonPressed(buttonIndexHelper));
      
        }

        CreateInfoBox();
    }

    void ContentButtonPressed(int _index)
    {
        if (lastSelectedIndex != _index)
        {
            
            lastSelectedIndex = _index;
            Debug.Log("Content button with index '" + _index + "' was pressed!");

            for (int i = 0; i < contentButtonManagers.Length; i++)
            {
                ColorBlock colours = contentButtonManagers[i].button.colors;

                if (i != _index)
                    colours.colorMultiplier = 0.1f;
                else
                    colours.colorMultiplier = 1f;

                contentButtonManagers[i].button.colors = colours;

                if (i > _index)
                {
                    contentButtonManagers[i].transform.SetSiblingIndex(i + 1);
                }
                else
                {
                    contentButtonManagers[i].transform.SetSiblingIndex(i);
                }
            }

            PopulateInfoBox(lastSelectedIndex);
        }

    }

    void CreateInfoBox()
    {
        contentInfoManagerForm = Instantiate(Resources.Load(contentsInfoResourceName) as GameObject).GetComponent<ContentInfoManager>();
        contentInfoManagerForm.transform.SetParent(transform); 
        contentInfoManagerForm.transform.localPosition = Vector3.zero;
        contentInfoManagerForm.transform.localRotation = Quaternion.identity;
        contentInfoManagerForm.OnSizeSet += PositionOffset;

        //Add list to 
        list.Add(contentInfoManagerForm);
    }

    void HideInfoBox()
    {
        list.RemoveAt(1);
    }

    void PopulateInfoBox(int _index)
    {
       
        contentInfoManagerForm.Populate(contentsData[_index].body);
        contentInfoManagerForm.transform.SetSiblingIndex(_index+1);
    }

    void PositionOffset(float _offset)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, -_offset/2, transform.localPosition.z);
    }
}
