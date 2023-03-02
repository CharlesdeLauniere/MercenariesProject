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
        
        if (GameObject.Find($"IndicG { xTile } { zTile }").GetComponent<Renderer>().isVisible==true&& 
            GameObject.Find($"IndicR {xTile} {zTile}").GetComponent<Renderer>().isVisible == false)
        {
            
        }

        //float x = player.transform.position.x;
        //float z = player.transform.position.z;
        //player.transform.position = new Vector3(xTile, 0.2f, zTile);
        //player.GetComponent<PlayerMouvement>().onMove(x, z);

        //GameObject player = GameObject.Find("CubePlayer1");
    }
}
