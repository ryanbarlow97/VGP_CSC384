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
            // Disable the powerup's renderer and collider
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            // Wait for 3.25 seconds and then destroy the powerup
            Destroy(gameObject, 3.25f);

            PowerUpEventManager.Instance.TriggerEvent("SpeedPowerUp", this);
        }
    }
}
