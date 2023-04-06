using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace MercenariesProject
{
    
    public class CarteManager : MonoBehaviour
    {
       public bool selectionValide = true;
       public Hero hero;
       public GameEventString heroDrafted;
       //public GameEventString showAbilitites;
       
       public void OnHeroSelect()
       {
            if(selectionValide == true)
            {
                string name = hero.heroClass.ClassName;
                Debug.Log(name);
                heroDrafted.Raise(name);
            }
           
            selectionValide = false;
       }
        public void ShowAbility()
        {
            //showAbilitites.Raise(hero.heroClass.ClassName);
        }
        
    }
}
