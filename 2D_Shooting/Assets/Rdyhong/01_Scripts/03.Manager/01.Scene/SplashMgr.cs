using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashMgr : Singleton<SplashMgr>
{
    private void Start()
    {
        SceneMgr.Inst.LoadScene(SceneKind.Title);

        // Test
    }
}
