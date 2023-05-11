using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour
{
    [SerializeField] private GameObject achievementEntryPrefab;
    private string achievementsFilePath;
    Achievement achievement;

    void Start()
    {
        AchievementManager.Instance.LoadAchievements();
        achievement = AchievementManager.Instance.achievementsData;

        if (achievement != null)
        {
            foreach (AchievementEntry entry in achievement.achievements)
            {
                CreateAchievementEntry(entry);
            }
        }
        else
        {
            Debug.Log("Achievement data not loaded");
        }
    }



    void CreateAchievementEntry(AchievementEntry achievement)
    {
        GameObject achievementEntry = (GameObject)Instantiate(achievementEntryPrefab);
        achievementEntry.transform.SetParent(this.transform);
        achievementEntry.transform.localScale = new Vector3(1, 1, 1);
        achievementEntry.transform.Find("Content/AchievementName").GetComponent<TextMeshProUGUI>().text = achievement.name;
        achievementEntry.transform.Find("Content/AchievementDescription").GetComponent<TextMeshProUGUI>().text = achievement.description;
        achievementEntry.transform.Find("IconContainer/IconUnlocked").gameObject.SetActive(achievement.unlocked);
        achievementEntry.transform.Find("IconContainer/IconLocked").gameObject.SetActive(!achievement.unlocked);


        if (achievement.unlocked)
        {
            achievementEntry.transform.Find("Content/AchievementName").GetComponent<TextMeshProUGUI>().color = Color.blue;
            achievementEntry.transform.Find("Content/AchievementDescription").GetComponent<TextMeshProUGUI>().color = Color.blue;
        }
        else
        {
            achievementEntry.transform.Find("Content/AchievementName").GetComponent<TextMeshProUGUI>().color = Color.red;
            achievementEntry.transform.Find("Content/AchievementDescription").GetComponent<TextMeshProUGUI>().color = Color.red;
        }
    }

}
