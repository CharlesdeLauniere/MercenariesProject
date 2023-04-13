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
    //public Sprite characterSprite;
    //public Image Abilete1;
    //public Sprite Abilete2;
    //public Sprite Abilete3;
    //public Sprite Passif;

    //private Vector3 Position;
    //public GameObject _abilites;

    public int i = 1;
    public Canvas canvas;
    public List<Hero> spawnableHeroes;

    public List<string> redHeroesTospawn;
    public List<string> blueHeroesTospawn;

    public GameObject _teamBlue;
    public GameObject _teamRed;
    //public GameObject _abilitiesPrefabContainer;
    public GameObject _archerSkills;
    //public GameObject _mageSkills;

    [SerializeField] private List<GameObject> abilityPrefabs;

    void Start()
    {
        //_knightSkill.SetActive(false);
        //_archerSkills.SetActive(false);
        ////_mageSkills.SetActive(false);
        //_teamBlue.SetActive(true);

        //foreach(var hero in spawnableHeroes)
        //{
        //    var prefab = Instantiate(hero.heroClass.abilitiesPrefab.gameObject, new Vector2(0f, 0f), Quaternion.identity);
        //    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(270f, 150f);
        //    prefab.transform.SetParent(_abilitiesPrefabContainer.transform);
        //    prefab.transform.localScale = Vector3.one;
        //    prefab.SetActive(false);
        //    abilityPrefabs.Add(prefab);
        //}


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
        Debug.Log("eeeeeeeeee");
        abilitiesPrefab.SetActive(true);
    }
    public void DesAffichage(GameObject abilitiesPrefab)
    {
        abilitiesPrefab.SetActive(false);
    }
    
    public void SelectionManager()
    {
        Debug.Log(i);
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
