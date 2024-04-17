using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Image))]
public class ImageAnimation : MonoBehaviour
{
    [PreviewField]
    public Sprite[] sprites;
    public int framesPerSprite = 6;
    public bool loop = true;
    public float loopInterval = 0f;
    public bool destroyOnEnd = false;
    public UnityEvent onEnd;

    private int index = 0;
    private Image image;
    private int frame = 0;
    private float intervalTimer = 0f;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    [Button]
    public void Play()
    {        
        index = 0;
        if (image == null)
            Awake();
        image.sprite = sprites[index];
        gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if (!loop && index == sprites.Length)
            return;
        else if(loop && index == sprites.Length && intervalTimer < loopInterval)
        {
            intervalTimer += Time.fixedDeltaTime;
            return;
        }
        else if (loop && index == sprites.Length && intervalTimer >= loopInterval)
        {
            intervalTimer = 0f;
            index = 0;
        }

        frame++;
        if (frame < framesPerSprite)
            return;
        image.sprite = sprites[index];
        frame = 0;
        index++;
        if (index >= sprites.Length)
        {
            ////if (loop)
            ////    index = 0;
            if (destroyOnEnd)
                Destroy(gameObject);
            if (onEnd != null)
                onEnd?.Invoke();
        }
    }
}