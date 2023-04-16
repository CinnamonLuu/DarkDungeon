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
    [SerializeField] protected private float blinkTime;

    protected bool scalingBig;
    protected float timeLeft;
    protected Vector3 startScale;
    protected Vector3 bigScale;
    protected Vector3 smallScale;

    private void Start()
    {
        timeLeft = blinkTime;
        scalingBig = true;
        rectTransform.localScale = new Vector3(currentSize, currentSize, 1);
        smallScale = new Vector3(currentSize - offsetBlink, currentSize - offsetBlink, 1);
        rectTransform.localScale = smallScale;
        startScale = rectTransform.localScale;
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
            if (timeLeft > 0f)
            {
                rectTransform.localScale = Vector3.Lerp(startScale, bigScale, 1f - timeLeft / blinkTime);
            }
            else
            {
                scalingBig = false;
                startScale = rectTransform.localScale;
                timeLeft = blinkTime;
            }

        }
        else
        {
            if (timeLeft > 0f)
            {
                rectTransform.localScale = Vector3.Lerp(startScale, smallScale, 1f - timeLeft / blinkTime);
            }
            else
            {
                scalingBig = true;
                startScale = rectTransform.localScale;
                timeLeft = blinkTime;
            }

        }
        timeLeft -= Time.deltaTime;
    }

    private void ChangeMaxSize(float value)
    {
        maxSize = value;
    }

    private void ChangeMinSize(float value)
    {
        minSize = value;
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
