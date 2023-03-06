using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public TurnState currentState;
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    public Image Timer;

    private void Awake()
    {
        Instance = this;
    }

    public enum TurnState
    {
        startCombat,
        processing,
        waiting,
        selecting,
        action,
        dead,
        next
    }
    
    
    //void Start()
    //{
    //    currentState = TurnState.processing;
    //}

    public void switchBetweenTurnStates(TurnState turnState)
    {
        currentState = turnState;
        switch (currentState)
        {
            case (TurnState.startCombat):
                BaseHero hero = UnitManager.Instance.baseHeroes[0];
                UnitManager.Instance.SetSelectedHero(hero);
                break;
            case (TurnState.processing):
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
            default:break;
        }
    }

    //void Update()
    //{
    //    cur_cooldown = cur_cooldown + Time.deltaTime;
    //    float calc_cooldown = cur_cooldown / max_cooldown;
    //    Timer.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), Timer.transform.localScale.y, Timer.transform.localScale.z);
    //    if (cur_cooldown >= max_cooldown)
    //    {
    //        currentState = TurnState.next;
    //    }
    //}
}
