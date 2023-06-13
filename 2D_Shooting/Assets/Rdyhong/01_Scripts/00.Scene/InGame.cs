using System;

public class InGame : GameScene
{
    public override void Init(Action action = null)
    {
        DebugMgr.Log($"Scene Init ::: {this.GetType()}");

        UIWindowMgr.GetInstance<UI_Pause>().Init();

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
