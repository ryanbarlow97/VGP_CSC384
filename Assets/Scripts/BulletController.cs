using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private ICommand despawnCommand;

    void Start()
    {
        despawnCommand = new DespawnCommand(gameObject);
    }

    void Update()
    {
        despawnCommand.Execute();
    }
}
