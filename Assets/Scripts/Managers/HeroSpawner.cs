using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MercenariesProject
{
    public class HeroSpawner : MonoBehaviour
    {
        public List<Hero> heroes;

        //public Tile focusedOnTile;

        public GameEventStringList spawnHeroes;
        public GameEventGameObject heroSpawned;
        public GameEvent startGame;
        public GameObject _bluePrefab;
        [SerializeField] GameObject _redPrefab;
        //public bool globalSpawn;

        private SpriteRenderer CharacterPreview;
        private List<string> heroesToSpawn;


        // Start is called before the first frame update
        void Start()
        {
            CharacterPreview = GetComponent<SpriteRenderer>(); 
        }
        public void SpawnHeroes(List<string> spawnName)
        {

            for (int i = 0; i < heroes.Count; i += 2)
            {
                var redHeroPrefab = GetSpecificHeroToSpawn<Hero>(spawnName[i]);
                var redSpawnedHero = Instantiate(redHeroPrefab);
                redSpawnedHero.tag = "Player1";
                redSpawnedHero.teamID = 1;
                var redRandomSpawnTile = GridManager.Instance.GetRedHeroSpawnTile();
                redSpawnedHero.SetupHealthBar();
                heroSpawned.Raise(redSpawnedHero.gameObject);
                var newRedPrefab=Instantiate(_redPrefab,new Vector3(redSpawnedHero.transform.position.x, redSpawnedHero.transform.position.y, redSpawnedHero.transform.position.z),Quaternion.identity);
                newRedPrefab.transform.parent = redSpawnedHero.transform;


                var blueHeroPrefab = GetSpecificHeroToSpawn<Hero>( spawnName[i+1]);
                var blueSpawnedHero = Instantiate(blueHeroPrefab);
                blueSpawnedHero.tag = "Player2";
                blueSpawnedHero.teamID = 2;
                var blueRandomSpawnTile = GridManager.Instance.GetBlueHeroSpawnTile();
                blueSpawnedHero.SetupHealthBar();
                heroSpawned.Raise(blueSpawnedHero.gameObject);
                var newBluePrefab = Instantiate(_bluePrefab, new Vector3(blueSpawnedHero.transform.position.x, blueSpawnedHero.transform.position.y, blueSpawnedHero.transform.position.z), Quaternion.identity);
                newBluePrefab.transform.parent = blueSpawnedHero.transform;


                redRandomSpawnTile.SetUnit(redSpawnedHero);
                blueRandomSpawnTile.SetUnit(blueSpawnedHero);

                //blueHeroPrefab.SetupHealthBar();
               //redHeroPrefab.SetupHealthBar();

                //heroes.Add(redSpawnedHero);
                //heroes.Add(blueSpawnedHero);

            }
            startGame.Raise();

        }
       
        private T GetSpecificHeroToSpawn<T>(string heroName) where T : Hero
        {
            return (T)heroes.Find(x => x.heroClass.ClassName == heroName);

        }
        public void SpawnRedHeroes(List<string> spawnName)
        {
            for (int i = 0; i < heroes.Count; i += 1)
            {
                var redHeroPrefab = GetSpecificHeroToSpawn<Hero>(spawnName[i]);
                var redSpawnedHero = Instantiate(redHeroPrefab);
                redSpawnedHero.tag = "Player1";
                redSpawnedHero.teamID = 1;
                var redRandomSpawnTile = GridManager.Instance.GetRedHeroSpawnTile();
                redSpawnedHero.SetupHealthBar();
                heroSpawned.Raise(redSpawnedHero.gameObject);

              

                //blueHeroPrefab.SetupHealthBar();
                //redHeroPrefab.SetupHealthBar();

                //heroes.Add(redSpawnedHero);
                //heroes.Add(blueSpawnedHero);

            }
            startGame.Raise();

        }
        public void SpawnBlueHeroes(List<string> spawnName)
        {
            for (int i = 0; i < heroes.Count; i += 1)
            {
               
                var blueHeroPrefab = GetSpecificHeroToSpawn<Hero>(spawnName[i]);
                var blueSpawnedHero = Instantiate(blueHeroPrefab);
                blueSpawnedHero.tag = "Player2";
                blueSpawnedHero.teamID = 2;
                var blueRandomSpawnTile = GridManager.Instance.GetBlueHeroSpawnTile();
                blueSpawnedHero.SetupHealthBar();
                heroSpawned.Raise(blueSpawnedHero.gameObject);

                blueRandomSpawnTile.SetUnit(blueSpawnedHero);
                //         

                //blueHeroPrefab.SetupHealthBar();
                //redHeroPrefab.SetupHealthBar();

                //heroes.Add(redSpawnedHero);
                //heroes.Add(blueSpawnedHero);

            }
            startGame.Raise();

        }
      
        //        {
        //            var heroCount = 3;
        //            for (int i = 0; i < heroCount; i++)
        //            {
        //                var redHeroPrefab = GetSpecificHeroToSpawn<BaseHero>(Faction.Red, spawnName[i]);
        //                var redSpawnedHero = Instantiate(redHeroPrefab);
        //                var redRandomSpawnTile = GridManager.Instance.GetRedHeroSpawnTile();

        //                var blueHeroPrefab = GetSpecificHeroToSpawn<BaseHero>(Faction.Blue, spawnName[i]);
        //                var blueSpawnedHero = Instantiate(blueHeroPrefab);
        //                var blueRandomSpawnTile = GridManager.Instance.GetBlueHeroSpawnTile();

        //                redRandomSpawnTile.SetUnit(redSpawnedHero);
        //                blueRandomSpawnTile.SetUnit(blueSpawnedHero);

        //                //blueHeroPrefab.SetupHealthBar();
        //                //redHeroPrefab.SetupHealthBar();

        //                baseHeroes.Add(redSpawnedHero);
        //                baseHeroes.Add(blueSpawnedHero);



        //            }
        //         
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
