using Oni;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDemo : MonoBehaviour
{
    public void ShowSimplePopup()
    {
        PopupInfo info = new PopupInfo.Builder()
            .SetContent("This is")
            .SetSmallContent("Simple Popup")
            .SetButtons(PopupInfo.PopupButtonType.Close)
            .Build();

        PopupManager.Instance.ShowPopup(info);
    }

    public void ShowCurrencyPopup()
    {
        PopupInfo info = new PopupInfo.Builder()
            .SetContent("This is")
            .SetSmallContent("Currency Popup")
            .AddCurrencyDict(PopupInfo.CurrencyType.Gold, 100)
            .AddCurrencyDict(PopupInfo.CurrencyType.Cash, 10)
            .SetButtons(PopupInfo.PopupButtonType.Purchase, PopupInfo.PopupButtonType.Close)
            .Build();

        PopupManager.Instance.ShowPopup(info);
    }
}
