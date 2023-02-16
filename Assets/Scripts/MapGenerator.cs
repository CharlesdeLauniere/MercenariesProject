using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject _tileCubeBasic;
    [SerializeField] GameObject _CubePlayer;
    [SerializeField] private int _grandeurGrid;




    private void Start()
    {
        GenerateGrid();

    }

    void GenerateGrid()
    {
        //Instantiate(_tileCubeBasic, new Vector3(0, 0.2f, 0), Quaternion.identity);
        GameObject newContainer = new GameObject("CubesTileContainer");
        Instantiate(newContainer, Vector3.zero, Quaternion.identity);

        GameObject[,] cubeGridMap = new GameObject[_grandeurGrid, _grandeurGrid];

        for (int i = 0; i < _grandeurGrid; i++)
        {
            for (int j = 0; j < _grandeurGrid; j++)
            {
                var spawnedCube = Instantiate(_tileCubeBasic, new Vector3(i, -0.38f, j), Quaternion.identity);
                spawnedCube.name = $"Cube {i} {j}";
                if( (i + j)%2 != 0)
                {
                    spawnedCube.GetComponent<Renderer>().material.color = Color.black;
                    spawnedCube.transform.parent = GameObject.Find("CubesTileContainer").transform;
                    cubeGridMap[i, j] = spawnedCube;
                }
               
            }
        }

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
