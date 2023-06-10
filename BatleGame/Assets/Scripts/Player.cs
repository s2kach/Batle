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
    public float chargeStamina = 1f;

    public Image HeathBar;
    public Image StaminaBar1;
    public Image StaminaBar2;
    public static float heath = 100f; // значение хп в процентах
    public static float stamina = 100f;
    public Vector3 Spawn;
    public static Vector3 SpawnPoint;

    bool shifting = false; // flag
    void Start()
    {
        StaminaBar1.enabled = false;
        StaminaBar2.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        SpawnPoint = Spawn;
    }

    
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        mouse = cum.ScreenToWorldPoint(Input.mousePosition);

        HeathBar.fillAmount = heath / 100f; // Отображение текущего хп на экране (измеряется в долях единицы)

        if (Input.GetKeyDown(KeyCode.LeftShift) && stamina>0f)
        {
            speed = 30f;
            StaminaBar1.enabled = true;
            StaminaBar2.enabled = true;
            stamina -= chargeStamina;
            StaminaBar1.fillAmount = stamina / 100f;
            StaminaBar2.fillAmount = stamina / 100f;

        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 15f;
            if (stamina < 100)
            {
                StaminaBar1.enabled = true;
                StaminaBar2.enabled = true;
                stamina += chargeStamina * 2f;
                StaminaBar1.fillAmount = stamina / 100f;
                StaminaBar2.fillAmount = stamina / 100f;
            }
            else
            {
                stamina = 100f;
                StaminaBar1.enabled = false;
                StaminaBar2.enabled = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 15f;
            if (stamina < 100)
            {
                StaminaBar1.enabled = true;
                StaminaBar2.enabled = true;
                stamina += chargeStamina * 2;
                StaminaBar1.fillAmount = stamina / 100f;
                StaminaBar2.fillAmount = stamina / 100f;
            }
            else
            {
                stamina = 100f;
                StaminaBar1.enabled = false;
                StaminaBar2.enabled = false;
            }
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        Vector2 look = mouse - rb.position;
        float ang = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
        rb.rotation = ang;
    }
}
