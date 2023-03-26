using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoosterCommand : ICommand
{
    private GameObject booster;

    public ActivateBoosterCommand(GameObject booster)
    {
        this.booster = booster;
    }

    public void Execute()
    {
        booster.SetActive(true);
    }
}