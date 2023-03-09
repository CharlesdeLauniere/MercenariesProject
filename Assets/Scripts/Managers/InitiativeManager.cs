using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PersonnageInitiative
{
    public int NumeroPersonnage { get; set; }
    public int Initiative { get; set; }

}


public class InitiativeManager : MonoBehaviour
{
    [SerializeField] GameObject _personnage1;
    [SerializeField] GameObject _personnage2;
    [SerializeField] GameObject _personnage3;
    [SerializeField] GameObject _personnage4;
    [SerializeField] GameObject _personnage5;
    [SerializeField] GameObject _personnage6;
    [SerializeField] GameObject _personnage7;

    
    

    
    public List<PersonnageInitiative> _initiative;
    
    void Start()
    {
      for (int i=0; i<6; i++)
        {
            _initiative.Add(new PersonnageInitiative { NumeroPersonnage = (i + 1),Initiative= UnitManager.Instance.baseHeroes[i].GetInitiative() });
            
        }
        List<PersonnageInitiative> SortedInitiative= _initiative.OrderBy(x=>x.Initiative).ToList();


    }

  
}
