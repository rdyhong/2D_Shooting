using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayType playType = PlayType.PC;

    protected override void Awake()
    {
        base.Awake();

        Util.CheckInternetConnected();

        //ResourceMgr.Inst.Init();
        //NoticeMgr.Inst.Init();

        //FirebaseMgr.Inst.WriteUserData("goni", "bbbb");
    }

}
