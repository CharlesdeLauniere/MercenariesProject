using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   public static MenuManager instance;

    [SerializeField] private GameObject _selectedHeroObject, _tileObject, _tileUnitObject, _attackButton, _targetHeroObject;
    private void Awake()
    {
        instance= this; 
    }

    public void ShowTileInfo(Tile tile)
    {
        if(tile==null)
        {
            _tileObject.SetActive(false);
            _tileUnitObject.SetActive(false);
            return;
        }
        _tileObject.GetComponentInChildren<TextMeshProUGUI>().text = tile.TileName;
        _tileObject.SetActive(true);

        if(tile.OccupiedUnit)
        {
            _tileUnitObject.GetComponentInChildren<TextMeshProUGUI>().text = tile.OccupiedUnit.UnitName;
            _tileUnitObject.SetActive(true);
        }
    }

    public void ShowSelectedHero(BaseHero hero)
    {
        if (hero == null)
        {
            _selectedHeroObject.SetActive(false);
            return;
        }
        _selectedHeroObject.GetComponentInChildren<TextMeshProUGUI>().text = hero.UnitName;
        _selectedHeroObject.SetActive(true);
       

    }
    public void ShowTargetedHero(BaseHero hero)
    {
        if (hero == null)
        {
            _targetHeroObject.SetActive(false);
            return;
        }
        _targetHeroObject.GetComponentInChildren<TextMeshProUGUI>().text = hero.UnitName;
        _targetHeroObject.SetActive(true);


    }

    public void ShowAbilities(BaseHero hero)
    {
        if (hero == null)
        {
            _attackButton.SetActive(false);
            return;
        }
        _attackButton.SetActive(true);
    }
}
