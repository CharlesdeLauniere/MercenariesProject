using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MercenariesProject
{
    [CreateAssetMenu(fileName = "HeroClass", menuName = "ScriptableObjects/HeroClass", order = 1)]
    public class HeroClass : ScriptableObject
    {
        public BaseStat Health;
        public BaseStat Mana;
        public BaseStat Strength;
        public BaseStat Endurance;
        public BaseStat Speed;
        public BaseStat Intelligence;

        public int MoveRange;
        public int AttackRange;


        public List<Ability> abilities;

    }
}
