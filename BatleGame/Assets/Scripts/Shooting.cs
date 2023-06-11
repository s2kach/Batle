using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    private Rigidbody2D rb;
    public GameObject bulletPref;
    public float bulletForce = 10f; //сила пули
    public float recoil = 10000f;
    public float recoilDuration = 0.1f;
    private float recoilTime = 0f;

    public float fireRate = 4f; // допустимая частота выстрелов в герцах
    private float ReadyForShot; // следующий выстрел возможен во время которое хранится в этой переменной

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > ReadyForShot) { // Добавил условие можно ли уже стрелять
                ReadyForShot = Time.time + 1 / fireRate; // когда будет разрешён следующий выстрел
                Shoot();
                recoilTime = recoilDuration;
            }
            
        }
        if (recoilTime > 0f)
        {
            recoilTime -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPref, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Player.Recoil();

        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
