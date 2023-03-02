using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;

    private List<ScriptableUnit> _heroes;

    private void Awake() {
        Instance = this;

        _heroes = Resources.LoadAll<ScriptableUnit>("Heroes").ToList();
        
    }

    
    public void SpawnHeroes()
    {
        var heroCount = 6;
        for (int i = 0; i < heroCount; i++)
        {
            var heroPrefab = GetSpecificHero<BaseHero>(Faction.Blue);
            var spawnedHero = Instantiate(heroPrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();

            randomSpawnTile.SetUnit(spawnedHero);
        }
    }

    private T GetSpecificHero<T>(Faction faction) where T : BaseHero
    {
        return (T)_heroes.Where(u => u.Faction == faction).OrderBy(o=> Random.value).FirstOrDefault().HeroPrefab;

    }
}
