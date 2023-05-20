//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;




//Ce script ne sert plus, car ce script a été fait pour qu'il fonctionne avec les anciens scripts de Charles
//, mais comme j'ai fait ce script (Brandon) au début de la session, les scripts de Charles ont beaucoup changer
//, donc celui-ci ne fonctionne plus avec les nouveaux scripts



//namespace MercenariesProject
//{

//    public class PersonnageInitiative
//    {
//        public BaseHero Hero { get; set; }
//        public int Initiative { get; set; }

//    }


//    public class InitiativeManager : MonoBehaviour
//    {
//        public List<PersonnageInitiative> _initiative;

//        public void ResetBarreInitiative()
//        {
//            List<Vector3> _positionList = new List<Vector3>();
//            _positionList.Add(new Vector3(150, 185f, 0));
//            _positionList.Add(new Vector3(100, 185f, 0));
//            _positionList.Add(new Vector3(50, 185f, 0));
//            _positionList.Add(new Vector3(0, 185f, 0));
//            _positionList.Add(new Vector3(-50, 185f, 0));
//            _positionList.Add(new Vector3(-100, 185f, 0));

//            for (int i = 0; i < 6; i++)
//            {
//                _initiative.Add(new PersonnageInitiative{Initiative = UnitManager.Instance.baseHeroes[i].GetInitiative(),Hero = UnitManager.Instance.baseHeroes[i]});

//            }
//            List<PersonnageInitiative> SortedInitiative = _initiative.OrderBy(x => x.Initiative).ToList();

//            for (int i = 0; i < 6; i++)
//            {

//                Instantiate(SortedInitiative[i].Hero._imageIcon, _positionList[i], Quaternion.identity);

//            }
//            _initiative.Clear();
//        }
//    }
//}