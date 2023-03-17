using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace MercenariesProject
{
    

    public class UnitManager : MonoBehaviour
    {
        public static UnitManager Instance;

        //[SerializeField] private Canvas Canvas;
        private List<ScriptableUnit> _heroes;
        public List<BaseHero> baseHeroes = new();

        public GameEvent startGame;

        // private SpriteRenderer CharacterPreview;

        [field: SerializeField] public BaseHero SelectedHero { get; set; }
        [field: SerializeField] public BaseHero TargetedHero { get; set; }


        private void Awake()
        {
            Instance = this;

            _heroes = Resources.LoadAll<ScriptableUnit>("Heroes").ToList();

        }
        public void SpawnHeroes(List<string> spawnName)
        {
            var heroCount = 3;
            for (int i = 0; i < heroCount; i++)
            {
                var redHeroPrefab = GetSpecificHeroToSpawn<BaseHero>(Faction.Red, spawnName[i]);
                var redSpawnedHero = Instantiate(redHeroPrefab);
                var redRandomSpawnTile = GridManager.Instance.GetRedHeroSpawnTile();

                var blueHeroPrefab = GetSpecificHeroToSpawn<BaseHero>(Faction.Blue, spawnName[i]);
                var blueSpawnedHero = Instantiate(blueHeroPrefab);
                var blueRandomSpawnTile = GridManager.Instance.GetBlueHeroSpawnTile();

                redRandomSpawnTile.SetUnit(redSpawnedHero);
                blueRandomSpawnTile.SetUnit(blueSpawnedHero);

                //blueHeroPrefab.SetupHealthBar();
                //redHeroPrefab.SetupHealthBar();

                baseHeroes.Add(redSpawnedHero);
                baseHeroes.Add(blueSpawnedHero);



            }
            GameManager.Instance.ChangeState(GameState.TurnBasedCombat);
        }


        public void IsWinner()
        {
            List<Faction> WinningFaction = new List<Faction>();
            foreach (BaseHero hero in baseHeroes)
            {
                WinningFaction.Add(hero.Faction);
            }
            bool win = WinningFaction.Distinct().Count() == 1;
            if (win) TurnManager.Instance.SwitchBetweenTurnStates(TurnManager.TurnState.end);
        }
        public void NextHeroTurn()
        {
            BaseHero hero = baseHeroes[0];
            this.baseHeroes.Remove(hero);
            this.baseHeroes.Insert(baseHeroes.Count, hero); //baseHeroes.Count
            TurnManager.Instance.SwitchBetweenTurnStates(TurnManager.TurnState.startCombat);
        }
        private T GetSpecificHeroToSpawn<T>(Faction faction, string _heroName) where T : BaseHero
        {
            return (T)_heroes.Find(x => x.HeroPrefab.UnitName == _heroName && x.Faction == faction).HeroPrefab;

        }

        public void SetSelectedHero(BaseHero hero)
        {
            SelectedHero = hero;
            MenuManager.Instance.ShowSelectedHero(hero);
        }

        public void SetTargetedHero(BaseHero hero)
        {
            MenuManager.Instance.ShowTargetedHero(hero);
            TargetedHero = hero;

        }

        //public T OrderByIntiative<T> (List­<BaseHero> _heroes)
        //public void OrderByIntiative<T>(List­<BaseHero> _heroes)
        //{
        //    //foreach (BaseHero hero in _heroes)
        //    //{
        //    //    Debug.Log($"{hero.UnitName}");
        //    //}
        //    //var ok =  (T)_heroes.OrderBy(o => o.Initiative);

        //    //foreach(BaseHero hero in ok)
        //    //{
        //    //    Debug.Log($"{(T)_heroes.OrderBy(o => o.Initiative)}");
        //    //}
        //    UnitManager.Instance.baseHeroes = (T)_heroes.OrderBy(o => o.Initiative); ;

        //}
    }

}