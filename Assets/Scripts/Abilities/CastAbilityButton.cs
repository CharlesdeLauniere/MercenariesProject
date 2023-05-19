using UnityEngine;
using UnityEngine.UI;

namespace MercenariesProject
{
    public class CastAbilityButton : MonoBehaviour
    {
        public GameEventString castAbility;

        public void CastAbilityByName()
        {
            var abilityName = transform.GetComponentInChildren<Text>().text;
            castAbility.Raise(abilityName);
        }
    }
}
