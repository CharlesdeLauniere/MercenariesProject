using System.Collections.Generic;
using UnityEngine;

namespace MercenariesProject
{
   
    [CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability", order = 1)]
    public class Ability : ScriptableObject
    {
        [Header("General")]
        public string Name;

        public string Desc;

        [Header("Ability")]
        public TextAsset abilityShape;

        public List<ScriptableEffect> effects;

        public int range;

        public int cooldown;

        public int cost;

        public int value;

        public AbilityTypes abilityType;

        public bool includeOrigin;


        public enum AbilityTypes
        {
            Ally,
            Enemy,
            All
        }

       
    }
}