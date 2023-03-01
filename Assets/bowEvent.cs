using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class bowEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void disappearArrow()
    {
        Renderer visible;
        GameObject Arrow = GameObject.Find("Arrow");
        visible = Arrow.GetComponent<Renderer>();
        visible.enabled = false;
    }

    public void AppearArrow()
    {
        Renderer visible;
        GameObject Arrow = GameObject.Find("Arrow");
        visible = Arrow.GetComponent<Renderer>();
        visible.enabled = true;
    }

    public void disappearRope()
    {
        Renderer visible;
        GameObject Rope = GameObject.Find("BowRope");
        visible = Rope.GetComponent<Renderer>();    
        visible.enabled = false;
    }


    public void AppearRope()
    {
        Renderer visible;
        GameObject Rope = GameObject.Find("BowRope");
        visible = Rope.GetComponent<Renderer>();
        visible.enabled = true;
    }

}
