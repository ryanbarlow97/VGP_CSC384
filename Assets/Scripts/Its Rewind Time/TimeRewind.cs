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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            StopRewind();
            Time.timeScale = 1;
        }
        
        if (isRewinding)
        {
            timeRewinder.rewindElapsedTime += Time.deltaTime;
        }
        else
        {
            timeRewinder.rewindElapsedTime = 0;
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
}