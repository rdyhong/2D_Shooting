using System;

public class Title : GameScene
{
    public override void Init(Action action = null)
    {
        DebugMgr.Log($"Scene Init ::: {this.GetType()}");

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
