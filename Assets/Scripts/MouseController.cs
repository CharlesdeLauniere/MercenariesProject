using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
namespace MercenariesProject
{



    public class MouseController : MonoBehaviour
    {
        public HeroSpawner heroSpawner;
        public Tile focusedOnTile;
    [SerializeField] private Vector3 previousMousePosition;
        public GameEventGameObject focusOnNewTile;


        public GameObject cursor;
        public float speed;

        //public Vector3 mousePosition;
        [SerializeField] Camera mainCamera;
        [SerializeField] LayerMask layerMask;
       // public PathFinder pathFinder;
        //public List<Tile> path;
       // [SerializeField] public float moveSpeed = 10;

        //public GameObject characterPrefab;
        //private CharacterInfo character;
        private void Start()
        {
            previousMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           // pathFinder = new PathFinder();
           // path = new List<Tile>();
        }

        void FixedUpdate()
        {
            Debug.Log("Ok");
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (previousMousePosition != Camera.main.ScreenToWorldPoint(Input.mousePosition))
            {
                Debug.Log("Ok2");
                Tile newFocusedOnTile;
                newFocusedOnTile = GetFocusedOnTile(mousePos);


                if (newFocusedOnTile)
                {
                    Debug.Log("Ok3");
                    if (focusedOnTile != newFocusedOnTile)
                    {
                        if (heroSpawner)
                        {
                            heroSpawner.focusedOnTile = newFocusedOnTile;
                        }
                        transform.position = newFocusedOnTile.transform.position;
                        focusedOnTile = newFocusedOnTile;

                        if (focusOnNewTile)
                            focusOnNewTile.Raise(newFocusedOnTile.gameObject);
                    }
                }
                previousMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        //void LateUpdate()
        //{
        //    Tile tempTile = null;
        //    RaycastHit? hit = GetFocusedOnTile();
        //    if (hit.HasValue)
        //    {
        //        //Debug.Log("GotAHit!");
        //        focusedTile = hit.Value.collider.gameObject.GetComponent<Tile>();
        //        float _y = cursor.transform.position.y;
        //        //cursor.transform.position = tile.transform.position;
        //        //cursor.transform.position = new Vector3(tile.transform.position.x,_y+1, tile.transform.position.z);
        //        //cursor.gameObject.GetComponent<MeshRenderer>().sortingOrder = tile.transform.GetComponent<MeshRenderer>().sortingOrder;
        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            if (GameManager.Instance.GameState != GameState.TurnBasedCombat) return;

        //            if (focusedTile.OccupiedUnit != null && TurnManager.Instance.currentState == TurnManager.TurnState.chosingTarget)
        //            {
        //                Debug.Log("moem0");
        //                if (focusedTile.OccupiedUnit.Faction != UnitManager.Instance.SelectedHero.Faction)
        //                {
        //                    var enemyHero = (BaseHero)focusedTile.OccupiedUnit;
        //                    UnitManager.Instance.SetTargetedHero(enemyHero);
        //                    MenuManager.Instance.ShowTargetedHero(enemyHero);
        //                    MenuManager.Instance.ShowAbilities(null);
        //                    TurnManager.Instance.SwitchBetweenTurnStates(TurnManager.TurnState.usingBaseAttack);
        //                }

        //            }
        //            if (focusedTile.OccupiedUnit == null && TurnManager.Instance.currentState == TurnManager.TurnState.movement && focusedTile != null)
        //            {
        //                Debug.Log("moem");
        //                tempTile = focusedTile;
        //                path = pathFinder.FindPath(UnitManager.Instance.SelectedHero.OccupiedTile, tempTile);


        //                //tile.SetUnit(UnitManager.Instance.SelectedHero);
        //            }

        //        }
        //    }

        //    if (UnitManager.Instance.SelectedHero.OccupiedTile == tempTile)
        //    {
        //        Debug.Log("moem2");
        //        TurnManager.Instance.SwitchBetweenTurnStates(TurnManager.TurnState.selectingAttack);
        //    }
        //    if (path.Count > 0)
        //    {
        //        MoveAlongPath();
        //    }
        //}

        //private RaycastHit? GetFocusedOnTile(Vector3 mousePos)
        //{
        //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        //    {
        //        transform.position = raycastHit.point;
        //        return raycastHit;

        //    }

        //    //Debug.Log("Casting");

        //    //Vector3 mousePos = Input.mousePosition;//Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    //mousePosition = mousePos;
        //    ////Vector2 mousePos3D = new Vector3(mousePos.x,mousePos.y, mousePos.z);

        //    //RaycastHit[] hits = Physics.RaycastAll(mousePos, Vector3.zero);

        //    //if (hits.Length > 0)
        //    //{
        //    //    Debug.Log("Raycasting!!!");
        //    //    return hits.OrderByDescending(i => i.collider.transform.position.z).First();

        //    //}




        //    return null;
        //}
        public Tile GetFocusedOnTile(Vector3 mousePos)
        {
            Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.z);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);
            RaycastHit[] hits2 = Physics.RaycastAll(mousePos2d, Vector2.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                var renderer = hit.transform.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    return hit.collider.gameObject.GetComponent<Tile>();
                }
            }
            return null;
        }
        //public void MoveAlongPath()
        //{
        //    Debug.Log("Moving");
        //    UnitManager.Instance.SelectedHero.transform.position = 
        //        Vector3.MoveTowards(UnitManager.Instance.SelectedHero.transform.position,
        //        path[0].transform.position, moveSpeed * Time.deltaTime);
        //    UnitManager.Instance.SelectedHero.transform.position = new Vector3(UnitManager.Instance.SelectedHero.transform.position.x,
        //        0.2f, UnitManager.Instance.SelectedHero.transform.position.z);

        //    if ((Mathf.Abs(UnitManager.Instance.SelectedHero.transform.position.x - path[0].transform.position.x) +
        //        Mathf.Abs(UnitManager.Instance.SelectedHero.transform.position.z - path[0].transform.position.z)) < 0.01f)
        //    {
        //        Debug.Log("Moving2");
        //        path[0].SetUnit(UnitManager.Instance.SelectedHero);
        //        path.RemoveAt(0);
        //        if (path.Count == 0) TurnManager.Instance.SwitchBetweenTurnStates(TurnManager.TurnState.selectingAttack);
        //    }
        //}

    }

}