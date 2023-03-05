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


    public string getUnitName()
        {
        return this.UnitName;
        }
    public virtual void baseAttack(BaseHero baseHero)
    {
        
    }

    public virtual void ability1(BaseHero baseHero)
    {

    }

    public virtual void ability2(BaseHero baseHero)
    {

    }
}
