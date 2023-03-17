using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace MercenariesProject
{
    public class CombatManager : MonoBehaviour
    {
        public Hero activeCharacter;

        public GameEvent clearTiles;
        public GameEventString cancelActionEvent;

        private bool InAttackMode = false;
        private int focusedCharIndex = 0;
        private List<Hero> inRangeCharacters;
        private RangeFinder rangeFinder;

        private void Start()
        {
            rangeFinder = new RangeFinder();
            inRangeCharacters = new List<Hero>();
        }

        public void SetActiveCharacter(GameObject character)
        {
            activeCharacter = character.GetComponent<Hero>();
        }

        private void Update()
        {
            //Some controls for targetting different characters in range and then attacking them. 
            if (InAttackMode)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    inRangeCharacters[focusedCharIndex].SetTargeted(false);
                    focusedCharIndex++;

                    if (focusedCharIndex >= inRangeCharacters.Count)
                    {
                        FocusNewCharacter(0);
                    }
                    else
                    {
                        FocusNewCharacter(focusedCharIndex);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    inRangeCharacters[focusedCharIndex].SetTargeted(false);
                    focusedCharIndex--;

                    if (focusedCharIndex < 0)
                    {
                        FocusNewCharacter(inRangeCharacters.Count - 1);
                    }
                    else
                    {
                        FocusNewCharacter(focusedCharIndex);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AttackUnit();
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                    ResetAttackMode();
            }
        }

        //Cancel attack.
        private void ResetAttackMode()
        {
            var focusedCharacter = inRangeCharacters[focusedCharIndex];

            focusedCharacter.SetTargeted(false);

            cancelActionEvent.Raise("Attack");
            inRangeCharacters.Clear();
            InAttackMode = false;
        }

        //Attack targeted Hero.
        private void AttackUnit()
        {
            var focusedCharacter = inRangeCharacters[focusedCharIndex];
            focusedCharacter.SetTargeted(false);

            focusedCharacter.TakeDamage(activeCharacter.GetStat(Stats.Strenght).statValue);
            activeCharacter.UpdateInitiative(Constants.AttackCost);
            inRangeCharacters.Clear();
            InAttackMode = false;
            focusedCharIndex = 0;
        }

        //Enter attack mode and get all in range characters.
        public void EnterAttackMode()
        {
            InAttackMode = true;
            var inRangeTiles = rangeFinder.GetTilesInRange(activeCharacter.activeTile, activeCharacter.GetStat(Stats.AttackRange).statValue, true);
            inRangeCharacters = inRangeTiles.Where(x => x.activeHero && x.activeHero.teamID != activeCharacter.teamID && x.activeHero.isAlive).Select(x => x.activeHero).ToList();

            if (inRangeCharacters.Count > 0)
                inRangeCharacters[focusedCharIndex].SetTargeted(true);
            else
            {
                InAttackMode = false;
            }
        }

        //Focus on a character.
        private void FocusNewCharacter(int newIndex)
        {
            focusedCharIndex = newIndex;
            inRangeCharacters[newIndex].SetTargeted(true);
        }

        //Show all the tiles in attack range based on mouse position. 
        public void DisplayAttackRange(GameObject focusedOnTile = null)
        {
            var tileToUse = focusedOnTile != null ? focusedOnTile.GetComponent<Tile>() : activeCharacter.activeTile;
            var attackColor = OverlayController.Instance.AttackRangeColor;
            List<Tile> inAttackRangeTiles = rangeFinder.GetTilesInRange(tileToUse, activeCharacter.GetStat(Stats.AttackRange).statValue, true, true);
            OverlayController.Instance.ColorTiles(attackColor, inAttackRangeTiles);
        }
    }
}
