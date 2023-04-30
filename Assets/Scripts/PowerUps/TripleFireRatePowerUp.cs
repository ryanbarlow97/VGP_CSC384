using System.Collections;
using UnityEngine;

public class TripleFireRatePowerUp : MonoBehaviour
{
    public float fireRateMultiplier = 3f;
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerShip"))
        {
            // Destroy the powerup
            Destroy(gameObject);

            PowerUpEventManager.Instance.TriggerEvent("TripleFireRatePowerUp", this);
        }
    }
}
