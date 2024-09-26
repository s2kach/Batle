using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    private Vector2 dir;
    private Vector2 mouse;
    private static Rigidbody2D rb;
    public Camera cum;
    public float chargeStamina = 1f;

    public Image HeathBar;
    public Image StaminaBar1;
    public Image StaminaBar2;
    public Image ManaBar;
    public static float heath = 100f; // значение хп в процентах
    public static float stamina = 100f;
    public Vector3 Spawn;
    public static Vector3 SpawnPoint;
    static bool isDead = false;
    private static Transform transform;
    public int powerJerk = 5000;
    public int powerRecoil = 5000;
    public static int powerRecoilstat;
    public Transform directionPoint;
    public static Transform directionPointstat;

    
    public static float Mana = 100f;

    private bool lockJerk = false;

    private bool shifting = false;
    private static bool hitting = false;
    public float LockTime = 4f;

    float ReadyToRestoreMana;
    void Start()
    {
        StaminaBar1.enabled = false;
        StaminaBar2.enabled = false;
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        SpawnPoint = Spawn;
        powerRecoilstat = powerRecoil;
        cum.orthographicSize = 23;
    }

    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        mouse = cum.ScreenToWorldPoint(Input.mousePosition);

        //ManaBar.fillAmount = Mana / 100f;
        //HeathBar.fillAmount = heath / 100f; // Отображение текущего хп на экране (измеряется в долях единицы)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shifting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shifting = false;
            cum.orthographicSize = 23;
        }
        shifts(shifting);



        if (Input.GetKeyDown(KeyCode.Space) && !lockJerk)
        {
            lockJerk = true;
            Invoke("LockJerk", LockTime);
            ManaBar.fillAmount = 0;
            jerk();
        }

        if (ManaBar.fillAmount < 1 && Time.time >= ReadyToRestoreMana)
        {
            ReadyToRestoreMana = Time.time + LockTime / 1000f;
            ManaBar.fillAmount += 0.001f;
        }
        

        if (hitting)
        {
            if (HeathBar.fillAmount * 100 > heath)
            {
                HeathBar.fillAmount -= 0.001f;
            }
            else
            {
                hitting = false;
            }
        }

        if (isDead)
        {
            Respawn();
        }
        directionPointstat = directionPoint;

    }

    void FixedUpdate()
    {
        rb.transform.position = Vector2.MoveTowards(rb.position, rb.position + dir, speed * Time.fixedDeltaTime);
        Vector2 look = mouse - rb.position;
        float ang = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
        rb.rotation = ang;
    }

    public static void Damage(float d)
    {
        hitting = true;
        heath -= d;
        if (heath <= 0f) { isDead = true; }
    }

    public void Respawn()
    {
        transform.position = SpawnPoint;
        heath = 100f;
        HeathBar.fillAmount = 1;
        isDead = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            Respawn();
        }
    }

    void shifts(bool s)
    {
        if (s)
        {
            if (dir.x == 0 && dir.y == 0 && stamina != 0)
            {
                cum.orthographicSize = 30;
            }
            else
            {
                cum.orthographicSize = 23;
            }
            if (stamina > 0)
            {
                StaminaBar1.enabled = true;
                StaminaBar2.enabled = true;
                speed = 30f;
                stamina -= chargeStamina;
            }
            else
            {
                stamina = 0;
                speed = 15f;
            }
            StaminaBar1.fillAmount = stamina / 100f;
            StaminaBar2.fillAmount = stamina / 100f;
        }
        else
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

    void jerk()
    {
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(directionPoint.up * powerJerk);
        Invoke("stop", 0.05f);
    }

    public void stop()
    {
        rb.velocity = new Vector2(0, 0);
    }

    public static void Stop()
    {
        rb.velocity = new Vector2(0, 0);
    }

    void LockJerk()
    {
        lockJerk = false;
    }

    public static void Recoil()
    {
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(-directionPointstat.up * powerRecoilstat);
    }
}
