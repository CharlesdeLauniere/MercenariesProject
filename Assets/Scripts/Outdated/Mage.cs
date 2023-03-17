using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MercenariesProject
{
  
    public class Mage : BaseHero
    {

        private void Awake()
        {
            UnitName = "Mage";
            _maxHealth = 100;
            _currentHealth = _maxHealth;
            _baseAttackDmg = 20;
            Initiative = 20;
            this.SetupHealthBar();

        }
    }

}