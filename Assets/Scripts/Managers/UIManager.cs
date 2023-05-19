using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MercenariesProject
{
    public class UIManager : MonoBehaviour
    {
        private List<Button> actionButtons;

        void Start()
        {
            actionButtons = GetComponentsInChildren<Button>().ToList();
        }

        //Active les boutons
        public void EnableUI()
        {
            foreach (var item in actionButtons)
            {
                item.interactable = true;
            }
        }

        //Désactive les boutons
        public void DisableUI()
        {
            foreach (var item in actionButtons)
            {
                item.interactable = false;
            }
        }

        //Annule une action
        public void CancelActionState(string actionButton)
        {
            var button = actionButtons.Where(x => x.GetComponentInChildren<Text>().text == actionButton).First();
            button.interactable = true;
        }
    }
}
