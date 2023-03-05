using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knight : BaseHero
{
    public new string UnitName = "Knight";
    public BaseHero OccupiedUnit;
    public Knight _knight;

    private void Awake()
    {
        UnitName = "Knight";
    }

    public override void baseAttack(BaseHero baseHero)
    {

       OnMouseDown();
        
       

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

    public override void ability1(BaseHero baseHero)
    {

    }

    public override void ability2(BaseHero baseHero)
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
