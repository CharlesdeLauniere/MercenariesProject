using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace MercenariesProject
{
    public class TurnManager : MonoBehaviour
    {
        private List<Hero> teamA = new List<Hero>();
        private List<Hero> teamB = new List<Hero>();

        public TurnSorting turnSorting;

        public GameEventGameObject startNewCharacterTurn;
        public GameEventGameObjectList turnOrderSet;

        public List<Hero> combinedList;

        public bool ignorePlayers = false;
        public bool ignoreEnemies = false;

        public enum TurnSorting
        {
            ConstantAttribute,
            CTB
        };

        void Start()
        {
            if (!ignorePlayers)
                teamA = GameObject.FindGameObjectsWithTag("Player1").Select(x => x.GetComponent<Hero>()).ToList();

            if (!ignoreEnemies)
                teamB = GameObject.FindGameObjectsWithTag("Player2").Select(x => x.GetComponent<Hero>()).ToList();

            combinedList = new List<Hero>();

            foreach (var item in teamA)
            {
                item.teamID = 1;
            }

            foreach (var item in teamB)
            {
                item.teamID = 2;
            }

            SortTeamOrder(true);
        }

        //Sort the team turn order based on TurnSorting.
        private void SortTeamOrder(bool updateListSize = false)
        {
            switch (turnSorting)
            {
                case TurnSorting.ConstantAttribute:
                    if (updateListSize)
                    {
                        combinedList = new List<Hero>();
                        combinedList.AddRange(teamA.Where(x => x.isAlive).ToList());
                        combinedList.AddRange(teamB.Where(x => x.isAlive).ToList());
                        combinedList = combinedList.OrderBy(x => x.statsContainer.Speed.statValue).ToList();
                    }
                    else
                    {
                        Hero item = combinedList[0];
                        combinedList.RemoveAt(0);
                        combinedList.Add(item);
                    }
                    break;
                case TurnSorting.CTB:
                    combinedList = new List<Hero>();
                    combinedList.AddRange(teamA.Where(x => x.isAlive).ToList());
                    combinedList.AddRange(teamB.Where(x => x.isAlive).ToList());
                    combinedList = combinedList.OrderBy(x => x.initiativeValue).ToList();
                    break;
                default:
                    break;
            }

            turnOrderSet.Raise(combinedList.Select(x => x.gameObject).ToList());
        }

        public void StartLevel()
        {
            SortTeamOrder(true);
            if (combinedList.Where(x => x.isAlive).ToList().Count > 0)
            {
                var firsCharacter = combinedList.First();
                firsCharacter.StartTurn();
                startNewCharacterTurn.Raise(firsCharacter.gameObject);
            }
        }

        //On end turn, update the turnorder and start a new characters turn.
        public void EndTurn()
        {
            if (combinedList.Count > 0)
            {
                FinaliseEndCharactersTurn();

                SortTeamOrder();

                foreach (var Hero in combinedList)
                    Hero.isActive = false;

                if (combinedList.Where(x => x.isAlive).ToList().Count > 0)
                {
                    var firstCharacter = combinedList.First();

                    if (firstCharacter.isAlive)
                    {
                        firstCharacter.isActive = true;
                        firstCharacter.ApplyEffects();

                        if (firstCharacter.isAlive)
                        {
                            firstCharacter.StartTurn();
                            startNewCharacterTurn.Raise(firstCharacter.gameObject);
                        }
                        else
                            EndTurn();


                        foreach (var ability in firstCharacter.abilitiesForUse)
                        {
                            ability.turnsSinceUsed++;
                        }
                    }
                    else
                    {
                        EndTurn();
                    }
                }
            }
        }

        //Last few steps of ending a characters turn. 
        private void FinaliseEndCharactersTurn()
        {
            var characterEndingTurn = combinedList.First();

            if (characterEndingTurn.activeTile && characterEndingTurn.activeTile.tileData)
            {
                //Attach Apply Tile Effect
                var tileEffect = characterEndingTurn.activeTile.tileData.effect;

                if (tileEffect != null)
                    characterEndingTurn.AttachEffect(tileEffect);
            }

            combinedList.First().UpdateInitiative(Constants.BaseCost);
        }


        //Wait until next loop to avoid possible race condition. 
        IEnumerator DelayedSetActiveCharacter(Hero firstCharacter)
        {
            yield return new WaitForFixedUpdate();
            startNewCharacterTurn.Raise(firstCharacter.gameObject);
        }

        //Add a character to the turn order when they spawn. 
        public void SpawnNewCharacter(GameObject character)
        {
            teamA.Add(character.GetComponent<HeroManager>());
            SortTeamOrder(true);
        }

    }
}
       





    //public class TurnManager : MonoBehaviour
    //{
    //    public static TurnManager Instance;
    //    public TurnState currentState;
    //    //private float cur_cooldown = 0f;
    //    // private float max_cooldown = 5f;
    //    public Image Timer;
    //    private int Actions;

//    private void Awake()
//    {
//        Instance = this;
//    }

//    public void SwitchBetweenTurnStates(TurnState turnState)
//    {
//        currentState = turnState;
//        switch (currentState)
//        {
//            case (TurnState.startCombat):
//                Actions = 2;
//                BaseHero hero = UnitManager.Instance.baseHeroes[0];
//                UnitManager.Instance.SetSelectedHero(hero);
//                this.SwitchBetweenTurnStates(TurnState.selectingAttack);
//                break;
//            case (TurnState.chosingTarget):
//                if (Actions > -1) MenuManager.Instance.ShowAbilities(null);
//                break;
//            case (TurnState.movement):
//                MenuManager.Instance.ShowAbilities(null);


//                break;
//            case (TurnState.usingBaseAttack):

//                UnitManager.Instance.SelectedHero.BaseAttack(UnitManager.Instance.TargetedHero);
//                UnitManager.Instance.SetTargetedHero(null);
//                MenuManager.Instance.ShowTargetedHero(null);

//                this.SwitchBetweenTurnStates(TurnState.selectingAttack);
//                break;
//            case (TurnState.selectingAttack):
//                Actions--;
//                UnitManager.Instance.IsWinner();
//                if (Actions > -1) MenuManager.Instance.ShowAbilities(UnitManager.Instance.SelectedHero);
//                else this.SwitchBetweenTurnStates(TurnState.next);
//                break;
//            case (TurnState.next):
//                UnitManager.Instance.IsWinner();
//                UnitManager.Instance.NextHeroTurn();
//                break;
//            case (TurnState.end):
//                GameManager.Instance.ChangeState(GameState.GameEnd);
//                break;
//            default: break;
//        }
//    }

//    public void BasicAttackButtonPress()
//    {
//        TurnManager.Instance.SwitchBetweenTurnStates(TurnState.chosingTarget);
//    }
//    public void MovementButtonPress()
//    {
//        TurnManager.Instance.SwitchBetweenTurnStates(TurnState.movement);
//    }
//    public enum TurnState
//    {
//        startCombat,
//        chosingTarget,
//        selectingAttack,
//        movement,
//        usingBaseAttack,
//        usingAbility1,
//        usingAbility2,
//        next,
//        end
//    }

//    //void Update()
//    //{
//    //    cur_cooldown = cur_cooldown + Time.deltaTime;
//    //    float calc_cooldown = cur_cooldown / max_cooldown;
//    //    Timer.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), Timer.transform.localScale.y, Timer.transform.localScale.z);
//    //    if (cur_cooldown >= max_cooldown)
//    //    {
//    //        currentState = TurnState.next;
//    //    }
//    //}
//}

