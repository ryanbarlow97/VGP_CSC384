using System.Collections;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float speedMultiplier = 2f;
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerShip"))
        {
            // Destroy the powerup
            Destroy(gameObject);

            PowerUpEventManager.Instance.TriggerEvent("SpeedPowerUp", this);
        }
    }
}
