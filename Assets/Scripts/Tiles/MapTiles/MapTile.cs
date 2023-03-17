using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MercenariesProject
{

    public class MapTile : MonoBehaviour
    {
        [SerializeField] List<GameObject> TileVariants;
        [SerializeField] List<int> Weights;
        [SerializeField] int MapTileWeight;

        public int GetMapTileWeight()
        {
            return MapTileWeight;
        }

        public GameObject GetRandomVariant()
        {
            float totalWeight = 0;
           
            foreach (var weight in Weights)
            {
                totalWeight += weight;  

            }
            float itemWeightIndex = (float)new System.Random().NextDouble() * totalWeight;
            float currentWeightIndex = 0;

            int counter = 0;
            foreach (var variant in TileVariants)
            {
                
                    currentWeightIndex += Weights[counter];
                    // If we've hit or passed the weight we are after for this item then it's the one we want....
                    if (currentWeightIndex > itemWeightIndex)
                        return variant;
                counter++;
            }
            return null;
        }
        
    }
}
