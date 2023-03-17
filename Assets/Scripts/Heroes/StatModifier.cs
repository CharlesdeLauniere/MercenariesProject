using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MercenariesProject
{
  


    public class StatModifier
    {
        public Stats attributeName;
        public float value;
        public int duration;
        public Operation Operator;
        public bool isActive;
        public string statModName;


        public StatModifier(Stats attribute, float value, int duration, Operation op, string statModName)
        {
            this.attributeName = attribute;
            this.value = value;
            this.duration = duration;
            this.Operator = op;
            this.statModName = statModName;
            isActive = true;
        }
    }
}
