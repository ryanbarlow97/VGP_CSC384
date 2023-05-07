using System.Collections;
using UnityEngine;

public class TripleFireRatePowerUp : MonoBehaviour
{
    public float fireRateMultiplier = 3f;
    public float duration = 5f;
    private AchievementManager achievementManager;
    private GameSession gameSession;

    private void Start()
    {
        achievementManager = FindObjectOfType<AchievementManager>();
        gameSession = FindObjectOfType<GameSession>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerShip"))
        {
            // Disable the powerup's renderer and collider
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            // Wait for 3.25 seconds and then destroy the powerup
            Destroy(gameObject, 3.25f);

            string[] achievementIds = { "collector1", "collector2", "collector3" };
            foreach (string achievementId in achievementIds)
            {
                Achievement achievement = achievementManager.GetAchievement(achievementId);

                if (!achievement.unlocked)
                {
                    achievementManager.IncrementProgress(achievementId);
                }
            }

            (string id, int goal)[] powerUpAchievements = {
                ("collectorSingleGame5", 5),
                ("collectorSingleGame10", 10),
            };

            foreach (var achievementData in powerUpAchievements)
            {
                if (gameSession.PowerupsCollected >= achievementData.goal)
                {
                    Achievement achievement = achievementManager.GetAchievement(achievementData.id);
                    if (!achievement.unlocked)
                    {
                        achievementManager.IncrementProgress(achievementData.id);
                    }
                }
            }
            PowerUpEventManager.Instance.TriggerEvent("TripleFireRatePowerUp", this);
        }
    }
}
