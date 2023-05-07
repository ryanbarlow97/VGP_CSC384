using UnityEngine;
using TMPro;
using System.Collections;
using System.IO;

public class LeaderboardDisplay : MonoBehaviour
{
    public GameObject playerScoreEntryPrefab;
    private string leaderboardFilename;

    Leaderboard leaderboard;

    // Use this for initialization
    void Start()
    {
        leaderboardFilename = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        leaderboard = Leaderboard.Load(leaderboardFilename);

        GameObject go1 = (GameObject)Instantiate(playerScoreEntryPrefab);
        go1.transform.SetParent(this.transform);
        go1.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = "#";
        go1.transform.Find("Username").GetComponent<TextMeshProUGUI>().text = "Username";
        go1.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score";


        int rank = 1;
        foreach (LeaderboardEntry entry in leaderboard.leaderboardEntries)
        {
            GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
            go.transform.SetParent(this.transform);
            go.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rank.ToString();
            go.transform.Find("Username").GetComponent<TextMeshProUGUI>().text = entry.playerName;
            go.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = entry.score.ToString();

            rank++;
        }
    }
}
