using System.Collections;
using UnityEngine;

public class TripleFireRatePowerUp : MonoBehaviour
{
    public float fireRateMultiplier = 3f;
    public float duration = 5f;
    private PowerUpManager powerUpManager;

    private void Start()
    {
        powerUpManager = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<PowerUpManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerShip"))
        {
            // Disable the powerup's renderer and collider
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            // Wait for 3.25 seconds and then destroy the powerup
            Destroy(gameObject, 3.25f);

            PowerUpEventManager.Instance.TriggerEvent("TripleFireRatePowerUp", this);
        }
    }
}
