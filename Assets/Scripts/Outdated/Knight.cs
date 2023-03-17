using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace MercenariesProject
{
   

    public class Knight : BaseHero
    {

        private void Awake()
        {
            UnitName = "Knight";
            _maxHealth = 200;
            _currentHealth = _maxHealth;
            _baseAttackDmg = 30;
            this.SetupHealthBar();
            Initiative = 2;

        }


        public override void Ability1(BaseHero baseHero)
        {

        }

        public override void Ability2(BaseHero baseHero)
        {

        }

        //private void OnMouseDown()
        //{
        //    if (OccupiedUnit != null)
        //    {
        //        if (OccupiedUnit.Faction != _knight.Faction) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
        //        else
        //        {
        //            if (UnitManager.Instance.SelectedHero != null)
        //            {
        //                BaseHero baseHero = (BaseHero)OccupiedUnit;
        //                UnitManager.Instance.SetTargetedHero(baseHero);
        //                UnitManager.Instance.SetSelectedHero(null);
        //            }
        //        }
        //    }

        //}
    }

}