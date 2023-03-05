using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AnimTestYouCanEraseIt : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public Transform Arrow;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (anim != null)
            {
                // play Bounce but start at a quarter of the way though
                anim.Play("Base Layer.drawArrow", 0, 0.05f);
                Instantiate(Arrow, new Vector3(0, 0, 0), Quaternion.identity);


            }
        }
    }
}
