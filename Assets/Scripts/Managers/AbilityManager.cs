using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static MercenariesProject.Ability;
using static MercenariesProject.EffectManager;

namespace MercenariesProject
{
    public class AbilityManager : MonoBehaviour
    {
        public GameEventString disableAbility;
        public GameEvent cancelAbilityMode;
        public RangeFinder eventRangeController;

        [SerializeField] private Hero activeHero;
        [SerializeField] private List<Tile> abilityRangeTiles;
        [SerializeField] private List<Tile> abilityAffectedTiles;
        public EffectManager effectManager;
        private ShapeParser shapeParser;
        [SerializeField] private Ability ability;

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
            if (activeHero != null) { 
            if (Input.GetKeyDown(KeyCode.Return) && activeHero.heroClass.abilities != null )
            {
                Debug.Log("cast");
                CastAbility();
            }
            if (Input.GetKeyDown(KeyCode.Escape) &&  ability != null)
                {
                    CancelEventMode();
                }
            }
        }

        //Cast an ability
        private void CastAbility()
        {
            //Laurent was here
            Animator anim = activeHero.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                anim.Play("Base Layer.1");

            }

            var inRangeCharacters = new List<Hero>();

            //get in range characters
            foreach (var tile in abilityAffectedTiles)
            {

                var targetCharacter = tile.activeHero;
                if (targetCharacter != null && CheckAbilityTargets(ability.abilityType, targetCharacter) && targetCharacter.isAlive)
                {
                    Debug.Log("targetaquired");
                    inRangeCharacters.Add(targetCharacter);
                }
            }
            foreach (var Hero in inRangeCharacters)
            {
                Debug.Log(Hero.heroClass.ClassName);
            }
            //attach effects
            foreach (var character in inRangeCharacters)
            {
                foreach (var effect in ability.effects)
                {
                    Debug.Log("effecting");
                    character.AttachEffect(effect);
                    if (effect.Duration == 0)
                        character.ApplySingleEffects(effect.GetStatKey());
                }

                //apply value
                switch (ability.abilityType)
                {
                    case AbilityTypes.Ally:
                        character.HealEntity(ability.value);
                        break;
                    case AbilityTypes.Enemy:
                        character.TakeDamage(ability.value);
                        Debug.Log("Ability used");
                        break;
                    case AbilityTypes.All:
                        character.TakeDamage(ability.value);
                        break;
                    default:
                        break;
                }
                
            }
            //Brandon Here
            effectManager.findAbility(ability.Name, new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            //turnsSinceUsed = 0;
            activeHero.UpdateInitiative(Constants.AbilityCost);
            activeHero.UpdateMana(ability.cost);
            activeHero.UpdateCharacterUI();
            disableAbility.Raise(ability.Name);
            ability = null;
            OverlayTileColorManager.Instance.ClearTiles(null);

            
            

        }
        
        //The event receiver for Casting an Ability. 
        public void CastAbilityCommand(EventCommand abilityCommand)
        {
            if (abilityCommand is CastAbilityCommand)
            {
                CastAbilityCommand command = (CastAbilityCommand)abilityCommand;
                CastAbilityParams castAbilityParams = command.StronglyTypedCommandParam();
                abilityAffectedTiles = castAbilityParams.affectedTiles;
                ability = castAbilityParams.ability;
                CastAbility();
            }
        }

        //Check if Abilities are targeting the right entities.
        private bool CheckAbilityTargets(AbilityTypes abilityType, Hero characterTarget)
        {
            if (abilityType == AbilityTypes.Enemy)
            {
                return characterTarget.teamID != activeHero.teamID;
            }
            else if (abilityType == AbilityTypes.Ally)
            {
                return characterTarget.teamID == activeHero.teamID;
            }

            return true;
        }

        public void SetActiveCharacter(GameObject activeChar)
        {
            activeHero = activeChar.GetComponent<Hero>();
        }

        //Set the position the abilities origin.
        public void SetAbilityPosition(GameObject focusedOnTile)
        {
            var map = GridManager.Instance.tileMap;
            Tile tilePosition = focusedOnTile.GetComponent<Tile>();
            if (ability != null)
            {
                foreach (var tile in abilityAffectedTiles)
                {
                    if (map.ContainsKey(tile.gridLocation))
                    {
                        var gridTile = map[tile.gridLocation];
                        gridTile.GetComponent<SpriteRenderer>().color = abilityRangeTiles.Contains<Tile>(gridTile)
                            ? OverlayTileColorManager.Instance.MoveRangeColor
                            : new Color(0, 0, 0, 0);
                    }
                }

                if (abilityRangeTiles.Contains(map[tilePosition.gridLocation]))
                {
                    abilityAffectedTiles = shapeParser.GetAbilityTileLocations(tilePosition, ability.abilityShape, activeHero.activeTile.gridLocation);

                    if (ability.includeOrigin)
                        abilityAffectedTiles.Add(tilePosition);

                    OverlayTileColorManager.Instance.ColorTiles(OverlayTileColorManager.Instance.AttackRangeColor, abilityAffectedTiles);
                }
            }
        }

        //Set ability casting mode. 
        public void AbilityModeEvent(string abilityName)
        {
            OverlayTileColorManager.Instance.ClearTiles(null);

            var ability = activeHero.heroClass.abilities.Find(x => x.Name == abilityName);
            if (ability.cost <= activeHero.statsContainer.CurrentMana.statValue)
            {
                abilityRangeTiles = eventRangeController.GetTilesInRange(activeHero.activeTile, ability.range, true);

                OverlayTileColorManager.Instance.ColorTiles(OverlayTileColorManager.Instance.MoveRangeColor, abilityRangeTiles);

                this.ability = ability;
            }
        }

        //Cancel ability casting mode. 
        public void CancelEventMode()
        {
            OverlayTileColorManager.Instance.ClearTiles(null);
            ability = null;
        }
    }
}
