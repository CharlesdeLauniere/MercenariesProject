using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MercenariesProject
{
    public class TurnOrderDisplay : MonoBehaviour
    {
        public GameObject portraitPrefab;


        private List<string> turnOrder;

        void Start()
        {
            turnOrder = new List<string>();
        }

        public void SetTurnOrderDisplay(List<GameObject> characters)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
                turnOrder.RemoveRange(0, turnOrder.Count);
            }

            turnOrder = new List<string>();

            foreach (var item in characters)
            {
                var spawnedObject = Instantiate(portraitPrefab, transform);
                spawnedObject.GetComponent<Image>().sprite = item.GetComponent<Hero>().portrait;

                turnOrder.Add(item.ToString() + " - " + item.name);
            }
        }
    }
}
