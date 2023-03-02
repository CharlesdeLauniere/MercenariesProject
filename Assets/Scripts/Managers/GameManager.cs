using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _CubePlayer;

    public static GameManager Instance;
    public GameState GameState;

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
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeroes();
                break;
            case GameState.BluePlayerTurn:

                break;
            case GameState.RedPlayerTurn:
                break;
            case GameState.GameEnd:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }

    }

}
public enum GameState
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    BluePlayerTurn = 2,
    RedPlayerTurn = 3,
    GameEnd = 4

}