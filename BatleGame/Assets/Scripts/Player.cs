using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Vector2 dir;
    private Vector2 mouse;
    private Rigidbody2D rb;
    public Camera cum;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        mouse = cum.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        Vector2 look = mouse - rb.position;
        float ang = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
        rb.rotation = ang;
    }
}
