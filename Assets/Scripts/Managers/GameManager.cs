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
        {
            Instance = this;
        }
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
            case GameState.Player1Move:
                break;
            case GameState.Player2Move:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }

    }
    //public void Begin()
    //{
    //    GameObject player1 = Instantiate(_CubePlayer, new Vector3(4, 0.2f, 5), Quaternion.identity);
    //    player1.name = "CubePlayer1";
    //    //GameObject player2 = Instantiate(_CubePlayer, new Vector3(7, 0.2f, 2), Quaternion.identity);
    //    //player2.name = "CubePlayer2";



    //    Round(player1);
    //    //Round(player2);

    //}

}
public enum GameState
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    Player1Move = 2,
    Player2Move = 3
}