using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseCollider : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject tile;
    void OnMouseDown()
    {
        float zTile = tile.transform.position.z;
        float xTile = tile.transform.position.x;
        cube.transform.position = new Vector3(xTile, 0.2f, zTile);
    }
}
