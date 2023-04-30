using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
    void Awake()
    {
        if (AudioManager.instance != null)
        {
            Destroy(AudioManager.instance.gameObject);
        }
    }
}
