using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : MonoBehaviour
{

    public string UnitName;
    public Tile OccupiedTile;
    public Faction Faction;
    public int _maxHealth;
    public int _currentHealth;
    public int _baseAttackDmg;


    public string GetUnitName()
    {
        return this.UnitName;
    }
    public virtual void BaseAttack(BaseHero baseHero)
    {
        
    }

    public virtual void Ability1(BaseHero baseHero)
    {

    }

    public virtual void Ability2(BaseHero baseHero)
    {

    }

    public void TakeDamage(int baseAttackDmg)
    {
        if (_currentHealth - baseAttackDmg > 0) _currentHealth -= baseAttackDmg;
        
    }
}
