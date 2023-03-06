using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : BaseHero
{

    private void Awake()
    {
        UnitName = "Mage";
        _maxHealth = 100;
        _currentHealth = _maxHealth;
        _baseAttackDmg = 10;
        
    }
    public override void BaseAttack(BaseHero baseHero)
    {
        baseHero.TakeDamage(_baseAttackDmg);
    }
}
