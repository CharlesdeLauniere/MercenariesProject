using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseCollider : MonoBehaviour
{
    
    [SerializeField] private GameObject tile;
    [SerializeField] private GameObject cubeplayer;
    void OnMouseDown()
    {
        
         GameObject cube = GameObject.Find("CubePlayer");
        float zTile = tile.transform.position.z;
        float xTile = tile.transform.position.x;
        cube.transform.position = new Vector3(xTile, 0.2f, zTile);
        cubeplayer.GetComponent<PlayerMouvement>().onMove();
    }
}
