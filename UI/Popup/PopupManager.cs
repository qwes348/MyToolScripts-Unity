using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oni;
using System;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class PopupManager : SingletonSerializedMono<PopupManager>
{
    [SerializeField]
    private Popup popupPrefab;
    [SerializeField]
    private Image darkBg;

    [SerializeField]
    Dictionary<PopupInfo.PopupButtonType, PopupButtonInfo> buttonInfoDict;
    
    private List<Popup> popupPool = new List<Popup>();

    private Stack<Popup> activePopupStack;

    public Popup CurrentActivePopup
    {
        get
        {
            if (activePopupStack.Count <= 0)
                return null;

            return activePopupStack.Peek();
        }
        private set
        {
            if (value == null)
                return;

            activePopupStack.Push(value);
        }
    }

    
    private void Awake()
    {

        activePopupStack = new Stack<Popup>();
    }


    public void ShowPopup(PopupInfo info)
    {
        Popup popup = null;
        foreach(var pop in popupPool)
        {
            if(!pop.IsShow)
            {
                popup = pop;
                break;
            }
        }

        if (popup == null)
        {
            popup = Instantiate(popupPrefab, transform);
            popupPool.Add(popup);
        }

        popup.Init(info);
        popup.Show();
        CurrentActivePopup = popup;

        darkBg.enabled = true;
    }

    public PopupButtonInfo GetButtonInfo(PopupInfo.PopupButtonType type)
    {
        if (!buttonInfoDict.ContainsKey(type))
            return null;

        return buttonInfoDict[type];
    }

    public void OnPopupClose(Popup pop)
    {
        darkBg.enabled = false;

        if (pop == CurrentActivePopup)
            activePopupStack.Pop();
    }

    public void CloseCurrentActivePopup()
    {
        if (CurrentActivePopup == null)
            return;

        CurrentActivePopup.Hide();
    }

    public void ShowErrorPopup(string error, Action<PopupInfo.PopupButtonType> act = null)
    {
        string errorContentMsg = "Error : {0}";
        PopupInfo info = new PopupInfo.Builder()
        .SetContent(string.Format(errorContentMsg, error))
        .SetButtons(PopupInfo.PopupButtonType.Close)
        .SetListener(act)
        .Build();

        ShowPopup(info);
    }
}

[Serializable]
public class PopupButtonInfo
{
    [SerializeField]
    private string buttonText;

    public string ButtonText { get => buttonText; }
}
