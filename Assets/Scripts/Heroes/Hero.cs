using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

namespace MercenariesProject
{
    //Parent Class for Characters and Enemys
    public class Hero : MonoBehaviour
    {
        [Header("Character Specific")]
        //public List<Ability> abilities;


        [Header("General")]
        public int teamID = 0;
        public Sprite portrait;
        [HideInInspector]
        public Tile activeTile;
        public HeroClass heroClass;
       // [HideInInspector]
        public HeroStats statsContainer;
        [HideInInspector]
        public int initiativeValue;

        [HideInInspector]
        public bool isAlive = true;
        [HideInInspector]
        public bool isActive;
        public GameEvent endTurn;
        public HealthBar HealthBar;
        public ManaBar ManaBar;
        [HideInInspector]
        public int previousTurnCost = -1;

        private bool isTargetted = false;
        [HideInInspector]
        public SpriteRenderer myRenderer;

        //public GameConfig gameConfig;

        private int initiativeBase = 1000;

        private void Awake()
        {
            SpawnCharacter();
        }

        public void SpawnCharacter()
        {
            //SetAbilityList();
            SetStats();
            //requiredExperience = gameConfig.GetRequiredExp(level);

            myRenderer = gameObject.GetComponent<SpriteRenderer>();
            initiativeValue = Mathf.RoundToInt(initiativeBase / GetStat(Stats.Speed).statValue);
        }

        //Setup the statsContainer and scale up the stats based on level. 
        public void SetStats()
        {
            if (statsContainer == null)
            {
                statsContainer = ScriptableObject.CreateInstance<HeroStats>();
                statsContainer.Health = new Stat(Stats.Health, heroClass.Health.baseStatValue, this);
                statsContainer.Mana = new Stat(Stats.Mana, heroClass.Mana.baseStatValue, this);
                statsContainer.Strength = new Stat(Stats.Strength, heroClass.Strength.baseStatValue, this);
                statsContainer.Endurance = new Stat(Stats.Endurance, heroClass.Endurance.baseStatValue, this);
                statsContainer.Speed = new Stat(Stats.Speed, heroClass.Speed.baseStatValue, this);
                statsContainer.Intelligence = new Stat(Stats.Intelligence, heroClass.Intelligence.baseStatValue, this);
                statsContainer.MoveRange = new Stat(Stats.MoveRange, heroClass.MoveRange, this);
                statsContainer.AttackRange = new Stat(Stats.AttackRange, heroClass.AttackRange, this);
                statsContainer.CurrentHealth = new Stat(Stats.CurrentHealth, heroClass.Health.baseStatValue, this);
                statsContainer.CurrentMana = new Stat(Stats.CurrentMana, heroClass.Mana.baseStatValue, this);
            }

            //for (int i = 0; i < level; i++)
            //{
            //    LevelUpStats();
            //}
        }

        // Update is called once per frame
        //public void Update()
        //{
        //    if (isTargetted)
        //    {
        //        //Just a Color Lerp for when a character is targetted for an attack. 
        //        i += Time.deltaTime * 0.5f;
        //        myRenderer.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 0.5f, 0, 1), Mathf.PingPong(i * 2, 1));
        //    }
        //}

        //Get's all the available abilities from the characters class. 
        //public void SetAbilityList()
        //{
        //    abilities = new List<Ability>();
        //    foreach (var ability in heroClass.abilities)
        //    {
        //        //if (level >= ability.requiredLevel)
        //            abilities.Add(new Ability(ability));
        //    }
        //}
        public void SetupHealthBar()
        {
            HealthBar.SetMaxHealth((float)heroClass.Health.baseStatValue);
            ManaBar.SetMaxMana((float)heroClass.Mana.baseStatValue);
        }


        //public void IncreaseExp(int value)
        //{
        //    experience += value;

        //    while (experience >= requiredExperience)
        //    {
        //        experience -= requiredExperience;
        //        LevelUpCharacter();
        //    }
        //}

        ////Level down stats and get the new required experience for the next level. 
        //public void LevelDownCharacter()
        //{
        //    level--;
        //    LevelDownStats();
        //    requiredExperience = gameConfig.GetRequiredExp(level);
        //}

        //Update the characters initiative after the perform an action. This is used for Dynamic Turn Order. 
        public void UpdateInitiative(int turnValue)
        {
            initiativeValue += Mathf.RoundToInt(turnValue / GetStat(Stats.Speed).statValue + 1);
            previousTurnCost = turnValue;
        }

        //Entity is being targets for an attack. 
        public void SetTargeted(bool focused = false)
        {
            isTargetted = focused;

            //if (isTargetted)
            //{
            //    myRenderer.color = new Color(1, 0, 0, 1);
            //}
            //else
            //{
            //    myRenderer.color = new Color(1, 1, 1, 1);
            //}
        }

        //Take damage from an attack or ability. 
        public void TakeDamage(int damage, bool ignoreDefence = false)
        {
            int damageToTake = ignoreDefence ? damage : CalculateDamage(damage);

            if (damageToTake > 0)
            {
                statsContainer.CurrentHealth.statValue -= damageToTake;
                //CameraShake.Shake(0.125f, 0.1f);
                UpdateCharacterUI();

                if (GetStat(Stats.CurrentHealth).statValue <= 0)
                {
                    isAlive = false;
                    StartCoroutine(Die());
                    UnlinkCharacterToTile();

                    if (isActive)
                        endTurn.Raise();
                }
            }
        }
     
        public void ChangeMana(int value)
        {
            Debug.Log("ManaBoost");
            statsContainer.CurrentMana.statValue += value;
            UpdateCharacterUI();
            //if (statsContainer.CurrentMana.statValue < 0) statsContainer.CurrentMana.statValue = 0;
            //if(statsContainer.CurrentMana.statValue > statsContainer.Mana.statValue) statsContainer.CurrentMana.statValue = statsContainer.Mana.statValue;

        }
        public void HealEntity(int value)
        {
            statsContainer.CurrentHealth.statValue += value;
            UpdateCharacterUI();
        }

        //basic example if using a defensive stat
        private int CalculateDamage(int damage)
        {
            float percentage = (((float)GetStat(Stats.Endurance).statValue / (float)damage) * 100) / 2;

            percentage = percentage > 75 ? 75 : percentage;

            int damageToTake = damage - Mathf.CeilToInt((float)(percentage / 100f) * (float)damage);
            return damageToTake;
        }

        //Get a perticular stat object. 
        public Stat GetStat(Stats statName)
        {
            switch (statName)
            {
                case Stats.Health:
                    return statsContainer.Health;
                case Stats.Mana:
                    return statsContainer.Mana;
                case Stats.Strength:
                    return statsContainer.Strength;
                case Stats.Endurance:
                    return statsContainer.Endurance;
                case Stats.Speed:
                    return statsContainer.Speed;
                case Stats.Intelligence:
                    return statsContainer.Intelligence;
                case Stats.MoveRange:
                    return statsContainer.MoveRange;
                case Stats.CurrentHealth:
                    return statsContainer.CurrentHealth;
                case Stats.CurrentMana:
                    return statsContainer.CurrentMana;
                case Stats.AttackRange:
                    return statsContainer.AttackRange;
                default:
                    return statsContainer.Health;
            }
        }

        //What happens when a character dies. 
        public IEnumerator Die()
        {
            float DegreesPerSecond = 360f;
            Vector3 currentRot, targetRot = new Vector3();
            currentRot = transform.eulerAngles;
            targetRot.z = currentRot.z + 90; // calculate the new angle

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

            while (currentRot.z < targetRot.z)
            {
                currentRot.z = Mathf.MoveTowardsAngle(currentRot.z, targetRot.z, DegreesPerSecond * Time.deltaTime);
                transform.eulerAngles = currentRot;
                yield return null;
            }

           // GetComponent<SpriteRenderer>().color = new Color(0.35f, 0.35f, 0.35f, 1);
        }

        //Updates the characters healthbar. 
        public void UpdateCharacterUI()
        {
            Debug.Log(statsContainer.CurrentMana.statValue);
            this.HealthBar.SetHealth((float)statsContainer.CurrentHealth.statValue);
            this.ManaBar.SetMana((float)statsContainer.CurrentMana.statValue);
        }

        //Change characters mana
        public void UpdateMana(int value) => statsContainer.CurrentMana.statValue -= value;

        //Attach an effect to the Entity from a tile or ability. 
        public void AttachEffect(ScriptableEffect scriptableEffect)
        {
            if (scriptableEffect)
            {
                var statToEffect = GetStat(scriptableEffect.statKey);

                if (statToEffect.statMods.FindIndex(x => x.statModName == scriptableEffect.name) != -1)
                {
                    int modIndex = statToEffect.statMods.FindIndex(x => x.statModName == scriptableEffect.name);
                    statToEffect.statMods[modIndex] = new StatModifier(scriptableEffect.statKey, scriptableEffect.Value, scriptableEffect.Duration, scriptableEffect.Operator, scriptableEffect.name);
                }
                else
                    statToEffect.statMods.Add(new StatModifier(scriptableEffect.statKey, scriptableEffect.Value, scriptableEffect.Duration, scriptableEffect.Operator, scriptableEffect.name));
            }
            UpdateCharacterUI();
        }

        //Effects that don't have a duration can just be applied straight away. 
        public void ApplySingleEffects(Stats selectedStat)
        {
            Stat value = statsContainer.getStat(selectedStat);
            value.ApplyStatMods();
            UpdateCharacterUI();
        }

        //Apply all the currently attached effects. Happens when a new turn begins. 
        public void ApplyEffects()
        {
            var fields = typeof(HeroStats).GetFields();

            foreach (var item in fields)
            {
                var type = item.FieldType;
                Stat value = (Stat)item.GetValue(statsContainer);

                value.ApplyStatMods();
            }

            UpdateCharacterUI();
        }

        //Gets Entities ability. 
        public Ability GetAbilityByName(string abilityName)
        {
            return heroClass.abilities.Find(x => x.Name == abilityName);
        }

        public virtual void StartTurn()
        {
        }
        public virtual void CharacterMoved()
        {
        }

        //When an Entity moves, link it to the tiles it's standing on. 
        public void LinkCharacterToTile(Tile tile)
        {
            UnlinkCharacterToTile();
            tile.activeHero = this;
            tile.isWalkable = false;
            activeTile = tile;
        }

        //Unlink an entity from a previous tile it was standing on. 
        public void UnlinkCharacterToTile()
        {
            if (activeTile)
            {
                activeTile.activeHero = null;
                activeTile.isWalkable = true;
                activeTile = null;
            }
        }
    }
}

