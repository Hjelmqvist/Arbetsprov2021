using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base information for a type of character/class
/// </summary>
[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character")]
public partial class CharacterBase : ScriptableObject, IStats
{
    [SerializeField] string characterName = "Bob";
    [SerializeField] Stats stats = null;

    public string CharacterName => characterName;

    static Dictionary<string, CharacterBase> characterDatabase = null;

    public static bool TryGetCharacterBase(string characterName, out CharacterBase characterBase)
    {
        if (characterDatabase == null)
        {
            characterDatabase = new Dictionary<string, CharacterBase>();
            CharacterBase[] allCharacters = Resources.LoadAll<CharacterBase>("");
            foreach (CharacterBase character in allCharacters)
            {
                if (!characterDatabase.ContainsKey(character.CharacterName))
                    characterDatabase[character.CharacterName] = character;
            }
        }
        characterBase = characterDatabase.TryGetValue(characterName, out CharacterBase value) ? value : null;
        return characterBase != null;
    }

    public int GetStatValue(StatType stat)
    {
        return stats.GetStatValue(stat);
    }

    public bool TryModifyStat(StatType stat, int value)
    {
        //Base stats are not allowed to be modified.
        return false;
    }
}