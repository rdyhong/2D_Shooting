using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayType playType = PlayType.PC;

    protected override void Awake()
    {
        base.Awake();

        NetworkManager.CheckInternetConnect(true);

        //ResourceManager.Instance.StartLoadRecource();
        PoolManager.Instance.Init();

        FirebaseManager.Instance.WriteUserData("goni", "bbbb");

        //NoticeManager.Instance.AddNotice("111111111111111", "title1");
        //NoticeManager.Instance.AddNotice("22222222222222", "title2");
        //NoticeManager.Instance.AddNotice("111111111111111");
    }

}
