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
    private List<PopupPrefabContainer> popupPrefabCon;
    [SerializeField]
    private Image darkBg;

    [SerializeField]
    Dictionary<PopupInfo.PopupButtonType, PopupButtonInfo> buttonInfoDict;
    [SerializeField]
    private PopupButton defaultPopupButtonPrefab;
    
    private List<Popup> popupPool = new List<Popup>();

    private Stack<Popup> activePopupStack;

    #region 프로퍼티
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
    public PopupButton DefaultPopupButtonPrefab { get => defaultPopupButtonPrefab; }
    #endregion


    private void Awake()
    {
        activePopupStack = new Stack<Popup>();
        DontDestroyOnLoad(gameObject);
    }


    public void ShowPopup(PopupInfo info)
    {
        Popup popup = null;
        foreach(var pop in popupPool)
        {
            if(!pop.IsShow && pop.MyPopupType == info.MyPopupType)
            {
                popup = pop;
                break;
            }
        }

        if (popup == null)
        {
            var con = popupPrefabCon.Find(con => con.PopupType == info.MyPopupType);
            if(con == null)
            {
                Debug.LogError("팝업 프리팹이 없습니다");
                return;
            }

            popup = Instantiate(con.Prefab, transform);
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
        string errorContentMsg = "{0}";
        PopupInfo info = new PopupInfo.Builder()
        .SetContent(string.Format(errorContentMsg, error))
        .SetButtons(PopupInfo.PopupButtonType.Close)
        .SetListener(act)
        .Build();

        ShowPopup(info);
    }

    [Button]
    public void ShowDebugPopup(PopupInfo.PopupType tp)
    {
        PopupInfo info = new PopupInfo.Builder()
            .SetTitle("디버그 타이틀")
            .SetContent("디버그 메인 컨텐츠 디버그 메인 컨텐츠 디버그 메인 컨텐츠 디버그 메인 컨텐츠 디버그 메인 컨텐츠")
            .SetSmallContent("디버그 스몰 텍스트")
            .SetButtons(PopupInfo.PopupButtonType.Yes, PopupInfo.PopupButtonType.Close)
            .SetPopupType(tp)
            .Build();

        ShowPopup(info);
    }
}

[Serializable]
public class PopupButtonInfo
{
    [SerializeField]
    private string buttonText;
    [SerializeField]
    private PopupButton prefab;

    public string ButtonText { get => buttonText; }
    public PopupButton Prefab { get => prefab; }
}

[Serializable]
public class PopupPrefabContainer
{
    [SerializeField]
    private PopupInfo.PopupType popupType;
    [SerializeField]
    private Popup prefab;

    public PopupInfo.PopupType PopupType { get => popupType; }
    public Popup Prefab { get => prefab; }
}
