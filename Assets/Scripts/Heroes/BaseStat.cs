using System;
using UnityEngine;

namespace MercenariesProject
{
    //A stat object used for character attribute scaling on level up. 
    [Serializable]
    public class BaseStat
    {
        [SerializeField]
        public int baseStatValue;

        //[SerializeField]
        //public AnimationCurve baseStatModifier = new AnimationCurve();
    }
}
