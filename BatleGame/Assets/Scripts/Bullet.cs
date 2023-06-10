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
        Dest();
        damage(25, collision);


        if (collision.gameObject.CompareTag("Enemy"))
        {
            HpEnemy.heath -= 20f;
            if (HpEnemy.heath <= 0f) {Destroy(collision.gameObject); }
        }
    }

    void Dest()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }

   
    void damage(int dam, Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.heath -= dam;

            if (Player.heath <= 0f) { collision.transform.position = Player.SpawnPoint; Player.heath = 100f; }
        }
    }

}
