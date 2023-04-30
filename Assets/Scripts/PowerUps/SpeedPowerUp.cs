using System.Collections;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float speedMultiplier = 2f;
    public float duration = 5f;
    public Material powerupMaterial;

    private Material originalMaterial;
    private Renderer objectRenderer;
    private bool isActive;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerShip"))
        {
            // Destroy the powerup
            Destroy(gameObject);

            PowerUpEventManager.Instance.TriggerEvent("SpeedPowerUp", this);
        }
    }
}
