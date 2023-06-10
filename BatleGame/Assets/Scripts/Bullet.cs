using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    public GameObject hitEffect;
    private void Start()
    {
        Invoke("Dest", 3f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.heath -= 25f;
            if (Player.heath <= 0f) { collision.transform.position = Player.SpawnPoint; Player.heath = 100f; }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            HpEnemy.heath -= 1f;
            if (HpEnemy.heath <= 0f) {Destroy(collision.gameObject); }
        }
    }

    void Dest()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }


}
