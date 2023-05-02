using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform firePoint;
    public float fireRate = 0.25f;
    private float nextFireTime = 0f;
    public bool canShoot = true;

    public AudioClip shootSound;
    private ICommand playShootSoundCommand;
    private bool isPlayingShootSound;

    void Start()
    {
        playShootSoundCommand = new PlaySoundCommand(this, shootSound);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && canShoot)
        {
            if (Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                Shoot();
                playShootSoundCommand.Execute();

            }
        }
        else
        {
            StopAllCoroutines();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
    }
}
