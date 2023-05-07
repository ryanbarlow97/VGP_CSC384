using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject playerShip;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private GameObject explosionPrefab;
    public PauseMenu pauseMenu;
    public AudioClip healthLoss;
    public AudioClip gameOver;
    private ICommand playHeathLossSoundCommand;
    private ICommand playGameOverSoundCommand;
    private int saveSlotNumber;
    private float invincibilityDuration = 2f;
    private bool isInvincible = false;  
    private int lives;
    private GameSession gameSession;
    private SaveData saveData;
    private TransitionEffect transitionEffect;

    private void Start()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        saveSlotNumber = gameSession.SaveSlotNumber;
        playHeathLossSoundCommand = new PlaySoundCommand(this, healthLoss);
        playGameOverSoundCommand = new PlaySoundCommand(this, gameOver);

        saveData = SaveManager.Load(saveSlotNumber);

        transitionEffect = FindObjectOfType<TransitionEffect>();

        lives = saveData.playerHearts;
        
        UpdateLivesText();
        StartCoroutine(ReloadPlayer());
    }

    public void PlayerHit()
    {
        if (isInvincible)
        {
            return;
        }

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
            GameOver();
        }
    }

    private void UpdateLivesText()
    {
        livesText.text = lives.ToString();
    }

    private IEnumerator RespawnPlayer()
    {
        isInvincible = true;

        // Disable the BoxCollider2D
        BoxCollider2D playerCollider = playerShip.GetComponent<BoxCollider2D>();
        playerCollider.enabled = false;

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

        StartCoroutine(InvincibilityTimer());

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
    private IEnumerator InvincibilityTimer()
    {
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(invincibilityDuration);

        // Enable the BoxCollider2D after the invincibility period
        BoxCollider2D playerCollider = playerShip.GetComponent<BoxCollider2D>();
        playerCollider.enabled = true;
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        isInvincible = false;
    }

    private void GameOver()
    {
        transitionEffect.LoadScene("EndGame");
    }
}
