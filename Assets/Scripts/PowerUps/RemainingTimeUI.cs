using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingTimeUI : MonoBehaviour
{
    public RectTransform timerBarFill;
    public float maxTime = 5f;
    private float currentTime;

    public void SetMaxTime(float maxTime)
    {
        this.maxTime = maxTime;
        currentTime = maxTime;
    }

    public void SetCurrentTime(float currentTime)
    {
        this.currentTime = currentTime;
        UpdateTimerBar();
    }

    void Start()
    {
        currentTime = 0;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0f)
        {
            currentTime = 0f;
        }

        UpdateTimerBar();
    }

    private void UpdateTimerBar()
    {
        float heightPercentage = currentTime / maxTime;
        timerBarFill.sizeDelta = new Vector2(timerBarFill.sizeDelta.x, heightPercentage * timerBarFill.parent.GetComponent<RectTransform>().rect.height);
        timerBarFill.anchoredPosition = new Vector2(0, -((1 - heightPercentage) * timerBarFill.parent.GetComponent<RectTransform>().rect.height));
    }

}