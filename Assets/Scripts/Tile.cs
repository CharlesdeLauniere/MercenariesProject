using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool _isWalkable;

    public BaseHero OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;


    public virtual void Init(int x, int z)
    {

    }

    public void SetUnit(BaseHero hero)
    {
        if (hero.OccupiedTile != null) hero.OccupiedTile.OccupiedUnit = null;
        Vector3 temp = transform.position;
        temp.y = 0.2f;
        hero.transform.position = temp;
        OccupiedUnit = hero;
        hero.OccupiedTile = this;
    }
}

    

