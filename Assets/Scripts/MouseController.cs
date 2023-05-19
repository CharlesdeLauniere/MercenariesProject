using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
namespace MercenariesProject
{



    public class MouseController : MonoBehaviour
    {
        public Tile focusedOnTile;
        [SerializeField] private Vector3 previousMousePosition;
        public GameEventGameObject focusOnNewTile;


        [SerializeField] Camera mainCamera;
        [SerializeField] LayerMask layerMask;


        private void Start()
        {
            previousMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }

        void FixedUpdate()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (previousMousePosition != Camera.main.ScreenToWorldPoint(Input.mousePosition))
            {
                Tile newFocusedOnTile;
                newFocusedOnTile = GetFocusedOnTile(mousePos);
                if (newFocusedOnTile)
                {
                    if (focusedOnTile != newFocusedOnTile)
                    {
                       
                        transform.position = newFocusedOnTile.transform.position;
                        focusedOnTile = newFocusedOnTile;

                        if (focusOnNewTile)
                            focusOnNewTile.Raise(newFocusedOnTile.gameObject);
                    }
                }

                previousMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

        }
       
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
        

    }

}