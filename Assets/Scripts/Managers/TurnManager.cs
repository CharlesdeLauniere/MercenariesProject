using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public TurnState currentState;
    //private float cur_cooldown = 0f;
    // private float max_cooldown = 5f;
    public Image Timer;
    private int Actions;

    private void Awake()
    {
        Instance = this;
    }

    public void SwitchBetweenTurnStates(TurnState turnState)
    {
        currentState = turnState;
        switch (currentState)
        {
            case (TurnState.startCombat):
                Actions = 2;
                BaseHero hero = UnitManager.Instance.baseHeroes[0];
                UnitManager.Instance.SetSelectedHero(hero);
                this.SwitchBetweenTurnStates(TurnState.selectingAttack);
                break;
            case (TurnState.chosingTarget):
                if (Actions > -1) MenuManager.Instance.ShowAbilities(null);
                break;
            case (TurnState.movement) :
                MenuManager.Instance.ShowAbilities(null);
              
                
                break;
            case (TurnState.usingBaseAttack):
                
                UnitManager.Instance.SelectedHero.BaseAttack(UnitManager.Instance.TargetedHero);
                UnitManager.Instance.SetTargetedHero(null);
                MenuManager.Instance.ShowTargetedHero(null);
  
                this.SwitchBetweenTurnStates(TurnState.selectingAttack);
                break;
            case (TurnState.selectingAttack):
                Actions--;
                UnitManager.Instance.IsWinner();
                if (Actions > -1) MenuManager.Instance.ShowAbilities(UnitManager.Instance.SelectedHero);
                else this.SwitchBetweenTurnStates(TurnState.next);
                break;
            case (TurnState.next):
                UnitManager.Instance.IsWinner();
                UnitManager.Instance.NextHeroTurn();
                break;
            case (TurnState.end):
                GameManager.Instance.ChangeState(GameState.GameEnd);
                break;
            default: break;
        }
    }

    public void BasicAttackButtonPress()
    {
        TurnManager.Instance.SwitchBetweenTurnStates(TurnState.chosingTarget);
    }
    public void MovementButtonPress()
    {
        TurnManager.Instance.SwitchBetweenTurnStates(TurnState.movement);
    }
    public enum TurnState
    {
        startCombat,
        chosingTarget,
        selectingAttack,
        movement,
        usingBaseAttack,
        usingAbility1,
        usingAbility2,
        next,
        end
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
