using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimeRewinder : MonoBehaviour
{
    public bool isRewinding;
    private List<PointInTime> meteorStates = new List<PointInTime>();
    private List<PointInTime> smallMeteorStates = new List<PointInTime>();
    private List<PointInTime> powerUpStates = new List<PointInTime>();
    private List<PointInTime> bulletStates = new List<PointInTime>();
    private List<PointInTime> playerShipStates = new List<PointInTime>();

    private GameObject[] meteors;
    private GameObject[] smallMeteors;
    private GameObject[] powerUps;
    private GameObject[] bullets;
    private GameObject playerShip;

    public float maxRewindTime = 3f;
    public float rewindElapsedTime = 0f;


    public void Record()
    {
        SaveState();
    }

    public void Rewind()
    {
        


        if (meteorStates.Any())
        {
            ApplyState(meteorStates, meteors);
            meteorStates.RemoveAt(meteorStates.Count - 1);
        }
        if (smallMeteorStates.Any())
        {
            ApplyState(smallMeteorStates, smallMeteors);
            smallMeteorStates.RemoveAt(smallMeteorStates.Count - 1);
        }
        if (powerUpStates.Any())
        {
            ApplyState(powerUpStates, powerUps);
            powerUpStates.RemoveAt(powerUpStates.Count - 1);
        }
        if (bulletStates.Any())
        {
            ApplyState(bulletStates, bullets);
            bulletStates.RemoveAt(bulletStates.Count - 1);
        }           
        if (playerShipStates.Any())
        {
            ApplyState(playerShipStates, new GameObject[] { playerShip });
            playerShipStates.RemoveAt(playerShipStates.Count - 1);
        }        
    }
    
    public bool IsAnyRewind()
    {
        return meteorStates.Any() || smallMeteorStates.Any() || powerUpStates.Any() || bulletStates.Any() || playerShipStates.Any();
    }

    private void ApplyState(List<PointInTime> pointsInTime, GameObject[] gameObjects)
    {
        if (pointsInTime.Count == 0)
            return;

        PointInTime pointInTime = pointsInTime.Last();
        if (pointInTime == null)
            return;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i] != null && i < pointInTime.states.Count)
            {
                GameObjectState gameObjectState = pointInTime.states[i];
                gameObjects[i].transform.position = gameObjectState.position;
                gameObjects[i].transform.rotation = gameObjectState.rotation;
                gameObjects[i].transform.localScale = gameObjectState.scale;               

                Rigidbody2D rb = gameObjects[i].GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = gameObjectState.velocity;
                    rb.angularVelocity = gameObjectState.angularVelocity;
                }
            }
            else 
            {
                if (gameObjects[i] != null)
                {
                    Destroy(gameObjects[i]);
                }
            }
        }
    }



    private void SaveState()
    {
        meteors = GameObject.FindGameObjectsWithTag("MeteorLarge");

        smallMeteors = GameObject.FindGameObjectsWithTag("MeteorSmallBL")
            .Concat(GameObject.FindGameObjectsWithTag("MeteorSmallBR"))
            .Concat(GameObject.FindGameObjectsWithTag("MeteorSmallTL"))
            .Concat(GameObject.FindGameObjectsWithTag("MeteorSmallTR"))
            .ToArray();

        powerUps = GameObject.FindGameObjectsWithTag("SpeedPowerUp")
            .Concat(GameObject.FindGameObjectsWithTag("TripleFireRatePowerUp"))
            .ToArray();

        bullets = GameObject.FindGameObjectsWithTag("Bullet");

        playerShip = GameObject.FindWithTag("PlayerShip");



        if (meteors != null)
        {
            meteorStates.Add(GetState(meteors));
        }
        if (smallMeteors != null)
        {
            smallMeteorStates.Add(GetState(smallMeteors));
        }
        if (powerUps != null)
        {
            powerUpStates.Add(GetState(powerUps));
        }
        if (bullets != null)
        {
            bulletStates.Add(GetState(bullets));
        }
        if (playerShip != null)
        {
            playerShipStates.Add(GetState(new GameObject[] { playerShip }));
        }
    }


    private PointInTime GetState(GameObject[] gameObjects)
    {
        List<GameObjectState> gameObjectStates = new List<GameObjectState>();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject != null)
            {
                Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    gameObjectStates.Add(new GameObjectState(gameObject.transform.position, gameObject.transform.rotation, rb.velocity, rb.angularVelocity, gameObject.transform.localScale, gameObject.tag));
                }
                else
                {
                    gameObjectStates.Add(new GameObjectState(gameObject.transform.position, gameObject.transform.rotation, Vector3.zero, 0f, gameObject.transform.localScale, gameObject.tag));
                }
            }
        }
        return new PointInTime(gameObjectStates);
    }


}

public class GameObjectState
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector2 velocity;
    public float angularVelocity;
    public Vector3 scale;
    public string tag;

    public GameObjectState(Vector3 _position, Quaternion _rotation, Vector2 _velocity, float _angularVelocity, Vector3 _scale, string _tag)
    {
        position = _position;
        rotation = _rotation;
        velocity = _velocity;
        angularVelocity = _angularVelocity;
        scale = _scale;
        tag = _tag;
    }
}

public class PointInTime
{
    public List<GameObjectState> states;

    public PointInTime(List<GameObjectState> _states)
    {
        states = _states;
    }
}
