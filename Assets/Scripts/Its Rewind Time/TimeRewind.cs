using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimeRewind : MonoBehaviour
{
    private bool isRewinding = false;
    private TimeRewinder timeRewinder;
    public GameObject playerShip;

    private void Start()
    {
        timeRewinder = GetComponent<TimeRewinder>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TriggerRewind();
        }
    }



    private void FixedUpdate()
    {
        if (isRewinding)
        {
            timeRewinder.Rewind();
        }
        else
        {
            timeRewinder.Record();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        timeRewinder.isRewinding = true;
        playerShip.GetComponent<PlayerMovement>().enabled = false;
        playerShip.GetComponent<WeaponSystem>().enabled = false;
    }

    public void StopRewind()
    {
        isRewinding = false;
        timeRewinder.isRewinding = false;
        playerShip.GetComponent<PlayerMovement>().enabled = true;
        playerShip.GetComponent<WeaponSystem>().enabled = true;
    }
    public void TriggerRewind()
    {
        StartCoroutine(RewindCoroutine());
    }

    private IEnumerator RewindCoroutine()
    {
        float rewindStartTime = Time.time;
        float maxRewindDuration = 3f;

        if (rewindStartTime < maxRewindDuration)
        {
            maxRewindDuration = rewindStartTime;
        }

        StartRewind();
        while (Time.time - rewindStartTime < maxRewindDuration)
        {
            if (!timeRewinder.IsAnyRewind())
            {
                break;
            }
            yield return null;
        }
        StopRewind();
        Time.timeScale = 1;
    }

}