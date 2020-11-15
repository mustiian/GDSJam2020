using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScale : MonoBehaviour
{
    public bool OnStart;
    public bool Repeat;
    [Range(0.00001f, 1000)]
    public float ScaleDuration;
    public Vector2 ScaleValue;

    private Vector2 originalScale;
    private bool animationIsPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;

        if (OnStart)
            StartAnimation();
    }

    public bool isPlaying() => animationIsPlaying;

    public void StartAnimation(float duration, Vector2 scaleValue, bool repeat = false)
    {
        if (animationIsPlaying)
            return;

        Repeat = repeat;
        ScaleDuration = duration;
        ScaleValue = scaleValue;
        StartCoroutine(AnimationPlay());
    }

    public void StartAnimation()
    {
        if (!animationIsPlaying)
            StartCoroutine(AnimationPlay());
    }

    public void StopAnimation()
    {
        transform.localScale = originalScale;
        animationIsPlaying = false;
        StopCoroutine(AnimationPlay());
    }

    private IEnumerator AnimationPlay()
    {
        float startTime = Time.time;

        float deltaX = 0, deltaY = 0;
        Vector2 realScale = Vector2.zero;

        animationIsPlaying = true;
        do
        {
            while (startTime + ScaleDuration > Time.time)
            {
                deltaX += Time.deltaTime;
                deltaY += Time.deltaTime;
                realScale.x = Mathf.SmoothStep(originalScale.x, ScaleValue.x, deltaX / ScaleDuration);
                realScale.y = Mathf.SmoothStep(originalScale.y, ScaleValue.y, deltaY / ScaleDuration);
                transform.localScale = realScale;
                yield return null;
            }

            startTime = Time.time;
            deltaX = deltaY = 0;

            while (startTime + ScaleDuration > Time.time)
            {
                deltaX += Time.deltaTime;
                deltaY += Time.deltaTime;
                realScale.x = Mathf.SmoothStep(ScaleValue.x, originalScale.x, deltaX / ScaleDuration);
                realScale.y = Mathf.SmoothStep(ScaleValue.y, originalScale.y, deltaY / ScaleDuration);
                transform.localScale = realScale;
                yield return null;
            }

            startTime = Time.time;
            deltaX = deltaY = 0;

        } while (Repeat);

        animationIsPlaying = false;
    }
}
