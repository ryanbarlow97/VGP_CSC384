using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private bool speedPowerUpActive = false;
    private bool tripleFireRatePowerUpActive = false;
    private PowerUpEventManager eventManager;

    private void Start()
    {
        eventManager = FindObjectOfType<PowerUpEventManager>();
        eventManager.StartListening("SpeedPowerUp", ActivateSpeedPowerUp);
        eventManager.StartListening("TripleFireRatePowerUp", ActivateTripleFireRatePowerUp);
    }

    private void OnDestroy()
    {
        eventManager.StopListening("SpeedPowerUp", ActivateSpeedPowerUp);
        eventManager.StopListening("TripleFireRatePowerUp", ActivateTripleFireRatePowerUp);
    }

    private void ActivateSpeedPowerUp(object powerUpObject)
    {
        if (!speedPowerUpActive && powerUpObject is SpeedPowerUp powerUp)
        {
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

    private void ActivateTripleFireRatePowerUp(object powerUpObject)
    {
        if (!tripleFireRatePowerUpActive && powerUpObject is TripleFireRatePowerUp powerUp)
        {
            StartCoroutine(ApplyTemporaryTripleFireRatePowerUp(powerUp));
        }
    }

    private IEnumerator ApplyTemporaryTripleFireRatePowerUp(TripleFireRatePowerUp tripleFireRatePowerUp)
    {
        tripleFireRatePowerUpActive = true;
        WeaponSystem weaponSystem = GetComponent<WeaponSystem>();

        weaponSystem.fireRate /= tripleFireRatePowerUp.fireRateMultiplier;

        yield return new WaitForSeconds(tripleFireRatePowerUp.duration);

        weaponSystem.fireRate *= tripleFireRatePowerUp.fireRateMultiplier;
        tripleFireRatePowerUpActive = false;
    }
}
