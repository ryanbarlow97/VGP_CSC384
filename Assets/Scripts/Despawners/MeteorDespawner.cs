using UnityEngine;

public class MeteorDespawner : MonoBehaviour
{
    public float offset = 2f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ICommand despawnCommand = new DespawnCommand(gameObject, offset);
        despawnCommand.Execute();
    }
}
