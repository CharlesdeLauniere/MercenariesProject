using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Tile _grassTile, _mountainTile;
    [SerializeField] GameObject _TurnSystem;
    [SerializeField] private int _longueurGrid, _largeurGrid;
    [SerializeField] private GameObject _acessibleTileIndicator, _inacessibleTileIndicator;

    public static GridManager Instance;

    private Dictionary<Vector2, Tile> _tiles;
    void Awake()
    {
        Instance= this;
    }
        //float x =_CubeObstacle.transform.position.x;
        //float z = _CubeObstacle.transform.position.z;
        //GameObject Indic = GameObject.Find($"IndicR {x} {z}");
        //Indic.GetComponent<MeshRenderer>().enabled = true;

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
                var randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
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
