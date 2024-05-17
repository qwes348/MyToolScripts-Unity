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

    private AsyncOperationHandle currentDownloadingHandle;
    private List<string> labels;

    public Action onDownloadComplete;
    public enum AddressableLabels { UiAssets, CharacterImages, SceneAssets, AudioAssets }

    private void Start()
    {
        // enum타입으로 관리하는 모든 라벨을 List<string>으로 변환
        labels = Enum.GetNames(typeof(AddressableLabels)).ToList<string>();

        CheckTheDownloadFileSize();
    }

    [Button]
    public void DownloadBundle()
    {
        currentDownloadingHandle = Addressables.DownloadDependenciesAsync(labels, Addressables.MergeMode.Union);
        currentDownloadingHandle.Completed += (AsyncOperationHandle Handle) =>
        {
            Debug.Log("다운로드 완료!");
            launcherCanvas.SetStatusText("update complete");

            //다운로드가 끝나면 메모리 해제.
            Addressables.Release(Handle);

            onDownloadComplete?.Invoke();
        };

        StartCoroutine(CheckDownloadingPercentCor());
        launcherCanvas.SetStatusText("downloading...");
    }

    IEnumerator CheckDownloadingPercentCor()
    {
        while (!currentDownloadingHandle.IsDone)
        {
            launcherCanvas.UpdateDownloadingPercentage(currentDownloadingHandle.PercentComplete);

            yield return null;
        }

        launcherCanvas.UpdateDownloadingPercentage(1.0f);
    }

    [Button]
    public void CheckTheDownloadFileSize()
    {
        launcherCanvas.SetStatusText("check update...");

        //크기를 확인할 번들 또는 번들들에 포함된 레이블을 인자로 주면 됨.
        //long타입으로 반환되는게 특징임.
        Addressables.GetDownloadSizeAsync(labels).Completed +=
            (AsyncOperationHandle<long> SizeHandle) =>
            {
                if (SizeHandle.Result > 0)
                {
                    launcherCanvas.UpdateWillDownloadSize(SizeHandle.Result);

                    float sizeMB = SizeHandle.Result / 1024f / 1024f;

                    // 다운로드 확인 팝업
                    PopupInfo info = new PopupInfo.Builder()
                    .SetContent(string.Format("{0}MB의 추가 리소스 파일이 있습니다.\n지금 다운로드 하시겠습니까?", sizeMB.ToString("N1")))
                    .SetButtons(PopupInfo.PopupButtonType.Close, PopupInfo.PopupButtonType.Yes)
                    .SetPopupType(PopupInfo.PopupType.TextPopup)
                    .SetListener(btnType =>
                    {
                        switch (btnType)
                        {
                            case PopupInfo.PopupButtonType.Yes:
                                DownloadBundle();
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
                    launcherCanvas.UpdateWillDownloadSize(0);
                    launcherCanvas.UpdateDownloadingPercentage(1f);

                    onDownloadComplete?.Invoke();
                }

                //메모리 해제.
                Addressables.Release(SizeHandle);
            };
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