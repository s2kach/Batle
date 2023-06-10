using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpEnemy : MonoBehaviour
{

    public Image HeathBar;
    public static float heath = 100f; // значение хп в процентах
    private static bool hitting = false;
    private static Transform transform;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }
    public static void Damage(float d)
    {
        hitting = true;
        heath -= d;
        if (heath <= 0f) { Destroy(transform.gameObject); }
    }

    void Update()
    {
        //HeathBar.fillAmount = heath / 100f;

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
    }
}
