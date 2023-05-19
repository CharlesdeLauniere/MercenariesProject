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

        [SerializeField] Tile _overlayTile;
        [SerializeField] List<MapTile> MapTiles;
        public TileDataRuntimeSet tileTypeList;
        [SerializeField] private int XGridSize, ZGridSize;
        [SerializeField] private GameObject OverlayTilesContainer;

        public Dictionary<GameObject, TileData> dataFromTiles = new();

        private Hero activeHero;

         // Tuiles overlay
        public Dictionary<Vector2Int, Tile> tileMap = new();
         // Cubes de la map
        public Dictionary<Vector2Int, GameObject> grid = new();

        public GameEventStringList spawnHeroesByList;


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
        }

        public void GenerateGrid()
        {

            GameObject CubeContainer = new GameObject("CubesTileContainer");
            for (int i = 0; i < XGridSize; i++)
            {
                for (int j = 0; j < ZGridSize; j++)
                {
                   
                    MapTile randomTileType = GetRandomMapTile();

                    var randomTile = randomTileType.GetRandomVariant();
                    var spawnedTile = Instantiate(randomTile, new Vector3(i, -0.38f, j), Quaternion.identity);
                    grid[new Vector2Int(i, j)] = spawnedTile;
                    spawnedTile.name = $"MapTile {i} {j}";

                    spawnedTile.transform.SetParent(CubeContainer.transform);

                }

            }
            GenerateOverlayTiles();

        }
        
        //Ancienne manière de déterminer les équipes avant le draft
        public void TempSetHeroesToSpawnList()
        {
            HeroesToSpawnList.heroesTospawn = new List<string> { "Knight", "Archer", "Mage", "Bard", "Vampire", "Gragas" };
            spawnHeroesByList.Raise(HeroesToSpawnList.heroesTospawn);
        }

        public void GenerateOverlayTiles()
        {
  
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



            for (int x = 0; x < XGridSize; x++)
            {
                for (int z = 0; z < ZGridSize; z++)
                {

                    var overlayTile = Instantiate(_overlayTile, new Vector3(x, 0.121f, z), Quaternion.identity);
                    overlayTile.transform.Rotate(90, 0, 0);
                    overlayTile.name = $"MapTile {x} {z}";

                    overlayTile.transform.SetParent(OverlayTilesContainer.transform);
                    var tileKey = new Vector2Int(x, z);
                    if (!tileMap.ContainsKey(tileKey)) 
                    {

                        var tileLocation = overlayTile.transform.position;
                        var baseTile = grid.GetValueOrDefault(tileKey);
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
                if (currentWeightIndex > itemWeightIndex)
                        return variant;
                counter++;
            }
            return null;
        }
        

        public int[] GetGridSize()
        {
            return new int[] { XGridSize, ZGridSize };
        }
        public Tile GetRedHeroSpawnTile()
        {
            return tileMap.Where(t => t.Key.x < ZGridSize / 2 &&
            t.Value.tileData.type == TileTypes.Traversable).OrderBy(t => Random.value).First().Value;
        }
        public Tile GetBlueHeroSpawnTile()
        {
            return tileMap.Where(t => t.Key.x < ZGridSize && t.Key.x > ZGridSize / 2 &&
             t.Value.tileData.type == TileTypes.Traversable).OrderBy(t => Random.value).First().Value;
        }
        public Tile GetTileAtPosition(Vector2Int pos)
        {
            if (tileMap.TryGetValue(pos, out var tile))
            {
                return tile;
            }
            return null;
        }
        
        public List<Tile> GetNeighbourTiles(Tile currentTile, List<Tile> searchableTiles, bool ignoreObstacles = false, bool walkThroughAllies = true)
        {
            Dictionary<Vector2Int, Tile> tileToSearch = new();

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
                    currentTile.gridLocation.y + 1);

                ValidateNeighbour(currentTile, ignoreObstacles, walkThroughAllies, tileToSearch, neighbours, locationToCheck);

                //bottom
                locationToCheck = new Vector2Int(
                    currentTile.gridLocation.x,
                    currentTile.gridLocation.y - 1);


                ValidateNeighbour(currentTile, ignoreObstacles, walkThroughAllies, tileToSearch, neighbours, locationToCheck);

                //right
                locationToCheck = new Vector2Int(
                    currentTile.gridLocation.x + 1,
                    currentTile.gridLocation.y);


                ValidateNeighbour(currentTile, ignoreObstacles, walkThroughAllies, tileToSearch, neighbours, locationToCheck);

                //left
                locationToCheck = new Vector2Int(
                    currentTile.gridLocation.x - 1,
                    currentTile.gridLocation.y);


                ValidateNeighbour(currentTile, ignoreObstacles, walkThroughAllies, tileToSearch, neighbours, locationToCheck);
            }

            return neighbours;
        }
        private static void ValidateNeighbour(Tile currentTile, bool ignoreObstacles, bool walkThroughAllies,
            Dictionary<Vector2Int, Tile> tilesToSearch, List<Tile> neighbours, Vector2Int locationToCheck)
        {
            if (tilesToSearch.ContainsKey(locationToCheck) &&
                (ignoreObstacles || (!ignoreObstacles && tilesToSearch[locationToCheck].isWalkable) ||
                (!ignoreObstacles && walkThroughAllies && (tilesToSearch[locationToCheck].activeHero && Instance.activeHero &&
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

        //Cherche la liste de tuiles overlay par la position
        public List<Tile> GetOverlayTilesFromGridPositions(List<Vector2Int> positions)
        {
            List<Tile> overlayTiles = new List<Tile>();

            foreach (var item in positions)
            {
                overlayTiles.Add(tileMap[item]);
            }

            return overlayTiles;
        }
        public void ClearTiles()
        {
            foreach (var item in tileMap.Values)
            {
                item.HideTile();
            }
        }


    }

}