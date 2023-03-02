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
    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.BluePlayerTurn && 
            GameManager.Instance.GameState != GameState.RedPlayerTurn) return;

        if (OccupiedUnit != null && GameManager.Instance.GameState == GameState.BluePlayerTurn) {
            if (OccupiedUnit.Faction == Faction.Blue) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            else {
                if(UnitManager.Instance.SelectedHero != null) {
                    var enemyHero = (BaseHero)OccupiedUnit;
                    Destroy(enemyHero.gameObject);
                    UnitManager.Instance.SetSelectedHero(null);
                }
            }
        }
        else
        {
            if(UnitManager.Instance.SelectedHero != null)
            {
                SetUnit(UnitManager.Instance.SelectedHero);
                UnitManager.Instance.SetSelectedHero(null);
            }
        }
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

    

