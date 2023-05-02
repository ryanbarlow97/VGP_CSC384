using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform firePoint;
    public float fireRate = 0.25f; // Time in seconds between shots
    private float nextFireTime = 0f; // Time when the next shot is allowed
    public bool canShoot = true; // Add a public boolean variable to control shooting


    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime && canShoot)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }   
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
    }
}
