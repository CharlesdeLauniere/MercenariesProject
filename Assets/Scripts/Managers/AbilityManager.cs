using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static MercenariesProject.Ability;

namespace MercenariesProject
{
    public class AbilityManager : MonoBehaviour
    {
        public GameEventString disableAbility;
        public RangeFinder eventRangeController;

        private Hero activeCharacter;
        private List<Tile> abilityRangeTiles;
        private List<Tile> abilityAffectedTiles;
        private ShapeParser shapeParser;
        private AbilityContainer abilityContainer;

        // Start is called before the first frame update
        void Start()
        {
            eventRangeController = new RangeFinder();
            shapeParser = new ShapeParser();
            abilityRangeTiles = new List<Tile>();
            abilityAffectedTiles = new List<Tile>();
        }

        private void Update()
        {
            if (abilityContainer != null && Input.GetMouseButtonDown(0))
            {
                CastAbility();
            }
        }

        //Cast an ability
        private void CastAbility()
        {
            var inRangeCharacters = new List<Hero>();

            //get in range characters
            foreach (var tile in abilityAffectedTiles)
            {
                var targetCharacter = tile.activeHero;
                if (targetCharacter != null && CheckAbilityTargets(abilityContainer.ability.abilityType, targetCharacter) && targetCharacter.isAlive)
                {
                    inRangeCharacters.Add(targetCharacter);
                }
            }

            //attach effects
            foreach (var character in inRangeCharacters)
            {
                foreach (var effect in abilityContainer.ability.effects)
                {
                    character.AttachEffect(effect);
                    if (effect.Duration == 0)
                        character.ApplySingleEffects(effect.GetStatKey());
                }

                //apply value
                switch (abilityContainer.ability.abilityType)
                {
                    case AbilityTypes.Ally:
                        character.HealEntity(abilityContainer.ability.value);
                        break;
                    case AbilityTypes.Enemy:
                        character.TakeDamage(abilityContainer.ability.value);
                        break;
                    case AbilityTypes.All:
                        character.TakeDamage(abilityContainer.ability.value);
                        break;
                    default:
                        break;
                }
            }



            abilityContainer.turnsSinceUsed = 0;
            activeCharacter.UpdateInitiative(Constants.AbilityCost);
            activeCharacter.UpdateMana(abilityContainer.ability.cost);
            disableAbility.Raise(abilityContainer.ability.Name);
            abilityContainer = null;
            OverlayController.Instance.ClearTiles(null);
        }

        //The event receiver for Casting an Ability. 
        public void CastAbilityCommand(EventCommand abilityCommand)
        {
            if (abilityCommand is CastAbilityCommand)
            {
                CastAbilityCommand command = (CastAbilityCommand)abilityCommand;
                CastAbilityParams castAbilityParams = command.StronglyTypedCommandParam();
                abilityAffectedTiles = castAbilityParams.affectedTiles;
                abilityContainer = castAbilityParams.abilityContainer;
                CastAbility();
            }
        }

        //Check if Abilities are targeting the right entities.
        private bool CheckAbilityTargets(AbilityTypes abilityType, Hero characterTarget)
        {
            if (abilityType == AbilityTypes.Enemy)
            {
                return characterTarget.teamID != activeCharacter.teamID;
            }
            else if (abilityType == AbilityTypes.Ally)
            {
                return characterTarget.teamID == activeCharacter.teamID;
            }

            return true;
        }

        public void SetActiveCharacter(GameObject activeChar)
        {
            activeCharacter = activeChar.GetComponent<Hero>();
        }

        //Set the position the abilities origin.
        public void SetAbilityPosition(GameObject focusedOnTile)
        {
            var map = GridManager.Instance.tileMap;
            Tile tilePosition = focusedOnTile.GetComponent<Tile>();
            if (abilityContainer != null)
            {
                foreach (var tile in abilityAffectedTiles)
                {
                    if (map.ContainsKey(tile.gridLocation))
                    {
                        var gridTile = map[tile.gridLocation];
                        gridTile.GetComponent<SpriteRenderer>().color = abilityRangeTiles.Contains<Tile>(gridTile)
                            ? OverlayController.Instance.MoveRangeColor
                            : new Color(0, 0, 0, 0);
                    }
                }

                if (abilityRangeTiles.Contains(map[tilePosition.gridLocation]))
                {
                    abilityAffectedTiles = shapeParser.GetAbilityTileLocations(tilePosition, abilityContainer.ability.abilityShape, activeCharacter.activeTile.gridLocation);

                    if (abilityContainer.ability.includeOrigin)
                        abilityAffectedTiles.Add(tilePosition);

                    OverlayController.Instance.ColorTiles(OverlayController.Instance.AttackRangeColor, abilityAffectedTiles);
                }
            }
        }

        //Set ability casting mode. 
        public void AbilityModeEvent(string abilityName)
        {
            OverlayController.Instance.ClearTiles(null);

            var abilityContainer = activeCharacter.abilitiesForUse.Find(x => x.ability.Name == abilityName);
            if (abilityContainer.ability.cost <= activeCharacter.statsContainer.CurrentMana.statValue)
            {
                abilityRangeTiles = eventRangeController.GetTilesInRange(activeCharacter.activeTile, abilityContainer.ability.range, true);

                OverlayController.Instance.ColorTiles(OverlayController.Instance.MoveRangeColor, abilityRangeTiles);

                this.abilityContainer = abilityContainer;
            }
        }

        //Cancel ability casting mode. 
        public void CancelEventMode()
        {
            OverlayController.Instance.ClearTiles(null);
            abilityContainer = null;
        }
    }
}
