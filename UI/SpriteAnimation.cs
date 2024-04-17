using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    [PreviewField]
    public Sprite[] sprites;
    public int framesPerSprite = 6;
    public bool loop = true;
    public bool destroyOnEnd = false;
    public UnityEvent onEnd;

    private int index = 0;
    private SpriteRenderer rend;
    private int frame = 0;

    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    [Button]
    public void Play()
    {
        index = 0;
        if (rend == null)
            Awake();
        rend.sprite = sprites[index];
        gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if (!loop && index == sprites.Length)
            return;
        frame++;
        if (frame < framesPerSprite)
            return;
        rend.sprite = sprites[index];
        frame = 0;
        index++;
        if (index >= sprites.Length)
        {
            if (loop)
                index = 0;
            if (destroyOnEnd)
                Destroy(gameObject);
            if (onEnd != null)
                onEnd?.Invoke();
        }
    }
}
