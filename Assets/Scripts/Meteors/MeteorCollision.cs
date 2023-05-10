using UnityEngine;
using System.Collections;


public class MeteorCollision : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject[] smallMeteorPrefabs;
    private LivesCounter livesCounter;
    public AudioClip meteorExplosion;
    private ICommand playMeteorSoundCommand;
    private GameSession gameSession;
    public GameObject playerShip;
    private ScoreCount scoreCount;
    public AchievementManager achievementManager;

    private void Start()
    {
        livesCounter = FindObjectOfType<LivesCounter>();
        scoreCount = FindObjectOfType<ScoreCount>();
        playMeteorSoundCommand = new PlaySoundCommand(livesCounter, meteorExplosion);
        gameSession = FindObjectOfType<GameSession>();
        playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
        achievementManager = FindObjectOfType<AchievementManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && playerShip.GetComponent<TimeRewind>().isRewinding == false)
        {
            other.GetComponent<SpriteRenderer>().enabled = false;
            other.GetComponent<Collider2D>().enabled = false;

            // Wait for 3.25 seconds and then destroy the meteor
            Destroy(other.gameObject, 3.25f);

            // Spawn an explosion at the meteor's position
            GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Get the bullet impact direction
            Vector2 bulletImpactDirection = (other.transform.position - transform.position).normalized;

            // Get the meteor's speed at the time of collision
            float meteorSpeed = GetComponent<Rigidbody2D>().velocity.magnitude;
            // Spawn smaller meteors
            ICommand spawnSmallerMeteorsCommand = new SpawnSmallerMeteorsCommand(
                smallMeteorPrefabs, transform.position, meteorSpeed / 2, bulletImpactDirection, transform.localScale.x);
            spawnSmallerMeteorsCommand.Execute();

            playMeteorSoundCommand.Execute();

            // Disable the meteor's renderer and collider
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            // Wait for 3.25 seconds and then destroy the meteor
            Destroy(gameObject, 3.25f);

            Destroy(newExplosion, 0.8f);
            if (gameSession != null)
            {
                gameSession.IncrementMeteorsDestroyed();
            }
            if (scoreCount != null)
            {
                scoreCount.IncrementScore(10);
                gameSession.IncrementScore(10);
            }

            (string id, int goal)[] consecutiveMeteorAchievements = {
                ("meteorCrusher", 25),
                ("meteorDodger", 50),
                ("meteorEvader", 100)
            };

            foreach (var achievementData in consecutiveMeteorAchievements)
            {
                if (gameSession.ConsecutiveMeteorsDestroyed >= achievementData.goal)
                {
                    AchievementEntry achievement = achievementManager.GetAchievement(achievementData.id);
                    // Check if the achievement is not unlocked
                    if (!achievement.unlocked)
                    {
                        achievementManager.IncrementProgress(achievementData.id);
                    }
                }
            }

            string[] achievementIds = { "meteor1", "meteor2", "meteor3", "meteor4" };
            foreach (string achievementId in achievementIds)
            {
                AchievementEntry achievement = achievementManager.GetAchievement(achievementId);

                // Check if the achievement is not unlocked
                if (!achievement.unlocked)
                {
                    achievementManager.IncrementProgress(achievementId);
                }
            }
        }

        if (other.CompareTag("PlayerShip") && playerShip.GetComponent<TimeRewind>().isRewinding == false)
        {
            if (livesCounter != null)
            {
                livesCounter.PlayerHit();
                gameSession.SetConsecutiveMeteorsDestroyed(0);
            }

            // Spawn an explosion at the meteor's position
            GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Disable the meteor's renderer and collider
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // Wait for 3.25 seconds and then destroy the meteor
            Destroy(gameObject, 3.25f);
        }
    }
}
