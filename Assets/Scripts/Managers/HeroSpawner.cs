using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MercenariesProject
{
    public class HeroSpawner : MonoBehaviour
    {
        public List<HeroManager> characters;
        //public List<SpawnTileContainer> spawnZones;
        public Tile focusedOnTile;

        public GameEventGameObject spawnCharacter;
        public GameEvent startGame;

        public bool globalSpawn;

        private SpriteRenderer CharacterPreview;



        // Start is called before the first frame update
        void Start()
        {
            CharacterPreview = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        //void Update()
        //{
        //    //If there is a list of characters to spawn, loop through and spawn them. 
        //    if (CheckIsTileOnSpawnTile() || (globalSpawn && characters.Count > 0))
        //    {
        //        CharacterPreview.sprite = characters[0].GetComponent<SpriteRenderer>().sprite;
        //        CharacterPreview.color = new Color(1, 1, 1, 0.75f);

        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            HeroManager newCharacter = null;
        //            if (characters.Count > 0)
        //            {
        //                newCharacter = Instantiate(characters[0]).GetComponent<HeroManager>();

        //                if (spawnCharacter)
        //                    spawnCharacter.Raise(newCharacter.gameObject);

        //                characters.RemoveAt(0);
        //            }

        //            if (characters.Count == 0)
        //            {
        //                //foreach (var item in spawnZones)
        //                //{
        //                //    item.gameObject.SetActive(false);
        //                //}

        //                if (startGame)
        //                    startGame.Raise();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        CharacterPreview.color = new Color(1, 1, 1, 0);
        //    }
        //}

        //If using spawnZones check the tile is valid.
        //private bool CheckIsTileOnSpawnTile()
        //{
        //    if (spawnZones.Count == 0)
        //        return false;

        //    if (characters.Count > 0 && focusedOnTile && focusedOnTile.isWalkable)
        //    {
        //        var nextChar = characters.First();
        //        var charactersSpawnZone = spawnZones.Where(x => x.TeamID == nextChar.teamID).ToList().First();

        //        foreach (var item in charactersSpawnZone.spawnTiles)
        //        {
        //            if (focusedOnTile.gridLocation == item.gridLocation)
        //                return true;
        //        }
        //    }

        //    return false;
        //}
    }
}
