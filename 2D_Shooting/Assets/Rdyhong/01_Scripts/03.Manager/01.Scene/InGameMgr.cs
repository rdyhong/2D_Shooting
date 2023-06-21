using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMgr : Singleton<InGameMgr>
{
    public static CharacterController myCharacter;
    public Transform playerParent;
    public Transform weaponParent;
    public Transform ItemUIParent;

    public static void SetMyCharacter(CharacterController _c)
    {
        myCharacter = _c;
    }

}
