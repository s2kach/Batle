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
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Damage(25);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            HpEnemy.Damage(20);
        }
    }
    void Dest()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }

}
