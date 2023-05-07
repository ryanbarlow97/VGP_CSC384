using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimeRewind : MonoBehaviour
{
    public bool isRewinding = false;
    private float recordTime = 3f;
    public List<PointInTime> pointsInTime;
    public List<PointInTime> pointsInTimeFull;
    private Rigidbody2D rb;
    private float objectLifetime;
    private int instanceID;

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
        pointsInTimeFull = new List<PointInTime>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartRewind();
        }
    }

    void LateUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    private void Rewind()
    {
        if (gameObject.tag == "PlayerShip")
        {
            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            WeaponSystem weaponSystem = GetComponent<WeaponSystem>();
            if (weaponSystem != null)
            {
                weaponSystem.enabled = false;
            }
        }

        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            rb.velocity = pointInTime.velocity;
            rb.angularVelocity = pointInTime.angularVelocity;
            transform.localScale = pointInTime.scale;
            gameObject.tag = pointInTime.tag;
            GetComponent<SpriteRenderer>().enabled = pointInTime.isEnabled;
            GetComponent<Collider2D>().enabled = pointInTime.isEnabled;

            pointsInTimeFull.RemoveAt(0);
            pointsInTime.RemoveAt(0);
        }
        else
        {
            if (gameObject.tag == "PlayerShip")
            {
                PlayerMovement playerMovement = GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.enabled = true;
                }

                WeaponSystem weaponSystem = GetComponent<WeaponSystem>();
                if (weaponSystem != null)
                {
                    weaponSystem.enabled = true;
                }
            }
            if (pointsInTimeFull.Count == 0){
                Destroy(gameObject);
            }
            StopRewind();
        }
    }
    private void Record()
    {
        bool isEnabled = GetComponent<SpriteRenderer>().enabled;
        objectLifetime += Time.fixedDeltaTime;
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, rb.velocity, rb.angularVelocity, transform.localScale, gameObject.tag, Time.time, Time.time + objectLifetime, isEnabled));
        pointsInTimeFull.Insert(0, new PointInTime(transform.position, transform.rotation, rb.velocity, rb.angularVelocity, transform.localScale, gameObject.tag, Time.time, Time.time + objectLifetime, isEnabled));

        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
    }

    public SerializableTimeRewindData GetData()
    {
        SerializableTimeRewindData data = new SerializableTimeRewindData
        {
            instanceID = GetInstanceID(),
        };

        if (pointsInTimeFull.Count > 0)
        {
            data.pointsInTimeFull = new List<PointInTime> { pointsInTimeFull[pointsInTimeFull.Count - 1] };
        }
        else
        {
            data.pointsInTimeFull = new List<PointInTime>();
        }

        return data;
    }


    public void SetData(SerializableTimeRewindData data)
    {
        instanceID = data.instanceID;
        pointsInTime = data.pointsInTimeFull;
    }
}