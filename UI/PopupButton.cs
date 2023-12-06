using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Oni
{
    public class PopupButton : Button
    {
        [SerializeField]
        private Text buttonText;
        [SerializeField]
        private PopupInfo.PopupButtonType buttonType;

        public PopupInfo.PopupButtonType ButtonType { get => buttonType; }
        public Popup ParentPopup { get; set; }

        public void Init(string buttonStr, PopupInfo.PopupButtonType buttonType, Popup parentPopup)
        {
            if (buttonText == null)
                buttonText = GetComponentInChildren<Text>();

            buttonText.text = buttonStr;
            this.buttonType = buttonType;
            this.ParentPopup = parentPopup;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            ParentPopup.OnButtonClicked(this);
        }
    }
}
