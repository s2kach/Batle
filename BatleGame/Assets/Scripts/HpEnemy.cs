using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpEnemy : MonoBehaviour
{

    public Image HeathBar;
    public static float heath = 100f; // значение хп в процентах

    // Update is called once per frame
    void Update()
    {
        HeathBar.fillAmount = heath / 100f;
    }
}
