using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMouvement : MonoBehaviour
{
    [SerializeField] private int _range;
    [SerializeField] private GameObject cubeplayer;
    [SerializeField] private GameObject acessibleTileIndicator;


    public void onMove()
    {
        if (_range >0)
        {
            float z = cubeplayer.transform.position.z;
            float x = cubeplayer.transform.position.x;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GameObject cube = GameObject.Find($"Cube {x + i - 1} {z + j - 1}");

                    cube.GetComponent<Collider>().enabled = !cube.GetComponent<Collider>().enabled;
                    Instantiate(acessibleTileIndicator, new Vector3(x + i - 1, .125f, z + j - 1), Quaternion.identity);

                }
            }
            _range--;

        }


    }

    private void Start()
    {
        if (_range > 0)
        {
            float z = cubeplayer.transform.position.z;
            float x = cubeplayer.transform.position.x;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GameObject cube = GameObject.Find($"Cube {x + i - 1} {z + j - 1}");

                    cube.GetComponent<Collider>().enabled = !cube.GetComponent<Collider>().enabled;
                    Instantiate(acessibleTileIndicator, new Vector3(x + i - 1, .125f, z + j - 1), Quaternion.identity);

                }
            }
            _range = -1;

        }


    }

}


    // GameObject mapGenerator = GameObject.Find("MapGenerator");
    //GameObject[,] matrix = mapGenerator.GetComponent<MapGenerator>.getMapMatrix();
