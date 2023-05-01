using TMPro;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    private int lives = 3;

    private void Start()
    {
        UpdateLivesText();
    }

    public void PlayerHit()
    {
        lives--;
        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        livesText.text = lives.ToString();
    }
}
