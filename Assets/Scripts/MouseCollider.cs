using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseCollider : MonoBehaviour
{
   
   
   
    void OnMouseDown()
    {
 
        float zTile = this.transform.position.z;
        float xTile = this.transform.position.x;
        GameObject cubeplayer = GameObject.Find("CubePlayer");
        if (GameObject.Find($"IndicG { xTile } { zTile }").GetComponent<Renderer>().isVisible==true&& 
            GameObject.Find($"IndicR {xTile} {zTile}").GetComponent<Renderer>().isVisible == false)
        {
            float x = cubeplayer.transform.position.x;
            float z = cubeplayer.transform.position.z;
            cubeplayer.transform.position = new Vector3(xTile, 0.2f, zTile);
            cubeplayer.GetComponent<PlayerMouvement>().onMove(x, z);
        }
        
    }
}
