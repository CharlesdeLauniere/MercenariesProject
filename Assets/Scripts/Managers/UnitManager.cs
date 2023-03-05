using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;

    private List<ScriptableUnit> _heroes;
    public BaseHero SelectedHero;
    public BaseHero TargetedHero;


    private void Awake() {
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
        }
        GameManager.Instance.ChangeState(GameState.TurnBasedCombat);
    }


    private T GetSpecificHeroToSpawn<T>(Faction faction, string _heroName) where T : BaseHero
    {
        return (T)_heroes.Find(x => x.HeroPrefab.UnitName == _heroName && x.Faction == faction).HeroPrefab;

    }
    public void SetSelectedHero(BaseHero hero)
    {
        SelectedHero = hero;
        MenuManager.instance.ShowSelectedHero(hero);
    }

    public void SetTargetedHero(BaseHero hero)
    {
        TargetedHero = hero;
        MenuManager.instance.ShowSelectedHero(hero);
    }
}
