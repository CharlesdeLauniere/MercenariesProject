using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : MonoBehaviour
{
    
    public string UnitName;
    public Tile OccupiedTile;
    public Faction Faction;
    public int _pvInitial;
    public int _currentPv;

    public virtual void attaque1(BaseHero baseHero)
    {
        
    }

    public virtual void habileté1(BaseHero baseHero)
    {

    }

    public virtual void habileté2(BaseHero baseHero)
    {

    }
}
