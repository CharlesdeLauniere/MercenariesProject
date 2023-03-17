//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static MercenariesProject.ScriptableEffect;
//namespace MercenariesProject
//{
    

//    [CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObjects/HeroData", order = 1)]
//    public class HeroData : ScriptableObject
//    {
//        public Dictionary<string, Stat> statsDir;
//        [Header("General")]

//        [SerializeField] private string Name;
//        [SerializeField] private Faction faction;
//        public float currentHealth;
//        public float currentMana;
//        public HealthBar healthbar;


//        [Header("Stats")]
//        public BaseStatContainer heroClass;

//        [SerializeField] public List<Ability> abiltiies;
//        [SerializeField] List<AbilityContainer> abilitiesForUse;
//        public void SetDictionaryStats()
//        {
//            if (statsDir == null)
//                statsDir = new Dictionary<string, Stat>();

//            var fields = typeof(BaseStatContainer).GetFields();

//            foreach (var item in fields)
//            {
//                var type = item.FieldType;
//                float value = (float)item.GetValue(heroClass);
//            }
//        }
//        public enum Faction
//        {
//            Blue = 0,
//            Red = 1
//        }
//        public void FillHealth()
//        {
//            currentHealth = statsDir["Health"].statValue;
//        }
//        public void UpdateHealth(float value)
//        {
//            currentHealth += value;

//            if (currentHealth <= 0)
//                currentHealth = 0;
//        }
//        public void FillMana()
//        {
//            currentMana = statsDir["Mana"].statValue;
//        }
//        public void UpdateMana(float value)
//        {
//            currentMana += value;

//            if (currentMana <= 0)
//                currentMana = 0;
//        }

//        public void ApplyEffect(string attribute, float value, int duration)
//        {
//            statsDir[attribute].statMods.Add(new StatModifier(attribute, value, duration, Operation.Add));
//            statsDir[attribute].isModified = true;

//        }
//        public void AttatchEffectImproved(string attribute, float value, int duration, Operation op)
//        {
//            statsDir[attribute].statMods.Add(new StatModifier(attribute, value, duration, op));
//            statsDir[attribute].isModified = true;

//        }


//    }

//}
