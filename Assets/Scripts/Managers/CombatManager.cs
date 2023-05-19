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
            if (InAttackMode)
            {
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
                        AttackUnit();
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        ResetAttackMode();
                    }
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
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

        //Attaque l'héro ciblé
        private void AttackUnit()
        {
            var focusedCharacter = inRangeCharacters[focusedCharIndex];
            focusedCharacter.SetTargeted(false);

            focusedCharacter.TakeDamage(activeHero.GetStat(Stats.Strength).statValue);
            activeHero.UpdateInitiative(Constants.AttackCost);
            inRangeCharacters.Clear();
            InAttackMode = false;
            focusedCharIndex = 0;
        }

        //Entre dans le mode attaque et cherche les ennemies dans la portée
        public void EnterAttackMode()
        {
            InAttackMode = true;
            var inRangeTiles = rangeFinder.GetTilesInRange(activeHero.activeTile, activeHero.GetStat(Stats.AttackRange).statValue, true);
            inRangeCharacters = inRangeTiles.Where(x => x.activeHero && x.activeHero.teamID != activeHero.teamID && x.activeHero.isAlive).Select(x => x.activeHero).ToList();

        }

        //Focus sur un personnage
        private void FocusNewCharacter(int newIndex)
        {
            focusedCharIndex = newIndex;
            inRangeCharacters[newIndex].SetTargeted(true);
        }

        //Montre le portée d'attaque
        public void DisplayAttackRange(GameObject focusedOnTile = null)
        {
            var tileToUse = focusedOnTile != null ? focusedOnTile.GetComponent<Tile>() : activeHero.activeTile;
            var attackColor = OverlayTileColorManager.Instance.AttackRangeColor;
            List<Tile> inAttackRangeTiles = rangeFinder.GetTilesInRange(tileToUse, activeHero.GetStat(Stats.AttackRange).statValue, true, true);
            OverlayTileColorManager.Instance.ColorTiles(attackColor, inAttackRangeTiles);
        }

        //Montre la portée de mouvement
        public void DisplayMoveRange(GameObject focusedOnTile = null)
        {
            var tileToUse = focusedOnTile != null ? focusedOnTile.GetComponent<Tile>() : activeHero.activeTile;
            var moveColor = OverlayTileColorManager.Instance.MoveRangeColor;
            List<Tile> inMoveRangeTiles = rangeFinder.GetTilesInRange(tileToUse, activeHero.GetStat(Stats.MoveRange).statValue, true, true);
            OverlayTileColorManager.Instance.ColorTiles(moveColor, inMoveRangeTiles);
        }

    }
}
