using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace MercenariesProject
{


    public class Tile : MonoBehaviour
    {
        //public string TileName;
        //public Color TextColor, TextBoxColor;
        //[SerializeField] public bool _isWalkable, _unitInRange;
        //[SerializeField] protected Renderer _renderer;
        //[SerializeField] protected Color _baseColour, _offsetColour, _highlightColour;
        public int G, H;
        public int F { get { return G + H; } }
        public Tile previous;
        public bool isWalkable = true;
        public Vector2Int gridLocation;
        //public Vector3Int grid3DLocation { get { return new Vector3Int(gridLocation.x, 0, gridLocation.y); } }
        public List<Sprite> arrows;
        public TileData tileData;
        public Hero activeHero;





        // public BaseHero OccupiedUnit;

        //public bool Walkable => isWalkable && OccupiedUnit == null;

        public void Start()
        {
            if (this.tileData.type == TileTypes.NonTraversable) isWalkable = false;
        }
        public enum TileColors
        {
            MovementColor,
            AttackRangeColor,
            AttackColor
        }

        public void ShowTile(Color color)
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }

        //Remove the color from a tile.
        public void HideTile()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            //SetArrowSprite(ArrowDirection.None);
        }

        //public void Init(bool isOffset)
        //{
        //    _renderer.material.color = isOffset ? _offsetColour : _baseColour;
        //}


        public virtual void Init(int x, int z)
        {

        }
        //private void OnMouseEnter()
        //{
        //    _renderer.material.color = _highlightColour;
        //    MenuManager.Instance.ShowTileInfo(this);
        //}
        //private void OnMouseExit()
        //{
        //    var isOffset = ((transform.position.x + transform.position.z) % 2 != 0);
        //    if (isOffset == false) _renderer.material.color = _baseColour;
        //    if (isOffset == true) _renderer.material.color = _offsetColour;

        //    MenuManager.Instance.ShowTileInfo(null);
        //}



        public void SetUnit(Hero hero)
        {
            if (hero.activeTile != null) hero.activeTile.activeHero = null;
           
            hero.transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
            activeHero = hero;
            hero.activeTile = this;
        }
    }




}