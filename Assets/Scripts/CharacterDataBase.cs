using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu]
public class CharacterDataBase : ScriptableObject
{
    public DraftManager[] draftManager;

    public int CharacterCount
    {
        get 
        { 
            return draftManager.Length;
        }
    }

    public DraftManager GetCharacter(int index)
    {
        return draftManager[index];
    }
}
