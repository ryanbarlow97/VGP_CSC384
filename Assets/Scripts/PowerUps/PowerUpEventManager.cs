using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEventManager : MonoBehaviour
{
    public static PowerUpEventManager Instance { get; private set; }

    private Dictionary<string, Action<object>> eventDictionary = new Dictionary<string, Action<object>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartListening(string eventName, Action<object> listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] += listener;
        }
        else
        {
            eventDictionary[eventName] = listener;
        }
    }

    public void StopListening(string eventName, Action<object> listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= listener;
        }
    }

    public void TriggerEvent(string eventName, object eventParam)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName]?.Invoke(eventParam);
        }
    }
}
