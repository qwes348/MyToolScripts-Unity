using System;
using System.Collections;
using UnityEngine;
using Oni;

public class DeviceChangeCatcher : SingletonMono<DeviceChangeCatcher>
{
    public static event Action<Vector2> OnResolutionChange;
    public static event Action<DeviceOrientation> OnOrientationChange;
    public static float CheckDelay = 0.5f; // How long to wait until we check again.

    static Vector2 resolution;                    // Current Resolution
    static DeviceOrientation orientation;        // Current Device Orientation
    static bool isAlive = true;                    // Keep this script running?

    private void Awake()
    {
        if (Instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(CheckForChange());
    }

    IEnumerator CheckForChange()
    {
        resolution = new Vector2(Screen.width, Screen.height);
        orientation = Input.deviceOrientation;

        while (isAlive)
        {

            // Check for a Resolution Change
            if (resolution.x != Screen.width || resolution.y != Screen.height)
            {
                resolution = new Vector2(Screen.width, Screen.height);
                if (OnResolutionChange != null) OnResolutionChange(resolution);
            }

            // Check for an Orientation Change
            switch (Input.deviceOrientation)
            {
                case DeviceOrientation.Unknown:            // Ignore
                case DeviceOrientation.FaceUp:            // Ignore
                case DeviceOrientation.FaceDown:        // Ignore
                    break;
                default:
                    if (orientation != Input.deviceOrientation)
                    {
                        orientation = Input.deviceOrientation;
                        if (OnOrientationChange != null) OnOrientationChange(orientation);
                    }
                    break;
            }

            yield return new WaitForSeconds(CheckDelay);
        }
    }

    void OnDestroy()
    {
        isAlive = false;
    }

}