using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Chevalier;

public enum BattleState { Start, Player1Turn, Player2Turn, FinManche, WinPlayer1, WinPlayer2 }

public class RoundSystem : MonoBehaviour
{

    [SerializeField] GameObject _chevalier;
    public BattleState state;

    [SerializeField] Transform _spawn1;
    [SerializeField] Transform _spawn2;

    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    public Image Timer;
    [SerializeField] Text dialogueText;

    public BattleHud playerHud;

    Unit playerUnit1;

    void Start()
    {
        state= BattleState.Start;
        SetupBattle();
    }

    private void SetupBattle()
    {
        GameObject player1 = Instantiate(_chevalier, _spawn1);
        playerUnit1 = player1.GetComponent<Unit>();  
        
        dialogueText.text = player1.name;

        
        Instantiate(_chevalier, _spawn2);
    }

    void UpdateProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        Timer.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), Timer.transform.localScale.y, Timer.transform.localScale.z);
        if (cur_cooldown >= max_cooldown)
        {
            state = BattleState.Player1Turn;
        }
    }
}
