using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private bool speedPowerUpActive = false;
    private PowerUpEventManager eventManager;

    private void Start()
    {
        eventManager = FindObjectOfType<PowerUpEventManager>();
        eventManager.StartListening("SpeedPowerUp", ActivateSpeedPowerUp);
    }

    private void OnDestroy()
    {
        eventManager.StopListening("SpeedPowerUp", ActivateSpeedPowerUp);
    }

    private void ActivateSpeedPowerUp(object powerUpObject)
    {
        if (!speedPowerUpActive && powerUpObject is SpeedPowerUp powerUp)
        {
            Debug.Log("hi");

            StartCoroutine(ApplyTemporarySpeedPowerUp(powerUp));
        }
    }

    private IEnumerator ApplyTemporarySpeedPowerUp(SpeedPowerUp speedPowerUp)
    {
        speedPowerUpActive = true;
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();

        playerMovement.acceleration *= speedPowerUp.speedMultiplier;
        
        yield return new WaitForSeconds(speedPowerUp.duration);

        playerMovement.acceleration /= speedPowerUp.speedMultiplier;
        speedPowerUpActive = false;
    }
}
