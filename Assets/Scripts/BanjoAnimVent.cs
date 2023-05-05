using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MercenariesProject
{
    public class BanjoAnimVent : MonoBehaviour
    {

        /*private Animator anim;
        [SerializeField] GameObject Banjo;
        [SerializeField] Transform ArrowPosition;
        */
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void disappearBanjoBack()
        {
            Renderer visible;
            GameObject banjoBack = GameObject.Find("banjoBack");
            visible = banjoBack.GetComponent<Renderer>();
            visible.enabled = false;
        }

        public void AppearBanjoBack()
        {
            Renderer visible;
            GameObject banjoBack = GameObject.Find("banjoBack");
            visible = banjoBack.GetComponent<Renderer>();
            visible.enabled = true;
        }

        public void disappearBanjo()
        {
            Renderer visible;
            GameObject banjo = GameObject.Find("banjo");
            visible = banjo.GetComponent<Renderer>();
            visible.enabled = false;
        }


        public void AppearBanjo()
        {
            Renderer visible;
            GameObject banjo = GameObject.Find("banjo");
            visible = banjo.GetComponent<Renderer>();
            visible.enabled = true;
        }
    }
}
