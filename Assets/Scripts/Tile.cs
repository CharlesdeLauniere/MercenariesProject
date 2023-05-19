using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace MercenariesProject
{


    public class Tile : MonoBehaviour
    {
        public int G, H;
        public int F { get { return G + H; } }
        public Tile previous;
        public bool isWalkable = true;
        public Vector2Int gridLocation;
        public List<Sprite> arrows;
        public TileData tileData;
        public Hero activeHero;


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

        public void HideTile()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }




        public virtual void Init(int x, int z)
        {

        }




        public void SetUnit(Hero hero)
        {
            if (hero.activeTile != null) hero.activeTile.activeHero = null;
           
            hero.transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
            activeHero = hero;
            hero.activeTile = this;
        }
    }




}