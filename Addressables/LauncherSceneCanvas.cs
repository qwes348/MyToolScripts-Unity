using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LauncherSceneCanvas : MonoBehaviour
{
    [SerializeField]
    private Text willDownloadSize;
    [SerializeField]
    private Text downloadingPercentageText;
    [SerializeField]
    private Text statusText;
    [SerializeField]
    private SlicedFilledImage progressBar;

    public void UpdateWillDownloadSize(long sizeByte)
    {
        if (sizeByte > 0)
            willDownloadSize.text = string.Format("{0} MB", (sizeByte / 1024f / 1024f).ToString("N0"));
        else
            willDownloadSize.text = "0 MB";
    }

    public void UpdateDownloadingPercentage(float percent)
    {
        progressBar.fillAmount = percent;

        percent *= 100f;
        downloadingPercentageText.text = string.Format("{0}%", percent.ToString("N1"));        
    }

    public void SetStatusText(string content)
    {
        statusText.text = content;
    }
}
