using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    private Vector2 dir;
    private Vector2 mouse;
    private Rigidbody2D rb;
    public Camera cum;

    public Image HeathBar;
    public static float heath = 100f; // значение хп в процентах
    public Vector3 Spawn;
    public static Vector3 SpawnPoint;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpawnPoint = Spawn;
    }

    
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        mouse = cum.ScreenToWorldPoint(Input.mousePosition);
        
        HeathBar.fillAmount = heath / 100f; // Отображение текущего хп на экране (измеряется в долях единицы)
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        Vector2 look = mouse - rb.position;
        float ang = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
        rb.rotation = ang;
    }
}
