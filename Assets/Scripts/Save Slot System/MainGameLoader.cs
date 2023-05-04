using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MainGameLoader : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI playerNameText;
    private int playerScore;
    private int playerHearts;
    public GameObject meteorPrefab;
    public GameObject[] smallMeteorPrefab;
    public GameObject[] powerUpPrefab;

    public int saveSlotNumber = 1;

    private void Start()
    {
        // Load the game data from the specified save slot
        SaveData savedData = SaveManager.Load(saveSlotNumber);

        if (savedData != null)
        {
            // Set the player name
            playerNameText.text = savedData.playerName;
            // Get the current score
            playerScore = savedData.playerScore;
            //Get the remaining hearts
            playerHearts = savedData.playerHearts;

            // Set the player position and rotation
            player.transform.position = savedData.playerPosition.ToVector3();
            player.transform.eulerAngles = savedData.playerRotation.ToVector3();

            // Spawn the meteors
            foreach (MeteorData meteorData in savedData.meteorDataList)
            {
                Vector3 position = meteorData.position.ToVector3();
                Vector3 velocity = meteorData.velocity.ToVector3();
                Vector3 scale = meteorData.scale.ToVector3();

                GameObject meteor = Instantiate(meteorPrefab, position, Quaternion.identity);
                meteor.GetComponent<Rigidbody2D>().velocity = velocity;
                meteor.transform.localScale = scale;
            }

            // Spawn the smaller meteors
            foreach (SmallMeteorData smallMeteorData in savedData.smallMeteorDataList)
            {
                int meteorType = smallMeteorData.meteorType;
                Vector3 position = smallMeteorData.position.ToVector3();
                Vector3 velocity = smallMeteorData.velocity.ToVector3();
                Vector3 scale = smallMeteorData.scale.ToVector3();
                Vector3 rotation = smallMeteorData.rotation.ToVector3();
                float angularVelocity = smallMeteorData.angularVelocity.ToFloat();

                Quaternion rotationQuaternion = Quaternion.Euler(rotation);
                GameObject meteor = Instantiate(smallMeteorPrefab[meteorType], position, rotationQuaternion);
                meteor.GetComponent<Rigidbody2D>().velocity = velocity;
                meteor.GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;
                meteor.transform.localScale = scale;
            }

            // Spawn the powerups
            foreach (PowerUpData powerUpData in savedData.powerUpDataList)
            {
                int powerUpType = powerUpData.powerUpType;
                Vector3 position = powerUpData.position.ToVector3();
                Vector3 velocity = powerUpData.velocity.ToVector3();
                Vector3 scale = powerUpData.scale.ToVector3();

                GameObject powerUp = Instantiate(powerUpPrefab[powerUpType], position, Quaternion.identity);
                powerUp.GetComponent<Rigidbody2D>().velocity = velocity;
                powerUp.transform.localScale = scale;
            }
        }
    }
}
