using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Archer : BaseHero
{
    private void Awake()
    {
        _maxHealth = 120;
        _currentHealth = _maxHealth;
        UnitName = "Archer";
        _baseAttackDmg = 30;
        
    }
    public override void BaseAttack(BaseHero baseHero)
    {
        baseHero.TakeDamage(_baseAttackDmg);
    }
}
