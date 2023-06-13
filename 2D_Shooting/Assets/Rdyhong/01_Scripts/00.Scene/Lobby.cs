using System;

public class Lobby : GameScene
{
    public override void Init(Action action = null)
    {
        DebugMgr.Log($"Scene Init ::: {this.GetType()}");

        UIWindowMgr.GetInstance<UI_RoomList>().Init();
        UIWindowMgr.GetInstance<UI_InRoom>().Init();
        UIWindowMgr.GetInstance<UI_CreateRoom>().Init();
        UIWindowMgr.GetInstance<UI_JoinRoom>().Init();

        action?.Invoke();
    }
    public override void DeInit(Action action = null)
    {



        action?.Invoke();
    }

    public override void DataInitialize(Action action = null)
    {

        action?.Invoke();
    }
}
