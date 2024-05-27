using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Oni;

public class LauncherSceneCanvas : MonoBehaviour
{
    [SerializeField]
    private Text downloadingSizeProgressText;
    [SerializeField]
    private Text downloadingPercentageText;
    [SerializeField]
    private Text statusText;
    [SerializeField]
    private SlicedFilledImage progressBar;

    public void UpdateDownloadingPercentage(float percent)
    {
        progressBar.fillAmount = percent;

        percent *= 100f;
        downloadingPercentageText.text = string.Format("{0}%", percent.ToString("N1"));        
    }

    public void UpdateDownloadingSize(long current, long total)
    {
        downloadingSizeProgressText.text = string.Format("{0} / {1}", Tools.BytesToFileSizeString(current), Tools.BytesToFileSizeString(total));
    }

    public void SetStatusText(string content)
    {
        statusText.text = content;
    }
}
