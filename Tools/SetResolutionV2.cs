using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SetResolutionV2 : MonoBehaviour
{
    [Header("Safe비율 설정")]
    // 이 비율을 초과 or 미달 하면 비율을 강제 조정함
    [SerializeField]
    private float minSafeAspectRatio;
    [SerializeField]
    private float maxSafeAspectRatio;

#if UNITY_EDITOR
    [Header("DEBUG : AspectRatio 계산기")]
    [SerializeField]
    private float calc_width;
    [SerializeField]
    private float calc_height;
    [SerializeField]
    private float calc_resultAspectRatio;
#endif

    void Awake()
    {
        float currentScreenRatioDiv = (float)Screen.width / Screen.height;
        if (currentScreenRatioDiv >= minSafeAspectRatio && currentScreenRatioDiv <= maxSafeAspectRatio)
            return;

        Camera camera = GetComponent<Camera>();
        if (camera == null)
            return;

        //Rect rect = camera.rect;
        Rect rect = new Rect(Vector2.zero, Vector2.one);

        float targetRatio;
        if (currentScreenRatioDiv < minSafeAspectRatio)
            targetRatio = minSafeAspectRatio;
        else
            targetRatio = maxSafeAspectRatio;

        //float scaleheight = ((float)Screen.width / Screen.height) / (desireWidth / desireHeight);
        float scaleheight = ((float)Screen.width / Screen.height) / targetRatio;
        float scalewidth = 1f / scaleheight;

        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }

        camera.rect = rect;
    }

    private void Start()
    {
        if (DeviceChangeCatcher.Instance != null)
            DeviceChangeCatcher.OnResolutionChange += OnResolutionChanged;
    }

    private void OnDestroy()
    {
        if (DeviceChangeCatcher.Instance != null)
            DeviceChangeCatcher.OnResolutionChange -= OnResolutionChanged;
    }

    public void OnResolutionChanged(Vector2 newResolution)
    {
        Awake();
    }    

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (calc_width <= 0 || calc_height <= 0)
            return;

        calc_resultAspectRatio = calc_width / calc_height;        
    }
#endif
}

