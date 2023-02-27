using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMouvement : MonoBehaviour
{
    [SerializeField] private int _deplacements = 3;



    public void onMove(float _x, float _z)
    {
        if (_deplacements >= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i+_x-1 < 0 || i + _x - 1 > 10|| j + _z - 1 < 0 || j + _z - 1 > 10)
                    {
                        continue;
                    }
                    GameObject Indic = GameObject.Find($"IndicG {i + _x - 1} {j + _z - 1}");
                    Indic.GetComponent<MeshRenderer>().enabled = false;
                }
            }
            

        }
        if (_deplacements > 0)
        {
            float z = this.transform.position.z;
            float x = this.transform.position.x;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i + x - 1 < 0 || i + x - 1 > 10 || j + z - 1 < 0 || j + z - 1 > 10)
                    {
                        continue;
                    }
                    GameObject Indic = GameObject.Find($"IndicG {i+x-1} {j+z-1}");
                    Indic.GetComponent<MeshRenderer>().enabled = true;
                }
            }
           

        }
        _deplacements--;
        if (_deplacements == 0)
        {
           


        }
    }

   
    public void OnAwake()
    {
        
        
          float z = this.transform.position.z;
          float x = this.transform.position.x;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                if (i + x - 1 < 0 || i + x - 1 > 10 || j + z - 1 < 0 || j + z - 1 > 10)
                {
                    continue;
                }
                   GameObject Indic = GameObject.Find($"IndicG {i+x-1} {j+z-1}");
                   Indic.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            

        


    }
    public Vector3 getPosition()
    {
        return this.transform.position;
    }

}


     //GameObject mapGenerator = GameObject.Find("MapGenerator");
     //GameObject[,] matrix = mapGenerator.GetComponent<MapGenerator>.getMapMatrix();
