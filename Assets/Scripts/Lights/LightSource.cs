using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;

    [SerializeField] protected float maxSize;
    [SerializeField] protected float minSize;
    [SerializeField] protected float currentSize;

    [Header("Blink configuration")]
    [SerializeField] protected float offsetBlink;
    [SerializeField] protected float blinkSpeed = 1;

    protected bool scalingBig;
    protected Vector3 bigScale;
    protected Vector3 smallScale;

    protected virtual void Start()
    {
        scalingBig = true;
        smallScale = new Vector3(currentSize - offsetBlink, currentSize - offsetBlink, 1);
        rectTransform.localScale = smallScale;
        bigScale = new Vector3(currentSize + offsetBlink, currentSize + offsetBlink, 1);
    }

    protected virtual void Update()
    {
        LightBlink();
    }

    private void LightBlink()
    {
        if (scalingBig)
        {
            if (rectTransform.localScale.x <= bigScale.x || rectTransform.localScale.y <= bigScale.y)
            {
                rectTransform.localScale = new Vector3(rectTransform.localScale.x + (Time.deltaTime * blinkSpeed), rectTransform.localScale.y + (Time.deltaTime * blinkSpeed), 1);
            }
            else
            {
                scalingBig = false;
            }

        }
        else
        {
            if (rectTransform.localScale.x >= smallScale.x || rectTransform.localScale.y >= smallScale.y)
            {
                rectTransform.localScale = new Vector3(rectTransform.localScale.x - (Time.deltaTime * blinkSpeed), rectTransform.localScale.y - (Time.deltaTime * blinkSpeed), 1);
            }
            else
            {
                scalingBig = true;
            }
        }
    }

    public void ChangeCurrentSize(float valueToAdd)
    {
        currentSize += valueToAdd;

        if (currentSize < minSize)
        {
            currentSize = minSize;
        }
        if (currentSize > maxSize)
        {
            currentSize = maxSize;
        }
        bigScale = new Vector3(currentSize + offsetBlink, currentSize + offsetBlink, 1);
        smallScale = new Vector3(currentSize - offsetBlink, currentSize - offsetBlink, 1);
    }
}
