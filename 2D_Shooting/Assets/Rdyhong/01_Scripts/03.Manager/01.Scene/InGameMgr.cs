using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMgr : Singleton<InGameMgr>
{
    public static CharacterController myCharacter;

    public static void SetMyCharacter(CharacterController _c)
    {
        myCharacter = _c;
    }

}
