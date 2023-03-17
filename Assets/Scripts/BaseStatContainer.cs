using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BaseStatContainer", menuName = "ScriptableObjects/BaseStatContainer", order = 1)]
public class BaseStatContainer : ScriptableObject
{
    public float Health;
    public float Mana;
    public float Attack;
    public float Defence;
    public float Initiative;
    public float MoveRange;
}
