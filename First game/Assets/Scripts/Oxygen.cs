using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    bool inOxygenBox;
    float timerDecimal, oxygenWidth, oxygenHeight, fullTimer;
    public float timer = 50;
    public GameObject oxygenBar;

    void Start()
    {
        fullTimer = timer;
        oxygenWidth = oxygenBar.GetComponent<RectTransform>().rect.width;
        oxygenHeight = oxygenBar.GetComponent<RectTransform>().rect.height;
    }

    void Update()
    {
        if (inOxygenBox == true)
        {
            if (timer < fullTimer)
            {
                timerDecimal += Time.deltaTime;
                if (Mathf.FloorToInt(timerDecimal) == 1)
                {
                    timerDecimal = 0;
                    timer += 3;
                    //If timer exceeds limits, set to max
                    if (timer > fullTimer)
                    {
                        timer = fullTimer;
                    }
                }
            }
            float newOxygenWidth = oxygenWidth * (timer / fullTimer);
            oxygenBar.GetComponent<RectTransform>().sizeDelta = new Vector2(newOxygenWidth, oxygenHeight);
        }
        else
        {
            if (timer > 0)
            {
                timerDecimal += Time.deltaTime;
                if (Mathf.FloorToInt(timerDecimal) == 1)
                {
                    timerDecimal = 0;
                    timer -= 1;
                }
            }
            float newOxygenWidth = oxygenWidth * (timer / fullTimer);
            oxygenBar.GetComponent<RectTransform>().sizeDelta = new Vector2(newOxygenWidth, oxygenHeight);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Has entered oxygen box
        if (other.name == "C_Player")
        {
            inOxygenBox = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Has left oxygen box
        if (other.name == "C_Player")
        {
            inOxygenBox = false;
        }
    }
}
