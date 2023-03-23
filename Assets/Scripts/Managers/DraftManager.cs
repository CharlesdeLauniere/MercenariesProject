using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public GameObject _attackButton;


    public void AffichageHero() 
    {
        Debug.Log("Yo");
        _attackButton.SetActive(true);
    }

   
}
