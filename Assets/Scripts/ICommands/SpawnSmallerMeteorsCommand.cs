using UnityEngine;

public class SpawnSmallerMeteorsCommand : ICommand
{
    private GameObject[] smallMeteorPrefabs;
    private Vector3 spawnPosition;
    private float smallMeteorSpeed;
    private Vector2 bulletImpactDirection;
    private float parentScale;


    public SpawnSmallerMeteorsCommand(GameObject[] smallMeteorPrefabs, Vector3 spawnPosition, float smallMeteorSpeed, Vector2 bulletImpactDirection, float parentScale)
    {
        this.smallMeteorPrefabs = smallMeteorPrefabs;
        this.spawnPosition = spawnPosition;
        this.smallMeteorSpeed = smallMeteorSpeed;
        this.bulletImpactDirection = bulletImpactDirection;
        this.parentScale = parentScale;
    }

    public void Execute()
    {
        if (smallMeteorPrefabs != null && smallMeteorPrefabs.Length == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject smallMeteor = GameObject.Instantiate(smallMeteorPrefabs[i], spawnPosition, Quaternion.identity);
                
                // Scale the small meteor to be 1/4 the size of the parent meteor
                smallMeteor.transform.localScale = parentScale * smallMeteor.transform.localScale; 

                MeteorMovement smallMeteorMovement = smallMeteor.GetComponent<MeteorMovement>();

                // Set useInitialDirection to true for small meteors
                smallMeteorMovement.useInitialDirection = true;

                // Calculate the base angle in degrees
                float baseAngle = Mathf.Atan2(bulletImpactDirection.y, bulletImpactDirection.x) * Mathf.Rad2Deg + 180f;

                // Calculate a random angle within the 45-degree range
                float angle = baseAngle - 22.5f + Random.Range(0f, 45f);

                // Convert the angle to a direction vector
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                // Set the direction and speed for the small meteor
                smallMeteorMovement.SetDirection(direction);
                smallMeteorMovement.speed = smallMeteorSpeed;
            }
        }
    }
}
