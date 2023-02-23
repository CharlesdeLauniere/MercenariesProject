using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject _tileCubeBasic;
    [SerializeField] GameObject _CubePlayer;
    [SerializeField] GameObject _CubeObstacle;
    [SerializeField] private int _grandeurGrid;
    [SerializeField] private GameObject _acessibleTileIndicator;
    [SerializeField] private GameObject _inacessibleTileIndicator;


    private void Start()
    {
        GenerateGrid();
        GameObject player = Instantiate(_CubePlayer, new Vector3(4, 0.2f, 5), Quaternion.identity);
        player.name = "CubePlayer";

        float x =_CubeObstacle.transform.position.x;
        float z = _CubeObstacle.transform.position.z;
        GameObject Indic = GameObject.Find($"IndicR {x} {z}");
        Indic.GetComponent<MeshRenderer>().enabled = true;
       

        player.GetComponent<PlayerMouvement>().OnAwake();
    }

    void GenerateGrid()
    {
        GameObject IndicContainer = new GameObject("Indicators");
        GameObject CubeContainer = new GameObject("CubesTileContainer");

        //GameObject[,] cubeGridMap = new GameObject[_grandeurGrid, _grandeurGrid];

        for (int i = 0; i < _grandeurGrid; i++)
        {
            for (int j = 0; j < _grandeurGrid; j++)
            {
                var spawnedCube = Instantiate(_tileCubeBasic, new Vector3(i, -0.38f, j), Quaternion.identity);
                spawnedCube.name = $"Cube {i} {j}";
                spawnedCube.transform.SetParent(CubeContainer.transform);
                //cubeGridMap[i, j] = spawnedCube;

                var indicatorG = Instantiate(_acessibleTileIndicator, new Vector3(i, .125f, j), Quaternion.identity);
                indicatorG.name = $"IndicG {i} {j}";
                indicatorG.transform.SetParent(IndicContainer.transform);

                var indicatorR = Instantiate(_inacessibleTileIndicator, new Vector3(i, .125f, j), Quaternion.identity);
                indicatorR.name = $"IndicR {i} {j}";
                indicatorR.transform.SetParent(IndicContainer.transform);

                if ( (i + j)%2 != 0)
                {
                    spawnedCube.GetComponent<Renderer>().material.color = Color.black;
                }
               
            }
        }

    }
    void Obstacles()
    {

    }
     GameObject [,] getMapMatrix(int _grandeurGrid,GameObject cube)
    {
        GameObject[,] cubeGridMap = new GameObject[_grandeurGrid,_grandeurGrid];
        for (int i = 0; i < _grandeurGrid; i++)
        {
            for (int j = 0; j < _grandeurGrid; j++)
            {  
              cubeGridMap[i, j] = GameObject.Find($"Cube {i} {j}");
               

            }
        }

        return cubeGridMap;
    }

}
