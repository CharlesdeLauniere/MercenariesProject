using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace MercenariesProject
{
    public class HeroSpawner : MonoBehaviour
    {
        public List<Hero> heroes;

        public GameEventStringList spawnHeroes;
        public GameEventGameObject heroSpawned;
        public GameEvent startGame;
        public GameObject _bluePrefab;
        [SerializeField] GameObject _redPrefab;
        public GameObject draftManager;

        private SpriteRenderer CharacterPreview;
        private List<string> heroesToSpawn;
        [SerializeField] private List<string> blueToSpawn;
        [SerializeField] private List<string> redToSpawn;



        void Start()
        {
            CharacterPreview = GetComponent<SpriteRenderer>();
    
        }
        public void listPerso()
        {

        }
        public void SpawnHeroes(List<string> redToSpawn)
        {
            draftManager = GameObject.Find("DraftManager");
            blueToSpawn = draftManager.GetComponent<DraftManager>().blueHeroesTospawn;
            redToSpawn = draftManager.GetComponent<DraftManager>().redHeroesTospawn;
            List<string> spawnNameRed = redToSpawn;
            List<string> spawnNameBlue = blueToSpawn;

            for (int i = 0; i < 3; i += 1)
            {
                var redHeroPrefab = GetSpecificHeroToSpawn<Hero>(spawnNameRed[i]);
                var redSpawnedHero = Instantiate(redHeroPrefab);
                redSpawnedHero.tag = "Player1";
                redSpawnedHero.teamID = 1;
                var redRandomSpawnTile = GridManager.Instance.GetRedHeroSpawnTile();
                redSpawnedHero.SetupHealthBar();
                heroSpawned.Raise(redSpawnedHero.gameObject);
                var newRedPrefab=Instantiate(_redPrefab,new Vector3(redSpawnedHero.transform.position.x, redSpawnedHero.transform.position.y, redSpawnedHero.transform.position.z),Quaternion.identity);
                newRedPrefab.transform.parent = redSpawnedHero.transform;


                var blueHeroPrefab = GetSpecificHeroToSpawn<Hero>( spawnNameBlue[i]);
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


            }
            startGame.Raise();

        }
       
        private T GetSpecificHeroToSpawn<T>(string heroName) where T : Hero
        {
            return (T)heroes.Find(x => x.heroClass.ClassName == heroName);

        }
        public void SpawnRedHeroes()
        {
            List<string> spawnName = redToSpawn;

            for (int i = 0; i < heroes.Count; i += 1)
            {
                var redHeroPrefab = GetSpecificHeroToSpawn<Hero>(spawnName[i]);
                var redSpawnedHero = Instantiate(redHeroPrefab);
                redSpawnedHero.tag = "Player1";
                redSpawnedHero.teamID = 1;
                var redRandomSpawnTile = GridManager.Instance.GetRedHeroSpawnTile();
                redSpawnedHero.SetupHealthBar();
                heroSpawned.Raise(redSpawnedHero.gameObject);



            }
            startGame.Raise();

        }
        public void SpawnBlueHeroes()
        {
            List<string> spawnName = blueToSpawn;

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
 

            }
            startGame.Raise();

        }
      
    }
}
