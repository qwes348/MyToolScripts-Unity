using System;
using UnityEngine;
using NaughtyAttributes;

public class Poolable : MonoBehaviour
{
    public string id;
    public int createCountInAwake = 0;
    public bool isUsing = false;
    public bool isAutoPooling;
    [ShowIf("isAutoPooling")]
    public float autoPoolingTime;

    public Action onPop;
    public Action onPush;

    private float poolingTimer = 0f;


    private void Update()
    {
        if (isAutoPooling && isUsing)
        {
            if (poolingTimer < autoPoolingTime)
                poolingTimer += Time.deltaTime;
            else
            {
                poolingTimer = 0f;
                PoolManager.instance.Push(this);
            }
        }
    }

    public void ChangeParent(Transform newParent, bool resetParentOnPush = true)
    {
        transform.parent = newParent;

        if (resetParentOnPush)
        {
            onPush += () => transform.parent = PoolManager.instance.transform;
        }
    }

    public void ResetAutoPoolingTimer()
    {
        poolingTimer = 0f;
    }
}
