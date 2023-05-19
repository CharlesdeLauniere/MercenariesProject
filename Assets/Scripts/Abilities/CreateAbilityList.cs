using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MercenariesProject
{
    public class CreateAbilityList : MonoBehaviour
    {
        public GameObject ButtonPrefab;
        public List<GameObject> buttons;

        private Hero activeHero;

        private void Start()
        {
            buttons = new List<GameObject>();
        }

        private void Update()
        {
            if(activeHero != null && buttons.Any() == true && Input.GetKeyDown(KeyCode.Escape))
            {
                ClearAbilityButtons();
            }
        }

        public void SetActiveCharacter(GameObject activeHero)
        {
            this.activeHero = activeHero.GetComponent<Hero>();
        }


        //Crée un bouton pour chacun des habilités d'un personnage
        public void CreateCharacterAbilityButtons()
        {
            if (!buttons.Any(x => x.activeInHierarchy))
            {
                foreach (var item in buttons)
                {
                    item.SetActive(false);
                }

                var abilityList = activeHero.heroClass.abilities;

                foreach (var ability in abilityList)
                {
                    var buttonToActivate = buttons.Find(x => !x.activeInHierarchy);

                    if (buttonToActivate == null)
                    {
                        buttonToActivate = Instantiate(ButtonPrefab, transform);
                        buttons.Add(buttonToActivate);
                    }
                    else
                    {
                        buttonToActivate.SetActive(true);
                    }

                    buttonToActivate.transform.GetComponentInChildren<Text>().text = ability.name;

                    if (activeHero.GetStat(Stats.CurrentMana).statValue >= ability.cost)
                    {
                        buttonToActivate.GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        buttonToActivate.GetComponent<Button>().interactable = false;
                    }
                }
            }
            else
            {
                ClearAbilityButtons();
            }
        }

        public void ClearAbilityButtons()
        {
            buttons.Clear();
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void DisableAbilityList() 
        {
            foreach (var item in buttons)
            {
                item.SetActive(false);
            }
        }
        public void DisableAbilityByName() 
        {
            
        }
    }
}