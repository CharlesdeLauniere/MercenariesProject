using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MercenariesProject
{
   

    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        [SerializeField] private GameObject _selectedHeroObject, _tileObject, _tileUnitObject, _targetedHeroObject, _attackButton, _moveButton, _abilityButton;
        private void Awake()
        {
            Instance = this;
        }

        public void ShowTileInfo(Tile tile)
        {
            if (tile == null)
            {
                _tileObject.SetActive(false);
                _tileUnitObject.SetActive(false);
                return;
            }
            //_tileObject.GetComponentInChildren<TextMeshProUGUI>().text = tile.TileName;
            //_tileObject.GetComponentInChildren<Image>().color = tile.TextBoxColor;
            //_tileObject.GetComponentInChildren<TextMeshProUGUI>().color = tile.TextColor;
            //_tileObject.SetActive(true);

            if (tile.OccupiedUnit)
            {
                _tileUnitObject.GetComponentInChildren<TextMeshProUGUI>().text = tile.OccupiedUnit.UnitName;
                if (tile.OccupiedUnit.Faction == Faction.Red) _tileUnitObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                if (tile.OccupiedUnit.Faction == Faction.Blue) _tileUnitObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
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
            if (hero.Faction == Faction.Red) _selectedHeroObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            if (hero.Faction == Faction.Blue) _selectedHeroObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
            _selectedHeroObject.SetActive(true);


        }
        public void ShowTargetedHero(BaseHero hero)
        {
            if (hero == null)
            {
                _targetedHeroObject.SetActive(false);
                return;
            }
            _targetedHeroObject.GetComponentInChildren<TextMeshProUGUI>().text = hero.UnitName;
            if (hero.Faction == Faction.Red) _targetedHeroObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            if (hero.Faction == Faction.Blue) _targetedHeroObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
            _targetedHeroObject.SetActive(true);


        }

        public void ShowAbilities(BaseHero hero)
        {
            if (hero == null)
            {
                _attackButton.SetActive(false);
                _moveButton.SetActive(false);
                return;
            }
            _attackButton.SetActive(true);
            _moveButton.SetActive(true);
        }
    }

}