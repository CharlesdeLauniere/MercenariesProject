using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MercenariesProject
{
    //A container that creates all the buttons needed for a characters abilities.
    public class CreateAbilityList : MonoBehaviour
    {
        public GameObject ButtonPrefab;
        public List<GameObject> buttons;

        private Hero activeHero;

        private void Start()
        {
            buttons = new List<GameObject>();
        }

        public void SetActiveCharacter(GameObject activeHero)
        {
            this.activeHero = activeHero.GetComponent<Hero>();
        }


        //When the ability button is clicked, create a new button for every ability the activeCharacter has
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

                    if (activeHero.GetStat(Stats.CurrentMana).statValue >= ability.cost) //abilityContainer.turnsSinceUsed > abilityContainer.ability.cooldown
                    {
                        Debug.Log("Yay");
                        buttonToActivate.GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        Debug.Log("Nay");
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

        public void DisableAbilityList(string abilityName)
        {
            foreach (var item in buttons)
            {
                item.SetActive(false);
            }
        }
    }
}