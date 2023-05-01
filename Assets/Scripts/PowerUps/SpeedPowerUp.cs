using System.Collections;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float speedMultiplier = 2f;
    public float duration = 5f;
    private Material originalMaterial;
    private Renderer objectRenderer;
    private bool isActive;
    private PowerUpManager powerUpManager;


    private void Start()
    {
        powerUpManager = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<PowerUpManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerShip") && !powerUpManager.IsAnyPowerUpActive())
        {
            // Destroy the powerup
            Destroy(gameObject);

            PowerUpEventManager.Instance.TriggerEvent("SpeedPowerUp", this);
        }
    }
}
