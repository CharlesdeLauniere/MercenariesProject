using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using static TurnManager;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _CubePlayer;

    public static GameManager Instance;
    public GameState GameState;
    public List<string> _spawnName = new List<string> {"Knight", "Archer", "Mage" };
    private void Awake()
    { 
            Instance = this;
    }
    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }
    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.SelectHeroes:
                break;
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeroes(_spawnName);
                break;
            case GameState.TurnBasedCombat:

                TurnManager.Instance.SwitchBetweenTurnStates(TurnState.startCombat);
            break;
            case GameState.GameEnd:
                //MenuManager.Instance.ShowVictoryScreen(Faction winningTeam);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }

    }

}
public enum GameState
{
    SelectHeroes = 0,
    GenerateGrid = 1,
    SpawnHeroes = 2,
    TurnBasedCombat = 3,
    GameEnd = 4

}