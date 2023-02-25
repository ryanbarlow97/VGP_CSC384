using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VolumeManager
{
    private const string VOLUME_KEY = "Volume";

    public static float volume = 1.0f;
    public static void SaveVolumePreference()
    {
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
    public static float GetSavedVolumePreference()
    {
        // Load the last volume value from PlayerPrefs
        return PlayerPrefs.GetFloat(VOLUME_KEY, 1f);
    }
}
