using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MercenariesProject
{
    [Serializable]
    public class Stat
    {
        public Stats statKey;
        public Hero hero;
        public string name;
        public float baseStatValue;
        public float statValue;
        public bool isModified;
        public List<StatModifier> statMods;

        public Stat(Stats statKey, float statValue, Hero hero)
        {
            this.hero = hero;
            this.statValue = statValue;
            this.statKey = statKey;

            baseStatValue = statValue;
            statMods = new List<StatModifier>();
            isModified = false;
        }
        public void ChangeStatValue(int newValue)
        {
            statValue = newValue;
            baseStatValue = newValue;
        }

        //Apply a modifier to a stat. Change stat value. 
        public void ApplyStatMods()
        {
            foreach (var statMod in statMods)
            {
                if (statMod != null)
                {
                    switch (statMod.Operator)
                    {
                        case Operation.Add:
                            statValue = Mathf.CeilToInt(statValue + statMod.value);
                            break;
                        case Operation.Minus:
                            if (statKey == Stats.CurrentHealth)
                            {
                                hero.TakeDamage(Mathf.CeilToInt(statMod.value));
                            }
                            else
                            {
                                statValue = Mathf.CeilToInt(statValue - statMod.value);
                            }
                            break;
                        case Operation.Multiply:
                            statValue = Mathf.CeilToInt(statValue * statMod.value);
                            break;
                        case Operation.Divide:
                            statValue = Mathf.CeilToInt(statValue / statMod.value);
                            break;
                        case Operation.AddByPercentage:
                            statValue = Mathf.CeilToInt(statValue * (1 + statMod.value / 100));
                            break;
                        case Operation.MinusByPercentage:
                            if (statKey == Stats.CurrentHealth)
                            {
                                float percentageDifference = (float)(statMod.value / 100f) * (float)baseStatValue;
                                hero.TakeDamage(Mathf.CeilToInt(percentageDifference), true);
                            }
                            else
                            {
                                statValue = Mathf.CeilToInt(statValue * (1 - statMod.value / 100));
                            }
                            break;
                    }

                    statMod.duration--;
                }
            }

            statMods.RemoveAll(x => x.duration <= 0);

            if (statMods.Count == 0)
                isModified = false;
        }
    }
}


