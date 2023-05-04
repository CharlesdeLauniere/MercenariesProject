using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace MercenariesProject
{
    public class CombatManager : MonoBehaviour
    {
        public Hero activeHero;

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
            activeHero = character.GetComponent<Hero>();
            activeHero.ApplyEffects();
        }

        private void Update()
        {
            //Some controls for targetting different characters in range and then attacking them. 
            if (InAttackMode)
            {
                //Debug.Log("ATKMODE");
                if (inRangeCharacters.Count > 0)
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

                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                    {
                        Debug.Log("SPACE");
                        AttackUnit();
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        //cancelActionEvent.Raise("Attack");
                        ResetAttackMode();
                    }
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    // cancelActionEvent.Raise("Attack");
                    ResetAttackMode();
                }
                
            }
        }

        //Cancel attack.
        private void ResetAttackMode()
        {
            if (inRangeCharacters?.Any() == true)
            {
                var focusedCharacter = inRangeCharacters[focusedCharIndex];
                focusedCharacter.SetTargeted(false);
                inRangeCharacters.Clear();
            }
            cancelActionEvent.Raise("Attack");
            InAttackMode = false;
        }

        //Attack targeted Hero.
        private void AttackUnit()
        {
            Debug.Log("AtkUnit");
            var focusedCharacter = inRangeCharacters[focusedCharIndex];
            focusedCharacter.SetTargeted(false);

            focusedCharacter.TakeDamage(activeHero.GetStat(Stats.Strength).statValue);
            activeHero.UpdateInitiative(Constants.AttackCost);
            inRangeCharacters.Clear();
            InAttackMode = false;
            focusedCharIndex = 0;
        }

        //Enter attack mode and get all in range characters.
        public void EnterAttackMode()
        {
            InAttackMode = true;
            var inRangeTiles = rangeFinder.GetTilesInRange(activeHero.activeTile, activeHero.GetStat(Stats.AttackRange).statValue, true);
            inRangeCharacters = inRangeTiles.Where(x => x.activeHero && x.activeHero.teamID != activeHero.teamID && x.activeHero.isAlive).Select(x => x.activeHero).ToList();

            //if (inRangeCharacters.Count > 0)
            //    inRangeCharacters[focusedCharIndex].SetTargeted(true);
            //else
            //{
            //    InAttackMode = false;
            //    OverlayTileColorManager.Instance.ClearTiles(null);
            //    cancelActionEvent.Raise("Attack");
            //}
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
            var tileToUse = focusedOnTile != null ? focusedOnTile.GetComponent<Tile>() : activeHero.activeTile;
            var attackColor = OverlayTileColorManager.Instance.AttackRangeColor;
            List<Tile> inAttackRangeTiles = rangeFinder.GetTilesInRange(tileToUse, activeHero.GetStat(Stats.AttackRange).statValue, true, true);
            OverlayTileColorManager.Instance.ColorTiles(attackColor, inAttackRangeTiles);
        }

        //Show all the tiles in move range based on player position. 
        public void DisplayMoveRange(GameObject focusedOnTile = null)
        {
            var tileToUse = focusedOnTile != null ? focusedOnTile.GetComponent<Tile>() : activeHero.activeTile;
            var moveColor = OverlayTileColorManager.Instance.MoveRangeColor;
            List<Tile> inMoveRangeTiles = rangeFinder.GetTilesInRange(tileToUse, activeHero.GetStat(Stats.MoveRange).statValue, true, true);
            OverlayTileColorManager.Instance.ColorTiles(moveColor, inMoveRangeTiles);
        }

    }
}
