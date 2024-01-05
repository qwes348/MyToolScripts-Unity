using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Gui;
using System;
using Sirenix.OdinInspector;
using LeTai.Asset.TranslucentImage;

namespace Oni
{
    [RequireComponent(typeof(LeanWindow))]
    public class Popup : SerializedMonoBehaviour
    {
        [SerializeField]
        protected PopupInfo.PopupType myPopupType;
        [SerializeField]
        protected Text contentText;
        [SerializeField]
        protected Text smallContentText;
        [SerializeField]
        protected Transform buttonParent;
        [SerializeField]
        protected List<PopupCurrencyContent> currencyContents;

        [SerializeField]
        protected TranslucentImage glassImage;

        protected LeanWindow myLeanWindow;
        protected Action<PopupInfo.PopupButtonType> buttonClickedAction;
        protected List<PopupButton> myButtons = new List<PopupButton>();

        #region 프로퍼티
        public bool IsShow
        {
            get
            {
                if (myLeanWindow == null)
                    myLeanWindow = GetComponent<LeanWindow>();

                return myLeanWindow.On;
            }
        }
        public PopupInfo.PopupType MyPopupType { get => myPopupType; }
        #endregion

        public virtual void Init(PopupInfo info)
        {
            contentText.text = info.Content;
            smallContentText.text = info.SmallContent;
            buttonClickedAction = info.Listener;
            SetButtons(info.ButtonTypes);

            foreach(var cur in currencyContents)
            {
                if (cur.gameObject == null)
                    continue;
                cur.gameObject.SetActive(false);
            }

            if(info.CurrencyDict != null && info.CurrencyDict.Count > 0)
            {
                currencyContents[0].transform.parent.gameObject.SetActive(true);
                SetCurrency(info);
            }
            else
            {
                if (currencyContents.Count > 0)
                    currencyContents[0].transform.parent.gameObject.SetActive(false);
            }
        }

        protected virtual void SetButtons(PopupInfo.PopupButtonType[] buttonTypes)
        {
            myButtons = new List<PopupButton>();
            for (int i = 0; i < buttonTypes.Length; i++)
            {
                PopupButton clone;
                var buttonInfo = PopupManager.Instance.GetButtonInfo(buttonTypes[i]);
                if (buttonInfo != null && buttonInfo.Prefab != null)
                    clone = Instantiate(buttonInfo.Prefab, buttonParent);
                else
                    clone = Instantiate(PopupManager.Instance.DefaultPopupButtonPrefab, buttonParent);

                var info = PopupManager.Instance.GetButtonInfo(buttonTypes[i]);
                clone.Init(info.ButtonText, buttonTypes[i], this);

                myButtons.Add(clone);
            }
        }

        protected virtual void SetCurrency(PopupInfo info)
        {
            int i = 0;
            foreach (var pair in info.CurrencyDict)
            {
                while (currencyContents.Count <= i)
                {
                    currencyContents.Add(Instantiate(currencyContents[0], currencyContents[0].transform.parent));
                }
                currencyContents[i].SetContent(pair.Key, pair.Value);
                currencyContents[i].gameObject.SetActive(true);
                i++;
            }            
        }

        public virtual void Show()
        {
            if (myLeanWindow == null)
                myLeanWindow = GetComponent<LeanWindow>();

            if(glassImage.source == null)
            {
                TranslucentImageSource source = FindObjectOfType<TranslucentImageSource>();
                if (source != null)
                    glassImage.source = source;
            }
            myLeanWindow.On = true;
        }

        public virtual void Hide()
        {
            if (myLeanWindow == null)
                myLeanWindow = GetComponent<LeanWindow>();
            
            foreach(var btn in myButtons)
            {
                Destroy(btn.gameObject);
            }

            PopupManager.Instance.OnPopupClose(this);

            myLeanWindow.On = false;
        }

        public virtual void OnButtonClicked(PopupButton btn)
        {
            buttonClickedAction?.Invoke(btn.ButtonType);
            //PopupManager.Instance.CloseCurrentActivePopup();
            // 버튼을 누르면 어떤버튼이든 팝업이 꺼지도록 세팅
            Hide();
        }
    }
}
