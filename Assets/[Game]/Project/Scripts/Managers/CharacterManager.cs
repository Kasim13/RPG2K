using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    private List<Character> characters;

    public List<Character> Characters { get { return (characters == null) ? characters = new List<Character>() : characters; } set { characters = value; } }

    private Character player;

    public Character Player
    {
        get
        {
            if(player == null)
            {
                foreach(var character in Characters)
                {
                    player = character;
                }
            }

            return player;
        }

        set { player = value; }
    }

    public void AddCharacter(Character character)
    {
        if (!Characters.Contains(character))
            Characters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        if (Characters.Contains(character))
            Characters.Remove(character);
    }
}
