using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class bowEvent : MonoBehaviour
{


    private Animator anim;
    [SerializeField] GameObject ArrowProjectile;
    [SerializeField] Transform ArrowPosition;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (anim != null)
            {
                // play Bounce but start at a quarter of the way though
                anim.Play("Base Layer.drawArrow", 0, 0.05f);

            }
        }*/
    }

    public void disappearArrow()
    {
        Renderer visible;
        GameObject Arrow = GameObject.Find("Arrow");
        visible = Arrow.GetComponent<Renderer>();
        visible.enabled = false;
        Instantiate(ArrowProjectile, ArrowPosition.transform.position, ArrowProjectile.transform.rotation);
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
