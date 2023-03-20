using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDataBase : ScriptableObject
{
    [SerializeField] public DraftManager[] draftManager;

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
