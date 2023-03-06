using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chevalier : MonoBehaviour
{
    public enum TurnStatex
    {
        processing,
        waiting,
        selecting,
        action,
        dead,
        next
    }
    public TurnStatex currentStatex;
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    public Image Timer;
    void Start()
    {
        currentStatex = TurnStatex.processing;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStatex)
        {
            case (TurnStatex.processing):
                UpdateProgressBar();

                break;

            case (TurnStatex.action):


                break;

            case (TurnStatex.selecting):


                break;

            case (TurnStatex.waiting):


                break;

            case (TurnStatex.dead):


                break;

            case (TurnStatex.next):


                break;
        }
    }

    void UpdateProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        Timer.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), Timer.transform.localScale.y, Timer.transform.localScale.z);
        if (cur_cooldown >= max_cooldown)
        {
            currentStatex = TurnStatex.next;
        }
    }
}
