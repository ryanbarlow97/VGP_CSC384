using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class Leaderboard
{
    public List<LeaderboardEntry> leaderboardEntries;

    public Leaderboard()
    {
        leaderboardEntries = new List<LeaderboardEntry>();
    }

    public void AddEntry(LeaderboardEntry entry)
    {
        leaderboardEntries.Add(entry);
        leaderboardEntries = leaderboardEntries.OrderByDescending(e => e.score).Take(5).ToList();
    }

    public void Save(string filename)
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(filename, json);
    }

    public static Leaderboard Load(string filename)
    {
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            return JsonUtility.FromJson<Leaderboard>(json);
        }

        return new Leaderboard();
    }
}
