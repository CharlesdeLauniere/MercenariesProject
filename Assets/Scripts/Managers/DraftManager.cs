using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MercenariesProject;
using System.Linq;

[System.Serializable]
public class DraftManager : MonoBehaviour
{
    public int i;
    public Canvas canvas;
    public List<Hero> spawnableHeroes;

    public List<string> redHeroesTospawn;
    public List<string> blueHeroesTospawn;

    public GameObject _teamBlue;
    public GameObject _teamRed;
    public GameObject _archerSkills;

    [SerializeField] private List<GameObject> abilityPrefabs;

    void Start()
    {
        _teamBlue.SetActive(true);
        _teamRed.SetActive(false);
    }

    void Update()
    {
        
    }

    public void ShowHeroAbility(string heroName)
    {
        var abilityPrefab = abilityPrefabs.Where(x => x.GetComponent<GameObject>().name == heroName).First();
        abilityPrefab.SetActive(true);
    }
    public void HideHeroAbility(string heroName)
    {
        var abilityPrefab = abilityPrefabs.Where(x => x.GetComponentInChildren<CarteManager>().hero.heroClass.ClassName == heroName).First();
        abilityPrefab.SetActive(false);
    }

    public void Selection(string className)
    {
        
        if (i == 0 || i == 3 || i == 4)
        {
            blueHeroesTospawn.Add(className);
        }
        else if (i == 1 || i == 2 || i == 5)
        {
            redHeroesTospawn.Add(className);
        }
        SelectionManager();
        Debug.Log("WTEWRR G");
    }

    public void Affichage(GameObject abilitiesPrefab) 
    {
        
        abilitiesPrefab.SetActive(true);
    }
    public void DesAffichage(GameObject abilitiesPrefab)
    {
        abilitiesPrefab.SetActive(false);
    }
    
    public void SelectionManager()
    {
        i++;

        if (i == 0 || i == 3 || i == 4)
        {
            _teamRed.SetActive(false);
            _teamBlue.SetActive(true);
        }

        else if (i == 1 || i == 2 || i == 5)
        {
            _teamRed.SetActive(true);
            _teamBlue.SetActive(false);
        }

        else
        {
            _teamRed.SetActive(false);
            _teamBlue.SetActive(false);
            Debug.Log("Fin de la sélection");
        }
        
    }
}
