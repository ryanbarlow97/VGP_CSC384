using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateBoosterCommand : ICommand
{
    private GameObject booster;

    public DeactivateBoosterCommand(GameObject booster)
    {
        this.booster = booster;
    }

    public void Execute()
    {
        booster.SetActive(false);
    }
}