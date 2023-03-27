using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MercenariesProject;

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

    public List<string> redHeroesTospawn;
    public List<string> blueHeroesTospawn;

    public GameObject _teamBlue;
    public GameObject _teamRed;
    public GameObject _knightSkill;
    public GameObject _archerSkills;
    //public GameObject _mageSkills;

    void Start()
    {
        _knightSkill.SetActive(false);
        _archerSkills.SetActive(false);
        //_mageSkills.SetActive(false);
        _teamBlue.SetActive(true);
    }

    void Update()
    {
        
    }


    public void AffichageKnight() 
    {
        _archerSkills.SetActive(false);
        Debug.Log("Yo");
        _knightSkill.SetActive(true);
    }
    public void AffichageArcher()
    {
        _knightSkill.SetActive(false);
        Debug.Log("Oy");
        _archerSkills.SetActive(true);
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
            Debug.Log("Fin de la sélection");
        }
        
    }

    public void TurnTeamBlue()
    {

    }

    public void TurnTeamRed()
    {
        
    }

    public string KnightSelection()
    {
        return "Knight";
    }

    public string ArcherSelection()
    {

        return "Archer";
    }

    public string MageSelection()
    {
        return "Mage";
    }

    public void DeleteKnightSelection()
    {

    }

    public int OnSelection(int compteur)
    {
        compteur++;
        return compteur;
    }
}
