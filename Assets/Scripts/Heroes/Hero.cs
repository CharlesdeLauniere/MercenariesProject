using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

namespace MercenariesProject
{
    public class Hero : MonoBehaviour
    {
        [Header("General")]
        public int teamID = 0;
        public Sprite portrait;
        [HideInInspector]
        public Tile activeTile;
        public HeroClass heroClass;
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

        private int initiativeBase = 1000;

        private void Awake()
        {
            SpawnCharacter();
        }

        public void SpawnCharacter()
        {
            SetStats();

            myRenderer = gameObject.GetComponent<SpriteRenderer>();
            initiativeValue = Mathf.RoundToInt(initiativeBase / GetStat(Stats.Speed).statValue);
        }

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

        }

        public void SetupHealthBar()
        {
            HealthBar.SetMaxHealth((float)heroClass.Health.baseStatValue);
            ManaBar.SetMaxMana((float)heroClass.Mana.baseStatValue);
        }

        public void UpdateInitiative(int turnValue)
        {
            initiativeValue += Mathf.RoundToInt(turnValue / GetStat(Stats.Speed).statValue + 1);
            previousTurnCost = turnValue;
        }

        public void SetTargeted(bool focused = false)
        {
            isTargetted = focused;

        }

        public void TakeDamage(int damage, bool ignoreDefence = false)
        {
            int damageToTake = ignoreDefence ? damage : CalculateDamage(damage);

            if (damageToTake > 0)
            {
                statsContainer.CurrentHealth.statValue -= damageToTake;
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
            statsContainer.CurrentMana.statValue += value;
            UpdateCharacterUI();

        }
        public void HealEntity(int value)
        {
            statsContainer.CurrentHealth.statValue += value;
            UpdateCharacterUI();
        }

        private int CalculateDamage(int damage)
        {
            float percentage = (((float)GetStat(Stats.Endurance).statValue / (float)damage) * 100) / 2;

            percentage = percentage > 75 ? 75 : percentage;

            int damageToTake = damage - Mathf.CeilToInt((float)(percentage / 100f) * (float)damage);
            return damageToTake;
        }

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

        public IEnumerator Die()
        {
            Animator anim = GetComponentInChildren<Animator>();
            if (anim != null)
            {
                anim.Play("Base Layer.Death");

            }
            yield return new WaitForSecondsRealtime(5f);


            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

        }

        //Mettre à jour la barre de vie
        public void UpdateCharacterUI()
        {
            this.HealthBar.SetHealth((float)statsContainer.CurrentHealth.statValue);
            this.ManaBar.SetMana((float)statsContainer.CurrentMana.statValue);
        }

        //Mettre à jour la mana après une habilité
        public void UpdateMana(int value) => statsContainer.CurrentMana.statValue -= value;

        //Attacher un effet sur un personnage
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

        //Applique le résultat de l'effet immédiatement
        public void ApplySingleEffects(Stats selectedStat)
        {
            Stat value = statsContainer.getStat(selectedStat);
            value.ApplyStatMods();
            UpdateCharacterUI();
        }

        //Applique tous les effets sur un personnage
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

        public void LinkCharacterToTile(Tile tile)
        {
            UnlinkCharacterToTile();
            tile.activeHero = this;
            tile.isWalkable = false;
            activeTile = tile;
        }

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

