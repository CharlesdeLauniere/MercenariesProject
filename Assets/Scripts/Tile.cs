using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    public string TileName;
    public Color TextColor, TextBoxColor;
    [SerializeField] private bool _isWalkable, _unitInRange;
    [SerializeField] protected Renderer _renderer;
    [SerializeField] protected Color _baseColour, _offsetColour, _highlightColour;

    public BaseHero OccupiedUnit;

    public bool Walkable => _isWalkable && OccupiedUnit == null ;

    public void Init(bool isOffset)
    {
        _renderer.material.color = isOffset ? _offsetColour : _baseColour;
    }
  

    public virtual void Init(int x, int z)
    {

    }
    private void OnMouseEnter()
    {
        _renderer.material.color = _highlightColour;
        MenuManager.Instance.ShowTileInfo(this);
    }
    private void OnMouseExit()
    {
        var isOffset = ((transform.position.x + transform.position.z) % 2 != 0);
        if (isOffset == false) _renderer.material.color = _baseColour;
        if (isOffset == true) _renderer.material.color = _offsetColour;

        MenuManager.Instance.ShowTileInfo(null);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.TurnBasedCombat) return;

        if (OccupiedUnit != null && TurnManager.Instance.currentState == TurnManager.TurnState.chosingTarget)
        {
            if (OccupiedUnit.Faction != UnitManager.Instance.SelectedHero.Faction)
            { 
                var enemyHero = (BaseHero)OccupiedUnit;
                UnitManager.Instance.SetTargetedHero(enemyHero);
                MenuManager.Instance.ShowTargetedHero(enemyHero);
                MenuManager.Instance.ShowAbilities(null);
                TurnManager.Instance.SwitchBetweenTurnStates(TurnManager.TurnState.usingBaseAttack);
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

    

