using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Oni;
using Sirenix.OdinInspector;

public class PopupCurrencyContent : SerializedMonoBehaviour
{
    [SerializeField]
    private Image currencyImage;
    [SerializeField]
    private Text currencyText;
    [SerializeField]
    private Dictionary<PopupInfo.CurrencyType, Sprite> currencyResourceDict;

    public void SetContent(PopupInfo.CurrencyType type, int amount)
    {
        switch (type)
        {
            case PopupInfo.CurrencyType.None:
                gameObject.SetActive(false);
                return;
            case PopupInfo.CurrencyType.Gold:
            case PopupInfo.CurrencyType.Cash:
                currencyImage.sprite = currencyResourceDict[type];
                break;
        }

        currencyText.text = amount.ToString();
    }
}
