using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Collections;
using Oni;

public class DownloadAddressable : MonoBehaviour
{
    [SerializeField]
    private LauncherSceneCanvas launcherCanvas;
    [SerializeField]
    private List<AssetLabelReference> labelsForDownload;

    private AsyncOperationHandle currentDownloadingHandle;
    private List<string> labels;
    private long patchSize;
    private Dictionary<string, long> patchMap = new Dictionary<string, long>();

    public Action onDownloadComplete;

    private void Start()
    {
        // 모든 라벨을 List<string>으로 변환
        labels = new List<string>();
        labelsForDownload.ForEach(lb => labels.Add(lb.labelString));

        Addressables.InitializeAsync().Completed += handle =>
        {
            Addressables.Release(handle);
            launcherCanvas.SetStatusText("Init addressables complete");
            CheckDownloadFileSize();
        };

        onDownloadComplete += GoTitleScene;
    }

    [Button]
    public void CheckDownloadFileSize()
    {
        launcherCanvas.SetStatusText("check update...");
        patchSize = default;

        //크기를 확인할 번들 또는 번들들에 포함된 레이블을 인자로 주면 됨.
        //long타입으로 반환되는게 특징임.
        Addressables.GetDownloadSizeAsync(labels).Completed +=
            (AsyncOperationHandle<long> SizeHandle) =>
            {
                if (SizeHandle.Result > 0)
                {
                    string fileSizeString = Tools.BytesToFileSizeString(SizeHandle.Result);

                    patchSize += SizeHandle.Result;

                    PopupInfo info = new PopupInfo.Builder()
                    .SetContent(string.Format("{0}의 추가 리소스 파일이 있습니다.\n지금 다운로드 하시겠습니까?", fileSizeString))
                    .SetButtons(PopupInfo.PopupButtonType.Close, PopupInfo.PopupButtonType.Yes)
                    .SetPopupType(PopupInfo.PopupType.TextPopup)
                    .SetListener(btnType =>
                    {
                        switch (btnType)
                        {
                            case PopupInfo.PopupButtonType.Yes:
                                //DownloadBundle();
                                //V2
                                StartCoroutine(PatchFilesCor());
                                StartCoroutine(CheckDownloadingProgressCor());
                                break;
                            case PopupInfo.PopupButtonType.No:
                                Application.Quit();
                                break;
                        }
                    })
                    .Build();

                    PopupManager.Instance.ShowPopup(info);

                    launcherCanvas.SetStatusText("update exist");
                }
                else
                {
                    Debug.Log("이미 모두 다운로드됨");

                    launcherCanvas.SetStatusText("up to date");
                    launcherCanvas.UpdateDownloadingPercentage(1f);

                    onDownloadComplete?.Invoke();
                }

                //메모리 해제.
                Addressables.Release(SizeHandle);
            };
    }

    IEnumerator PatchFilesCor()
    {
        foreach (var label in labels)
        {
            var handle = Addressables.GetDownloadSizeAsync(label);

            yield return handle;

            if (handle.Result != decimal.Zero)
            {
                launcherCanvas.SetStatusText(string.Format("download label: {0}", label));
                yield return StartCoroutine(DownloadLabel(label));
            }

            Addressables.Release(handle);
        }

        Debug.Log("다운로드 완료!");
        launcherCanvas.SetStatusText("patch complete");
        onDownloadComplete?.Invoke();
    }

    /// <summary>
    /// 라벨을 하나 하나 받기 (새로운 방식)
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IEnumerator DownloadLabel(string label)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        patchMap.Add(label, 0);

        var handle = Addressables.DownloadDependenciesAsync(label, false);

        while (!handle.IsDone)
        {
            patchMap[label] = handle.GetDownloadStatus().DownloadedBytes;
            yield return wait;
        }

        patchMap[label] = handle.GetDownloadStatus().TotalBytes;
        Addressables.Release(handle);
    }

    /// <summary>
    /// 모든 라벨을 한꺼번에 받기(이전에 쓰던 방식)
    /// </summary>
    public void DownloadBundle()
    {
        currentDownloadingHandle = Addressables.DownloadDependenciesAsync(labels, Addressables.MergeMode.Union);
        currentDownloadingHandle.Completed += (AsyncOperationHandle Handle) =>
        {
            Debug.Log("다운로드 완료!");
            launcherCanvas.SetStatusText("patch complete");

            //다운로드가 끝나면 메모리 해제.
            Addressables.Release(Handle);

            onDownloadComplete?.Invoke();
        };

        StartCoroutine(CheckDownloadingProgressCor());
        launcherCanvas.SetStatusText("downloading...");
    }

    IEnumerator CheckDownloadingProgressCor()
    {
        // 예전 방식(모든 라벨 한꺼번에 다운로드)
        //while(!currentDownloadingHandle.IsDone)
        //{
        //    launcherCanvas.UpdateDownloadingPercentage(currentDownloadingHandle.PercentComplete);

        //    yield return null;
        //}

        //launcherCanvas.UpdateDownloadingPercentage(1.0f);

        // V2
        var wait = new WaitForEndOfFrame();
        long currentTotalDownloaded = 0;

        while (true)
        {
            currentTotalDownloaded = patchMap.Sum(pair => pair.Value);

            if (currentTotalDownloaded == patchSize && patchSize > 0)
            {
                launcherCanvas.UpdateDownloadingPercentage(1.0f);
                launcherCanvas.UpdateDownloadingSize(patchSize, patchSize);
                yield break;
            }
            else
            {
                launcherCanvas.UpdateDownloadingPercentage((float)currentTotalDownloaded / patchSize);
                launcherCanvas.UpdateDownloadingSize(currentTotalDownloaded, patchSize);
            }

            currentTotalDownloaded = 0;
            yield return wait;
        }
    }

    public void GoTitleScene()
    {
        QRSceneManager.Instance.LoadSceneFromAddressable(QRSceneManager.TitleScene, null, null, null);
    }

    [Button]
    public void ClearCache()
    {
        StartCoroutine(ClearCacheCor());
    }

    IEnumerator ClearCacheCor()
    {
        foreach (var tmp in Addressables.ResourceLocators)
        {
            var async = Addressables.ClearDependencyCacheAsync(tmp.Keys, false);
            yield return async;
            Addressables.Release(async);
        }

        Caching.ClearCache();
        Addressables.UpdateCatalogs();
    }
}

// Tools
public static string BytesToFileSizeString(long byteCnt)
{
    string size = "0 Bytes";

    if (byteCnt >= MathF.Pow(1024f, 3f))
    {
        size = string.Format("{0:N2} GB", byteCnt / Mathf.Pow(1024f, 3f));
    }
    else if (byteCnt >= Mathf.Pow(1024f, 2f))
    {
        size = string.Format("{0:N2} MB", byteCnt / Mathf.Pow(1024f, 2f));
    }
    else if (byteCnt >= 1024f)
    {
        size = string.Format("{0:N2} KB", byteCnt / 1024f);
    }
    else
    {
        size = byteCnt.ToString() + "Bytes";
    }

    return size;
}
