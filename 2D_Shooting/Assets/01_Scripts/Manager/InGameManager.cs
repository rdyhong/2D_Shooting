using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    public static CharacterController myCharacter;

    public static void SetMyCharacter(CharacterController _character)
    {
        myCharacter = _character;
    }


}
