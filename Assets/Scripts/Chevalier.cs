using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chevalier : MonoBehaviour
{
    

    public enum TurnState
    {
        processing,
        waiting,
        selecting,
        action,
        dead,
        next
    }
    public TurnState currentState;
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    public Image Timer;
    void Start()
    {
        currentState = TurnState.processing;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (TurnState.processing):
                UpdateProgressBar();

                break;

            case (TurnState.action):


                break;

            case (TurnState.selecting):


                break;

            case (TurnState.waiting):


                break;

            case (TurnState.dead):


                break;

            case (TurnState.next):


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
            currentState = TurnState.next;
        }
    }
}
