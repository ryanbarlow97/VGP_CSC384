using System.Collections;
using TMPro;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject playerShip;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private int startingLives = 3;
    [SerializeField] private GameObject explosionPrefab;
    public PauseMenu pauseMenu;
    public AudioClip healthLoss;
    public AudioClip gameOver;
    private ICommand playHeathLossSoundCommand;
    private ICommand playGameOverSoundCommand;
    private int saveSlotNumber;
    private int lives;

    private void Start()
    {
        MainGameLoader mainGameLoader = FindObjectOfType<MainGameLoader>();
        if (mainGameLoader != null)
        {
            saveSlotNumber = mainGameLoader.saveSlotNumber;
        }
        playHeathLossSoundCommand = new PlaySoundCommand(this, healthLoss);
        playGameOverSoundCommand = new PlaySoundCommand(this, gameOver);

        SaveData saveData = SaveManager.Load(saveSlotNumber);
        if (saveData != null)
        {
            lives = saveData.playerHearts;
        }
        else
        {
            lives = startingLives;
        }
        UpdateLivesText();
        StartCoroutine(ReloadPlayer());
    }

    public void PlayerHit()
    {
        if (lives > 1)
        {
            lives--;
            UpdateLivesText();
            playHeathLossSoundCommand.Execute();
            StartCoroutine(RespawnPlayer());
        }
        else
        {
            lives--;
            UpdateLivesText();
            playGameOverSoundCommand.Execute();
        }
    }

    private void UpdateLivesText()
    {
        livesText.text = lives.ToString();
    }

    private IEnumerator RespawnPlayer()
    {
        // Teleport the player back to the middle
        playerShip.transform.position = Vector3.zero;

        // Reset player's velocity
        Rigidbody2D playerRigidbody = playerShip.GetComponent<Rigidbody2D>();
        playerRigidbody.velocity = Vector2.zero;

        // Set player's rotation to point north
        playerShip.transform.rotation = Quaternion.Euler(0, 0, 0);

        // Disallow shooting
        WeaponSystem weaponSystem = playerShip.GetComponent<WeaponSystem>();
        weaponSystem.canShoot = false;

        // Freeze the game for 3 seconds
        Time.timeScale = 0f;

        // Apply flashing color effect
        for (int i = 0; i < 10; i++)
        {
            playerSpriteRenderer.color = flashColor;
            yield return new WaitForSecondsRealtime(flashDuration);
            playerSpriteRenderer.color = Color.white;
            yield return new WaitForSecondsRealtime(flashDuration);
        }

        // Unfreeze the game
        if (!pauseMenu.IsGamePaused)
        {
            Time.timeScale = 1f;
        }

        // Allow shooting again
        weaponSystem.canShoot = true;
    }

    public int GetLives()
    {
        return lives;
    }


    private IEnumerator ReloadPlayer()
    {       
        // Disallow shooting
        WeaponSystem weaponSystem = playerShip.GetComponent<WeaponSystem>();
        weaponSystem.canShoot = false;

        // Freeze the game for 3 seconds
        Time.timeScale = 0f;

        // Apply flashing color effect
        for (int i = 0; i < 10; i++)
        {
            playerSpriteRenderer.color = flashColor;
            yield return new WaitForSecondsRealtime(flashDuration);
            playerSpriteRenderer.color = Color.white;
            yield return new WaitForSecondsRealtime(flashDuration);
        }

        if (!pauseMenu.IsGamePaused)
        {
            Time.timeScale = 1f;
        }

        // Allow shooting again
        weaponSystem.canShoot = true;
    }
}
