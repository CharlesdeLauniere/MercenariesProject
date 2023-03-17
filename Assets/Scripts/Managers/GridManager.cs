using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MercenariesProject
{

    public class GridManager : MonoBehaviour
    {
        public static GridManager Instance;
        //public static GridManager Instance { get { return _instance; } }

        [SerializeField] Tile _overlayTile;
        [SerializeField] List<MapTile> MapTiles;
        public TileDataRuntimeSet tileTypeList;
        //SerializeField] GameObject _TurnSystem;
        [SerializeField] private int XGridSize, ZGridSize;
        [SerializeField] private GameObject OverlayTilesContainer,_acessibleTileIndicator, _inacessibleTileIndicator;

        public Dictionary<GameObject, TileData> dataFromTiles = new Dictionary<GameObject, TileData>();

        private Hero activeHero;

        public Dictionary<Vector2Int, Tile> tileMap = new();
        public Dictionary<Vector2Int, GameObject> grid = new();




        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
            //GenerateGrid();
            //GenerateOverlayTiles();
        }

        public void GenerateGrid()
        {

            //Dictionary<Vector2Int, GameObject> grid = new();
            GameObject CubeContainer = new GameObject("CubesTileContainer");
            for (int i = 0; i < XGridSize; i++)
            {
                for (int j = 0; j < ZGridSize; j++)
                {
                   
                    MapTile randomTileType = GetRandomMapTile();

                    var randomTile = randomTileType.GetRandomVariant();
                    //var tileLocation = new Vector2Int(i, j);
                    var spawnedTile = Instantiate(randomTile, new Vector3(i, -0.38f, j), Quaternion.identity);
                    //grid[new Vector2Int(i, j)] = spawnedTile;
                    grid[new Vector2Int(i, j)] = spawnedTile;
                    spawnedTile.name = $"MapTile {i} {j}";

                    spawnedTile.transform.SetParent(CubeContainer.transform);
                    //if (spawnedTile.isWalkable)
                    //{
                    //    var indicatorG = Instantiate(_acessibleTileIndicator, new Vector3(i, .125f, j), Quaternion.identity);
                    //    indicatorG.name = $"IndicG {i} {j}";
                    //    indicatorG.transform.SetParent(spawnedTile.transform);
                    //}


                    //_indicators[new Vector2Int(i, j)] = indicatorG;
                    /*
                    var indicatorR = Instantiate(_inacessibleTileIndicator, new Vector3(i, .125f, j), Quaternion.identity);
                    indicatorR.name = $"IndicR {i} {j}";
                    indicatorR.transform.SetParent(IndicContainer.transform);*/
                    //spawnedTile.Init(i, j);
                    //var isOffset = ((i + j) % 2 != 0);
                    //spawnedTile.Init(isOffset);
                    //spawnedTile.gridLocation = tileLocation;
                    
                }

            }
            GenerateOverlayTiles();

        }
        public void GenerateOverlayTiles()
        {
            GameObject OverlayTilesContainer = new GameObject("OverlayTilesContainer");
            if (tileTypeList)
            {
                foreach (var tileData in tileTypeList.items)
                {
                    foreach (var item in tileData.baseTile)
                    {
                        dataFromTiles.Add(item, tileData);
                    }
                }
            }

           // gridTilemap = gameObject.GetComponentInChildren<Tilemap>();
            //tileMap = new Dictionary<Vector2Int, Tile>();




            //loop through the tilemap and create all the overlay tiles

            for (int x = 0; x < XGridSize; x++)
            {
                for (int z = 0; z < ZGridSize; z++)
                {

                    var overlayTile = Instantiate(_overlayTile, new Vector3(x, 0.6f, z), Quaternion.identity);
                    overlayTile.transform.SetParent(OverlayTilesContainer.transform);
                    var tileKey = new Vector2Int(x, z);
                    if (!tileMap.ContainsKey(tileKey))//gridTilemap.HasTile(tileKey) && 
                    {

                        var tileLocation = new Vector3(x, 0.6f, z);
                        var baseTile = grid.GetValueOrDefault(tileKey);
                        overlayTile.transform.position = tileLocation;
                        //overlayTile.GetComponent<SpriteRenderer>().sortingOrder = gridTilemap.GetComponent<TilemapRenderer>().sortingOrder;
                        overlayTile.gridLocation = new Vector2Int(x, z);

                        foreach (var tileData in tileTypeList.items)
                        {
                            foreach (var material in tileData.Tiles3D)
                            {
                                if (material == baseTile.GetComponent<MeshRenderer>().sharedMaterial)
                                {
                                    overlayTile.tileData = tileData;
                                }
                            }
                        }

                        if (dataFromTiles.ContainsKey(baseTile))
                        {
                            overlayTile.tileData = dataFromTiles[baseTile];
                            if (dataFromTiles[baseTile].type == TileTypes.NonTraversable)
                                overlayTile.isWalkable = false;
                        }

                        if (!tileMap.ContainsKey(tileKey))
                        {
                            tileMap.Add(tileKey, overlayTile);
                        }
                        else
                        {
                            tileMap.Remove(tileKey);
                            tileMap.Add(tileKey, overlayTile);
                        }
                    }

                }
            }

        }
        public MapTile GetRandomMapTile()
        {
            List<int> Weights = new List<int>();
            foreach(var mapTile in MapTiles)
            {
                Weights.Add(mapTile.GetMapTileWeight());
            }
            float totalWeight = 0;
            int counter = 0;
            foreach (var variant in MapTiles)
            {
                totalWeight += Weights[counter];
                counter++;


            }
            float itemWeightIndex = (float)new System.Random().NextDouble() * totalWeight;
            float currentWeightIndex = 0;
            counter = 0;
            foreach (var variant in MapTiles)
            {
                
                    currentWeightIndex += Weights[counter];
                // If we've hit or passed the weight we are after for this item then it's the one we want....
                if (currentWeightIndex > itemWeightIndex)
                        return variant;
                counter++;
            }
            return null;
        }
        
        //public void GenerateOverlayTiles()
        //{
        //    GameObject TileContainer = new GameObject("OverlayTileContainer");
        //    tileMap = new Dictionary<Vector2Int, Tile>();
        //    for (int i = 0; i < _longueurGrid; i++)
        //    {
        //        for (int j = 0; j < _largeurGrid; j++)
        //        {

        //            var tileLocation = new Vector2Int(i, j);
        //            var spawnedTile = Instantiate(overlayTile, new Vector3(i, -0.38f, j), Quaternion.identity);
        //            spawnedTile.name = $"Tile {i} {j}";

        //            spawnedTile.transform.SetParent(TileContainer.transform);
        //            if (spawnedTile.isWalkable)
        //            {
        //                var indicatorG = Instantiate(_acessibleTileIndicator, new Vector3(i, .125f, j), Quaternion.identity);
        //                indicatorG.name = $"IndicG {i} {j}";
        //                indicatorG.transform.SetParent(spawnedTile.transform);
        //            }


        //            //_indicators[new Vector2Int(i, j)] = indicatorG;
        //            /*
        //            var indicatorR = Instantiate(_inacessibleTileIndicator, new Vector3(i, .125f, j), Quaternion.identity);
        //            indicatorR.name = $"IndicR {i} {j}";
        //            indicatorR.transform.SetParent(IndicContainer.transform);*/
        //            spawnedTile.Init(i, j);
        //            var isOffset = ((i + j) % 2 != 0);
        //            //spawnedTile.Init(isOffset);
        //            spawnedTile.gridLocation = tileLocation;
        //            tileMap[new Vector2Int(i, j)] = spawnedTile;
        //        }

        //    }
        //}

        public int[] GetGridSize()
        {
            return new int[] { XGridSize, ZGridSize };
        }
        public Tile GetRedHeroSpawnTile()
        {
            return tileMap.Where(t => t.Key.x < ZGridSize / 2 &&
            t.Value.isWalkable).OrderBy(t => Random.value).First().Value;
        }
        public Tile GetBlueHeroSpawnTile()
        {
            return tileMap.Where(t => t.Key.x < ZGridSize && t.Key.x > ZGridSize / 2 &&
            t.Value.isWalkable).OrderBy(t => Random.value).First().Value;
        }
        public Tile GetTileAtPosition(Vector2Int pos)
        {
            if (tileMap.TryGetValue(pos, out var tile))
            {
                return tile;
            }
            return null;
        }
        //public List<Tile> GetSurroundingTiles(Vector2Int originTile)
        //{
        //    Dictionary<Vector2IntInt, Tile> tileToSearch = new Dictionary<Vector2IntInt, Tile>();
        //    var surroundingTiles = new List<Tile>();
        //    ////if (searchableTiles.Count > 0)
        //    ////{
        //    ////    foreach (var item in searchableTiles)
        //    ////    {
        //    ////        tileToSearch.Add(item.gridLocation, item);
        //    ////    }

        //    ////}
        //    ////else
        //    ////{
        //    ////    tileToSearch = tileMap;
        //    ////}


        //    //top neighbour
        //    Vector2IntInt locationToCheck = new Vector2IntInt(originTile.x, originTile.y + 1);
        //    if (tileMap.ContainsKey(locationToCheck))
        //    {
        //        surroundingTiles.Add(tileMap[locationToCheck]);
        //    }
        //    //bottom neighbour
        //    locationToCheck = new Vector2IntInt(originTile.x, originTile.y - 1);
        //    if (tileMap.ContainsKey(locationToCheck))
        //    {
        //        surroundingTiles.Add(tileMap[locationToCheck]);
        //    }
        //    //right neighbour
        //    locationToCheck = new Vector2IntInt(originTile.x + 1, originTile.y);
        //    if (tileMap.ContainsKey(locationToCheck))
        //    {
        //        surroundingTiles.Add(tileMap[locationToCheck]);
        //    }
        //    //left neighbour
        //    locationToCheck = new Vector2IntInt(originTile.x - 1, originTile.y);
        //    if (tileMap.ContainsKey(locationToCheck))
        //    {
        //        surroundingTiles.Add(tileMap[locationToCheck]);
        //    }
        //    return surroundingTiles;
        //}
        public List<Tile> GetNeighbourTiles(Tile currentTile, List<Tile> searchableTiles, bool ignoreObstacles = false, bool walkThroughAllies = true)
        {
            Dictionary<Vector2Int, Tile> tileToSearch = new Dictionary<Vector2Int, Tile>();

            if (searchableTiles.Count > 0)
            {
                foreach (var item in searchableTiles)
                {
                    tileToSearch.Add(item.gridLocation, item);
                }
            }
            else
            {
                tileToSearch = tileMap;
            }

            List<Tile> neighbours = new List<Tile>();
            if (currentTile != null)
            {
                //top
                Vector2Int locationToCheck = new Vector2Int(
                    currentTile.gridLocation.x,
                    currentTile.gridLocation.y + 1
                    );

                ValidateNeighbour(currentTile, ignoreObstacles, walkThroughAllies, tileToSearch, neighbours, locationToCheck);

                //bottom
                locationToCheck = new Vector2Int(
                    currentTile.gridLocation.x,
                    currentTile.gridLocation.y - 1
                    );


                ValidateNeighbour(currentTile, ignoreObstacles, walkThroughAllies, tileToSearch, neighbours, locationToCheck);

                //right
                locationToCheck = new Vector2Int(
                    currentTile.gridLocation.x + 1,
                    currentTile.gridLocation.y
                    );


                ValidateNeighbour(currentTile, ignoreObstacles, walkThroughAllies, tileToSearch, neighbours, locationToCheck);

                //left
                locationToCheck = new Vector2Int(
                    currentTile.gridLocation.x - 1,
                    currentTile.gridLocation.y
                    );


                ValidateNeighbour(currentTile, ignoreObstacles, walkThroughAllies, tileToSearch, neighbours, locationToCheck);
            }

            return neighbours;
        }
        private static void ValidateNeighbour(Tile currentTile, bool ignoreObstacles, bool walkThroughAllies,
            Dictionary<Vector2Int, Tile> tilesToSearch, List<Tile> neighbours, Vector2Int locationToCheck)
        {
            if (tilesToSearch.ContainsKey(locationToCheck) &&
                (ignoreObstacles ||
                (!ignoreObstacles && tilesToSearch[locationToCheck].isWalkable) ||
                (!ignoreObstacles &&
                walkThroughAllies &&
                (tilesToSearch[locationToCheck].activeHero && Instance.activeHero &&
                tilesToSearch[locationToCheck].activeHero.teamID == Instance.activeHero.teamID))))
            {
                neighbours.Add(tilesToSearch[locationToCheck]);
            }
        }


        public Tile GetOverlayByTransform(Vector3 position)
        {
            var gridLocation = new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z));

            if (tileMap.ContainsKey(new Vector2Int(gridLocation.x, gridLocation.y)))
                return tileMap[new Vector2Int(gridLocation.x, gridLocation.y)];

            return null;
        }

        //Get list of overlay tiles by grid positions. 
        public List<Tile> GetOverlayTilesFromGridPositions(List<Vector2Int> positions)
        {
            List<Tile> overlayTiles = new List<Tile>();

            foreach (var item in positions)
            {
                overlayTiles.Add(tileMap[item]);
            }

            return overlayTiles;
        }


    }

}