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

    //public GameObject _archerSkills;
    [SerializeField] GameObject nextScene;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] GameObject X;
    [SerializeField] private List<GameObject> abilityPrefabs;

    private void Start()
    {
        _text.text = "C'est aux bleus de choisir !";
        _text.color = Color.blue;
        nextScene.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void HeroIsSelected(GameObject image)
    {
        Vector3 pos;
        pos = image.transform.position;
        pos.x -= 1f;
        pos.y += 3f;
        pos.z -= 8f;
        Instantiate(X, pos, Quaternion.identity);
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
            _text.text = "C'est aux bleus de choisir !";
            _text.color = Color.blue;
        }            

        else if (i == 1 || i == 2 || i == 5)
        {
            _text.text = "C'est aux rouges de choisir !";
            _text.color = Color.red;
        }

        else
        {
            _text.text = "La sélection et terminé !";
            _text.color = Color.white;
            nextScene.SetActive(true);
        }
        
    }
}
