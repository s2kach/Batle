using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    private Rigidbody2D rb;
    public GameObject bulletPref;
    public float bulletForce = 10f; //���� ����
    // public float recoil = 10000f;
    public float recoilDuration = 0.1f;
    private float recoilTime = 0f;

    public float fireRate = 4f; // ���������� ������� ��������� � ������
    private float ReadyForShot; // ��������� ������� �������� �� ����� ������� �������� � ���� ����������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time >= ReadyForShot) { // ������� ������� ����� �� ��� ��������
                ReadyForShot = Time.time + 1 / fireRate; // ����� ����� �������� ��������� �������
                Shoot();
            }
            
        }
    }

    void stopPlayer()
    {
        Player.Stop();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPref, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Player.Recoil();
        Invoke("stopPlayer", 0.05f);

        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
