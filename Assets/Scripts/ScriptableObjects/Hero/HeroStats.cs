using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MercenariesProject
{
    //Generate character stats with the character class and levels.
    [CreateAssetMenu(fileName = "HeroStats", menuName = "ScriptableObjects/HeroStats", order = 1)]
    public class HeroStats : ScriptableObject
    {

        public Stat Health;
        public Stat Mana;
        public Stat Strenght;
        public Stat Endurance;
        public Stat Speed;
        public Stat Intelligence;
        public Stat MoveRange;
        public Stat AttackRange;
        public Stat CurrentHealth;
        public Stat CurrentMana;


        public Stat getStat(Stats statKey)
        {
            var fields = typeof(HeroStats).GetFields();

            foreach (var item in fields)
            {
                var type = item.FieldType;
                Stat value = (Stat)item.GetValue(this);

                if (value.statKey == statKey)
                    return value;
            }

            return null;
        }
    }
}
