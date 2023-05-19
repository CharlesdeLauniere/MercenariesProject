using System;
using UnityEngine;

namespace MercenariesProject
{
    //Effets qu'on peut mettre sur les tuiles ou les habilités
    [CreateAssetMenu(fileName = "ScriptableEffect", menuName = "ScriptableObjects/ScriptableEffect")]
    public class ScriptableEffect : ScriptableObject
    {
        public Stats statKey;
        public Operation Operator;
        public int Duration;
        public int Value;

        public Stats GetStatKey()
        {
            return statKey;
        }
    }
}