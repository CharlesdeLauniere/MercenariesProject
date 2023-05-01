using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MercenariesProject
{
    public class BackTextManager : MonoBehaviour
    {
        [SerializeField] GameObject _text;
        private int compteur=0;

        private void Start()
        {
            while(true) { StartCoroutine(changeColor()); }
            

        }

        private IEnumerator changeColor()
        {
           
            if(compteur % 2 == 0) 
            {
                _text.GetComponent<TextMeshProUGUI>().color = Color.white;
            }
            else
            {
                _text.GetComponent<TextMeshProUGUI>().color = Color.blue;
            }

            yield return new WaitForSecondsRealtime(1f);

            compteur++;
        }
    }
}
