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

    
    public void SpawnHeroes()
    {
        var heroCount = 3;
        for (int i = 0; i < heroCount; i++)
        {
            var redHeroPrefab = GetSpecificHero<BaseHero>(Faction.Red);
            var redSpawnedHero = Instantiate(redHeroPrefab);
            var redRandomSpawnTile = GridManager.Instance.GetRedHeroSpawnTile();

            var blueHeroPrefab = GetSpecificHero<BaseHero>(Faction.Blue);
            var blueSpawnedHero = Instantiate(blueHeroPrefab);
            var blueRandomSpawnTile = GridManager.Instance.GetBlueHeroSpawnTile();

            redRandomSpawnTile.SetUnit(redSpawnedHero);
            blueRandomSpawnTile.SetUnit(blueSpawnedHero);
        }
        GameManager.Instance.ChangeState(GameState.RedPlayerTurn);
    }


    private T GetSpecificHero<T>(Faction faction) where T : BaseHero
    {
        return (T)_heroes.Where(u => u.Faction == faction).OrderBy(o=> Random.value).FirstOrDefault().HeroPrefab;

    }
    public void SetSelectedHero(BaseHero hero)
    {
        SelectedHero = hero;
        MenuManager.instance.ShowSelectedHero(hero);
    }

    public void SetTargetedHero(BaseHero TargetHero)
    {
        TargetedHero = TargetHero;
        MenuManager.instance.ShowSelectedHero(TargetHero);
    }
}
