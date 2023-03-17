using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MercenariesProject
{
    //using static ArrowTranslator;

    public class MovementManager : MonoBehaviour
    {
        public float speed;
        public Hero activeHero;
        public bool enableAutoMove;
        public bool showAttackRange;
        public bool moveThroughAllies = true;

        public GameEvent endTurnEvent;
        public GameEventGameObject displayAttackRange;
        public GameEventString cancelActionEvent;

        private PathFinder pathFinder;
        private RangeFinder rangeFinder;
        //private ArrowTranslator arrowTranslator;
        private List<Tile> path = new List<Tile>();
        private List<Tile> inRangeTiles = new List<Tile>();
        private List<Tile> inAttackRangeTiles = new List<Tile>();
        private Tile focusedTile;
        private bool movementModeEnabled = false;
        private bool isMoving = false;

        private void Start()
        {
            pathFinder = new PathFinder();
            rangeFinder = new RangeFinder();
            //arrowTranslator = new ArrowTranslator();
        }

        // Update is called once per frame
        void Update()
        {
            //Is this the best way? Not sure
            if (activeHero && !activeHero.isAlive)
            {
                ResetMovementManager();
            }

            if (focusedTile)
            {
                if (inRangeTiles.Contains(focusedTile) && movementModeEnabled && !isMoving && focusedTile.isWalkable)
                {
                    path = pathFinder.FindPath(activeHero.activeTile, focusedTile, inRangeTiles, false, moveThroughAllies);

                    //foreach (var item in inRangeTiles)
                    //{
                    //    item.SetArrowSprite(ArrowDirection.None);
                    //}

                    for (int i = 0; i < path.Count; i++)
                    {
                        var previousTile = i > 0 ? path[i - 1] : activeHero.activeTile;
                        var futureTile = i < path.Count - 1 ? path[i + 1] : null;

                        //var arrowDir = arrowTranslator.TranslateDirection(previousTile, path[i], futureTile);
                        //path[i].SetArrowSprite(arrowDir);
                    }
                }
            }

            if (Input.GetMouseButtonDown(0) && movementModeEnabled && path.Count > 0)
            {
                isMoving = true;
                OverlayController.Instance.ClearTiles(null);
                activeHero.UpdateInitiative(Constants.MoveCost);
            }

            if (path.Count > 0 && isMoving)
            {
                MoveAlongPath();
            }

            //Cancel movement
            if (Input.GetKeyDown(KeyCode.Escape) && movementModeEnabled)
            {
                cancelActionEvent.Raise("Move");
                ResetMovementManager();
            }
        }

        //Resets movement mode when movement has Finished or is Cancelled. 
        private void ResetMovementManager()
        {
            movementModeEnabled = false;
            isMoving = false;
            OverlayController.Instance.ClearTiles(null);
            activeHero.CharacterMoved();
        }

        //Move along a set path.
        private void MoveAlongPath()
        {
            var step = speed * Time.deltaTime;

            var zIndex = path[0].transform.position.z;
            activeHero.transform.position = Vector3.MoveTowards(activeHero.transform.position, path[0].transform.position, step);
            //activeCharacter.transform.position = new Vector3(activeCharacter.transform.position.x, activeCharacter.transform.position.y, activeCharacter.transform.position.z);

            if (Vector3.Distance(activeHero.transform.position, path[0].transform.position) < 0.0001f)
            {
                //last tile
                if (path.Count == 1)
                    PositionCharacterOnTile(activeHero, path[0]);

                path.RemoveAt(0);
            }

            if (path.Count == 0)
            {
                ResetMovementManager();

                if (enableAutoMove)
                {
                    if (endTurnEvent)
                        endTurnEvent.Raise();
                    else
                        SetActiveCharacter(activeHero.gameObject);
                }
            }
        }

        //Get all tiles in movement range. 
        private void GetInRangeTiles()
        {
            var moveColor = OverlayController.Instance.MoveRangeColor;
            if (activeHero && activeHero.activeTile)
            {
                inRangeTiles = rangeFinder.GetTilesInRange(activeHero.activeTile, activeHero.GetStat(Stats.MoveRange).statValue, false, moveThroughAllies);
                OverlayController.Instance.ColorTiles(moveColor, inRangeTiles);
            }
        }

        //Link character to tile once movement has finished
        public void PositionCharacterOnTile(Hero character, Tile tile)
        {
            if (tile != null)
            {
                character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
                character.LinkCharacterToTile(tile);
            }
        }

        //Movement event receiver for the AI
        public void MoveCharacterCommand(List<GameObject> pathToFollow)
        {
            if (activeHero)
            {
                isMoving = true;
                activeHero.UpdateInitiative(Constants.MoveCost);

                if (pathToFollow.Count > 0)
                    path = pathToFollow.Select(x => x.GetComponent<Tile>()).ToList();
            }
        }

        //Moused over new tile and display the attack range. 
        public void FocusedOnNewTile(GameObject focusedOnTile)
        {
            if (!isMoving)
                focusedTile = focusedOnTile.GetComponent<Tile>();

            if (movementModeEnabled && inRangeTiles.Where(x => x.gridLocation == focusedTile.gridLocation).Any() && !isMoving && showAttackRange && displayAttackRange)
                displayAttackRange.Raise(focusedOnTile);
        }

        //Show all the tiles in attack range based on mouse position. 
        public void ShowAttackRangeTiles(GameObject focusedOnTile)
        {
            var attackColor = OverlayController.Instance.AttackRangeColor;
            inAttackRangeTiles = rangeFinder.GetTilesInRange(focusedOnTile.GetComponent<Tile>(), activeHero.GetStat(Stats.AttackRange).statValue, true, moveThroughAllies);

            OverlayController.Instance.ColorTiles(attackColor, inAttackRangeTiles);
        }

        //Set new active character
        public void SetActiveCharacter(GameObject character)
        {
            activeHero = character.GetComponent<Hero>();

            if (enableAutoMove && activeHero.isAlive)
                StartCoroutine(DelayedMovementmode());
        }

        //Wait until next loop to avoid possible race condition. 
        IEnumerator DelayedMovementmode()
        {
            yield return new WaitForFixedUpdate();
            EnterMovementMode();
        }

        //Set a character to a tile when it spawns. 
        public void SpawnCharacter(GameObject newCharacter)
        {
            PositionCharacterOnTile(newCharacter.GetComponent<Hero>(), focusedTile);
        }

        //Enter movement mode on button click.
        public void EnterMovementMode()
        {
            GetInRangeTiles();
            movementModeEnabled = true;
        }
    }
}
