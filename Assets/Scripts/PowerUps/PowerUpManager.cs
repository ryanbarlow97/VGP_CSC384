using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private bool speedPowerUpActive = false;
    private bool tripleFireRatePowerUpActive = false;
    private PowerUpEventManager eventManager;
    [SerializeField] private RemainingTimeUI timerBarController;
    [SerializeField] private Image speedPowerUpImage;
    [SerializeField] private Image tripleFireRatePowerUpImage;
    [SerializeField] private Image RemainingTinmeBar;
    private Queue<IEnumerator> powerUpQueue = new Queue<IEnumerator>();
    private GameSession gameSession;

    private void Update()
    {
        if (powerUpQueue.Count > 0 && !IsAnyPowerUpActive())
        {
            StartCoroutine(powerUpQueue.Dequeue());
        }
    }

    private void Start()
    {
        eventManager = FindObjectOfType<PowerUpEventManager>();
        eventManager.StartListening("SpeedPowerUp", ActivateSpeedPowerUp);
        eventManager.StartListening("TripleFireRatePowerUp", ActivateTripleFireRatePowerUp);
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnDestroy()
    {
        eventManager.StopListening("SpeedPowerUp", ActivateSpeedPowerUp);
        eventManager.StopListening("TripleFireRatePowerUp", ActivateTripleFireRatePowerUp);
    }

    private void ActivateSpeedPowerUp(object powerUpObject)
    {
        if (powerUpObject is SpeedPowerUp powerUp)
        {
            powerUpQueue.Enqueue(ApplyTemporarySpeedPowerUp(powerUp));
        }
    }

    private IEnumerator ApplyTemporarySpeedPowerUp(SpeedPowerUp speedPowerUp)
    {
        speedPowerUpActive = true;
        gameSession.IncrementPowerupsCollected();
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();

        playerMovement.acceleration *= speedPowerUp.speedMultiplier;

        timerBarController.SetMaxTime(speedPowerUp.duration);
        float timeElapsed = 0f;
        speedPowerUpImage.enabled = true;
        RemainingTinmeBar.enabled = true;


        while (timeElapsed < speedPowerUp.duration)
        {
            timeElapsed += Time.deltaTime;
            timerBarController.SetCurrentTime(speedPowerUp.duration - timeElapsed);
            yield return null;
        }

        playerMovement.acceleration /= speedPowerUp.speedMultiplier;
        speedPowerUpActive = false;
        speedPowerUpImage.enabled = false;
        RemainingTinmeBar.enabled = false;
    }


    private void ActivateTripleFireRatePowerUp(object powerUpObject)
    {
        if (powerUpObject is TripleFireRatePowerUp powerUp)
        {
            powerUpQueue.Enqueue(ApplyTemporaryTripleFireRatePowerUp(powerUp));
        }
    }


    private IEnumerator ApplyTemporaryTripleFireRatePowerUp(TripleFireRatePowerUp tripleFireRatePowerUp)
    {
        tripleFireRatePowerUpActive = true;
        gameSession.IncrementPowerupsCollected();
        WeaponSystem weaponSystem = GetComponent<WeaponSystem>();

        weaponSystem.fireRate /= tripleFireRatePowerUp.fireRateMultiplier;

        timerBarController.SetMaxTime(tripleFireRatePowerUp.duration);
        float timeElapsed = 0f;
        tripleFireRatePowerUpImage.enabled = true;
        RemainingTinmeBar.enabled = true;

        while (timeElapsed < tripleFireRatePowerUp.duration)
        {
            timeElapsed += Time.deltaTime;
            timerBarController.SetCurrentTime(tripleFireRatePowerUp.duration - timeElapsed);
            yield return null;
        }

        weaponSystem.fireRate *= tripleFireRatePowerUp.fireRateMultiplier;
        tripleFireRatePowerUpActive = false;
        tripleFireRatePowerUpImage.enabled = false;
        RemainingTinmeBar.enabled = false;

    }

    public bool IsAnyPowerUpActive()
    {
        return speedPowerUpActive || tripleFireRatePowerUpActive;
    }
}