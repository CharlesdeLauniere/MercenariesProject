using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace MercenariesProject
{

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
        [SerializeField] private List<Tile> path = new List<Tile>();
        [SerializeField] private List<Tile> inRangeTiles = new List<Tile>();
        [SerializeField] private List<Tile> inAttackRangeTiles = new List<Tile>();
        [SerializeField] private Tile focusedTile;
        private bool movementModeEnabled = false;
        private bool isMoving = false;

        private void Start()
        {
            pathFinder = new PathFinder();
            rangeFinder = new RangeFinder();
        }

        void Update()
        {
            if (activeHero && !activeHero.isAlive)
            {
                ResetMovementManager();
            }

            if (focusedTile)
            {
                if (inRangeTiles.Contains(focusedTile) && movementModeEnabled && !isMoving && focusedTile.isWalkable)
                {
                    path = pathFinder.FindPath(activeHero.activeTile, focusedTile, inRangeTiles, false, moveThroughAllies);


                    for (int i = 0; i < path.Count; i++)
                    {
                        var previousTile = i > 0 ? path[i - 1] : activeHero.activeTile;
                        var futureTile = i < path.Count - 1 ? path[i + 1] : null;

                    }
                }
            }

            if (Input.GetMouseButtonDown(0) && movementModeEnabled && path.Count > 0)
            {
                isMoving = true;
                OverlayTileColorManager.Instance.ClearTiles(null);
                activeHero.UpdateInitiative(Constants.MoveCost);
            }

            if (path.Count > 0 && isMoving)
            {
                MoveAlongPath();
            }

            //Annule le mouvement
            if (Input.GetKeyDown(KeyCode.Escape) && movementModeEnabled)
            {
                cancelActionEvent.Raise("Move");
                ResetMovementManager();
            }
        }

        //R�initialise le manager lorsqu'il y a une annulation ou un nouveau tour
        public void ResetMovementManager()
        {
            movementModeEnabled = false;
            isMoving = false;
            OverlayTileColorManager.Instance.ClearTiles(null);
            activeHero.CharacterMoved();
        }

        //Fait le d�placement selon un trajet pr�d�fini
        private void MoveAlongPath()
        {
            activeHero.transform.position = Vector3.MoveTowards(activeHero.transform.position,
                path[0].transform.position, speed * Time.deltaTime);

            Vector3 target = new Vector3 (path[0].transform.position.x, activeHero.transform.position.y, path[0].transform.position.z);
            activeHero.transform.GetChild(1).LookAt(target);
            activeHero.transform.position = new Vector3(activeHero.transform.position.x, 0.2f, activeHero.transform.position.z);

            if ((Mathf.Abs(activeHero.transform.position.x - path[0].transform.position.x) +
                Mathf.Abs(activeHero.transform.position.z - path[0].transform.position.z)) < 0.01f)
            {
                //dernier d�placement
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


        //Cherche tous les tuiles dans la zone de d�placement
        private void GetInRangeTiles()
        {
            var moveColor = OverlayTileColorManager.Instance.MoveRangeColor;
            if (activeHero && activeHero.activeTile)
            {
                inRangeTiles = rangeFinder.GetTilesInRange(activeHero.activeTile, activeHero.GetStat(Stats.MoveRange).statValue, false, moveThroughAllies);
                OverlayTileColorManager.Instance.ColorTiles(moveColor, inRangeTiles);
            }
        }

        //Lie le personnage � sa tuile finale
        public void PositionCharacterOnTile(Hero character, Tile tile)
        {
            if (tile != null)
            {
                character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
                character.LinkCharacterToTile(tile);
            }
        }



        //montre la port�e de l'attaque de base lorsque le joueur met son curseur sur une tuile de d�placement
        public void FocusedOnNewTile(GameObject focusedOnTile)
        {
            if (!isMoving)
                focusedTile = focusedOnTile.GetComponent<Tile>();

            if (movementModeEnabled && inRangeTiles.Where(x => x.gridLocation == focusedTile.gridLocation).Any() && !isMoving && showAttackRange && displayAttackRange)
                displayAttackRange.Raise(focusedOnTile);
        }

        //Montre la port�e de l'attaque du personnage en bleu
        public void ShowAttackRangeTiles(GameObject focusedOnTile)
        {
            var attackColor = OverlayTileColorManager.Instance.AttackRangeColor;
            inAttackRangeTiles = rangeFinder.GetTilesInRange(focusedOnTile.GetComponent<Tile>(), activeHero.GetStat(Stats.AttackRange).statValue, true, moveThroughAllies);

            OverlayTileColorManager.Instance.ColorTiles(attackColor, inAttackRangeTiles);
        }

        //Met une nouveau personnage comme actif
        public void SetActiveCharacter(GameObject character)
        {
            activeHero = character.GetComponent<Hero>();

            if (enableAutoMove && activeHero.isAlive)
                StartCoroutine(DelayedMovementmode());
        }

        //Attend pour �viter probl�mes
        IEnumerator DelayedMovementmode()
        {
            yield return new WaitForFixedUpdate();
            EnterMovementMode();
        }

        //Lie un personnage � sa tuile d'initialisation
        public void SpawnCharacter(GameObject newCharacter)
        {
            PositionCharacterOnTile(newCharacter.GetComponent<Hero>(), focusedTile);
        }

        //Mode mouvement
        public void EnterMovementMode()
        {
            GetInRangeTiles();
            movementModeEnabled = true;
        }
        public void ResetFocusedTile()
        {
            focusedTile = null;
        }
    }
}
