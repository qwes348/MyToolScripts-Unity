using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    [SerializeField]
    private float desireWidth = 4f;
    [SerializeField]
    private float desireHeight = 3f;

    void Start()
    {
        //Camera camera = Camera.main;
        Camera camera = GetComponent<Camera>();
        if (camera == null)
            return;

        Rect rect = camera.rect;

        float scaleheight = ((float)Screen.width / Screen.height) / (desireWidth / desireHeight);
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
}