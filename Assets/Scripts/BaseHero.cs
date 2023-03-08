using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : MonoBehaviour
{

    public string UnitName ="";
    public Tile OccupiedTile;
    public Faction Faction;
    public int _maxHealth;
    public int _currentHealth;
    public int _baseAttackDmg;
    public HealthBar HealthBar;
    public int BaseAttackRange { get; set; }

 

    public string GetUnitName()
    {
        return this.UnitName;
    }
    public void BaseAttack(BaseHero targetHero)
    {
        targetHero.TakeDamage(_baseAttackDmg);

    }

    public virtual void Ability1(BaseHero baseHero)
    {

    }

    public virtual void Ability2(BaseHero baseHero)
    {

    }
    public void TakeDamage(int baseAttackDmg)
    {
        _currentHealth -= baseAttackDmg;
        this.HealthBar.SetHealth(_currentHealth);
        if (_currentHealth < 0) this.Dead(this);

    }
    public void Dead(BaseHero baseHero)
    {
        Destroy(HealthBar);
        UnitManager.Instance.baseHeroes.Remove(UnitManager.Instance.baseHeroes.Find(o => o.name == baseHero.name && o.Faction == baseHero.Faction));
        Destroy(gameObject);
        
    }

    public void SetupHealthBar()
    {
        HealthBar.SetMaxHealth(_maxHealth);
    }
    
    
 
}
