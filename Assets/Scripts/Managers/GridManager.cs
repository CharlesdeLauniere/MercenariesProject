using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Tile _grassTile, _mountainTile;
    [SerializeField] Tile _grassTile1,_grassTile2,_grassTile3,_grassTile4,_grassTile5,_grassTile6,_grassTile7;
    [SerializeField] Tile _mountTile1;
    [SerializeField] int nombreTiles;
    [SerializeField] int _nombreMoutain;
    [SerializeField] GameObject _TurnSystem;
    [SerializeField] private int _longueurGrid, _largeurGrid;
    [SerializeField] private GameObject _acessibleTileIndicator, _inacessibleTileIndicator;
   
    
    public static GridManager Instance;

    public Dictionary<Vector2, Tile> _tiles;

  
    public Tile RandomGrassTile()
    {
        List<Tile> ListTile = new List<Tile>();
        ListTile.Add(_grassTile1);
        ListTile.Add(_grassTile2);
        ListTile.Add(_grassTile3);
        ListTile.Add(_grassTile4);
        ListTile.Add(_grassTile5);
        ListTile.Add(_grassTile6);
        ListTile.Add(_grassTile7);
        int random = Random.Range(0,nombreTiles);
        Debug.Log("Bob   "+random);
        return ListTile[random];
    }
    public Tile RandomMoutainTile()
    {
        List<Tile> ListTile = new List<Tile>();
        ListTile.Add(_mountTile1);
       
      
        int random = Random.Range(0,_nombreMoutain);
        return ListTile[random];
    }
    
    void Awake()
    {
        Instance = this;
    }
    //float x =_CubeObstacle.transform.position.x;
    //float z = _CubeObstacle.transform.position.z;
    //GameObject Indic = GameObject.Find($"IndicR {x} {z}");
    //Indic.GetComponent<MeshRenderer>().enabled = true;
    //private void Update()
    //{
    //    if (GridManager.Instance.path.Count > 0)// && GridManager.Instance.path != null && GridManager.Instance.moveSpeed != 0)
    //    {
    //        GridManager.Instance.MoveAlongPath();
    //    }
    //    if (GridManager.Instance.path.Count == 0 && TurnManager.Instance.currentState == TurnManager.TurnState.movement)
    //    {
    //        TurnManager.Instance.SwitchBetweenTurnStates(TurnManager.TurnState.selectingAttack);
    //    }
       
    //}
    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        //GameObject IndicContainer = new GameObject("Indicators");
        //GameObject CubeContainer = new GameObject("CubesTileContainer");
        for (int i = 0; i < _longueurGrid; i++)
        {
            for (int j = 0; j < _largeurGrid; j++)
            {
                
                
     
                
                    //better biome gen a mettre
                    var randomTile = Random.Range(0, 6) == 3 ? RandomMoutainTile() : RandomGrassTile();
                    var tileLocation = new Vector2Int(i, j);
                    var spawnedTile = Instantiate(randomTile, new Vector3(i, -0.38f, j), Quaternion.identity);
                    spawnedTile.name = $"Tile {i} {j}";
                    /*
                    spawnedTile.transform.SetParent(CubeContainer.transform);var indicatorG = Instantiate(_acessibleTileIndicator, new Vector3(i, .125f, j), Quaternion.identity);
                    indicatorG.name = $"IndicG {i} {j}";
                    indicatorG.transform.SetParent(IndicContainer.transform);
                    var indicatorR = Instantiate(_inacessibleTileIndicator, new Vector3(i, .125f, j), Quaternion.identity);
                    indicatorR.name = $"IndicR {i} {j}";
                    indicatorR.transform.SetParent(IndicContainer.transform);*/
                    spawnedTile.Init(i, j);
                    var isOffset = ((i + j) % 2 != 0);
                    spawnedTile.Init(isOffset);
                    spawnedTile.gridLocation = tileLocation;
                    _tiles[new Vector2(i, j)] = spawnedTile;
                
            }
            
        }
        GameManager.Instance.ChangeState(GameState.SpawnHeroes);

    }
    public Tile GetRedHeroSpawnTile()
    {
        return _tiles.Where(t => t.Key.x < _largeurGrid / 2  && 
        t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }
    public Tile GetBlueHeroSpawnTile()
    {
        return _tiles.Where(t => t.Key.x < _largeurGrid && t.Key.x > _largeurGrid / 2 &&
        t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
   
}
