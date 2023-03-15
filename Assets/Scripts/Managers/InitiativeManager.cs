using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PersonnageInitiative
{
    public BaseHero Hero { get; set; }
    public int Initiative { get; set; }
 
}
public class InitiativeManager : MonoBehaviour
{
 
    public static void ResetBarreInitiative()
    {
        
        List<PersonnageInitiative> _initiative= new List<PersonnageInitiative>();
        List<Vector3> _positionList = new List<Vector3>();
        _positionList.Add(new Vector3(-175, 176f, 0));
        _positionList.Add(new Vector3(-105, 176f, 0));
        _positionList.Add(new Vector3(-35, 176f, 0));
        _positionList.Add(new Vector3(35, 176f, 0));
        _positionList.Add(new Vector3(105, 176f, 0));         
        _positionList.Add(new Vector3(175, 176f, 0));
   
        for (int i = 0; i < 6; i++)
        {
            _initiative.Add(new PersonnageInitiative{Initiative = UnitManager.Instance.baseHeroes[i].GetInitiative(),Hero = UnitManager.Instance.baseHeroes[i]});

            /*if (UnitManager.Instance.baseHeroes[i].Faction == Faction.Blue)
            {

            }*/
        }
        List<PersonnageInitiative> SortedInitiative = _initiative.OrderBy(x => x.Initiative).ToList();
        
        for (int i = 0; i < 6; i++)
        {
            GameObject icon = Instantiate(SortedInitiative[i].Hero._imageIcon, _positionList[i], Quaternion.identity) as GameObject;
            icon.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);

        }
    }
}