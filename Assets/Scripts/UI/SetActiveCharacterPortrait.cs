using UnityEngine;
using UnityEngine.UI;

namespace MercenariesProject
{
    public class SetActiveCharacterPortrait : MonoBehaviour
    {
        public void SetCharacterImage(GameObject activeCharacter)
        {
            GetComponent<Image>().sprite = activeCharacter.GetComponent<Hero>().portrait;
        }
    }
}
