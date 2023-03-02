using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knight : BaseHero
{

    public BaseHero OccupiedUnit;
    public Knight _knight;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void attaque1(BaseHero baseHero)
    {

       
        

        /*if (OccupiedTile != null && GameManager.Instance.GameState == GameState.BluePlayerTurn)
        {
            if (OccupiedUnit.Faction == Faction.Blue) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
           
             else if (UnitManager.Instance.SelectedHero != null)
             {
                var enemyHero = (BaseHero)OccupiedUnit;
                baseHero._currentPv -= 15;
                UnitManager.Instance.SetSelectedHero(null);
             }
            
        }*/
    }

    public override void habileté1(BaseHero baseHero)
    {

    }

    public override void habileté2(BaseHero baseHero)
    {

    }

    private void OnMouseDown()
    {
        if (OccupiedUnit != null)
        {
            if (OccupiedUnit.Faction != _knight.Faction) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            else
            {
                if (UnitManager.Instance.SelectedHero != null)
                {
                    BaseHero baseHero = (BaseHero)OccupiedUnit;
                    UnitManager.Instance.SetTargetedHero(baseHero);
                    UnitManager.Instance.SetSelectedHero(null);
                }
            }
        }
       
    }
}
