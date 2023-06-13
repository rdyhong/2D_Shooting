using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMgr : Singleton<TitleMgr>
{
    //float loadValue = 0; // max = 1
    public static bool isLoadComplete = false;

    [SerializeField] Image loadProgressBar;

    private Queue<Action> loadQue = new Queue<Action>();
    private int maxQueCount = int.MaxValue;

    private bool ready = true;
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        AddLoadQue(() => ResourceMgr.Inst.Init(() => 
        {
            BasePerCompleteAction();
            DebugMgr.Log($"Loaded ::: ResourceMgr [ {loadProgressBar.fillAmount * 100} ]");
        }));

        AddLoadQue(() => NoticeMgr.Inst.Init(() =>
        {
            BasePerCompleteAction();
            DebugMgr.Log($"Loaded ::: NoticeMgr [ {loadProgressBar.fillAmount * 100} ]");
        }));

        AddLoadQue(() => PhotonMgr.Init(() =>
        {
            BasePerCompleteAction();
            DebugMgr.Log($"Loaded ::: PhotonMgr [ {loadProgressBar.fillAmount * 100} ]");
        }));

        maxQueCount = loadQue.Count;

        LoadStart();
    }
    private void BasePerCompleteAction()
    {
        ready = true;
        loadProgressBar.fillAmount = (float)(maxQueCount - loadQue.Count) / maxQueCount;
        if(loadQue.Count == 0)
        {
            isLoadComplete = true;
        }
    }

    public void AddLoadQue(Action _action)
    {
        loadQue.Enqueue(_action);
    }

    public void LoadStart()
    {
        loadProgressBar.fillAmount = 0;

        StartCoroutine(LoadSequence());
        //StartCoroutine(ProgressUpdae());
    }
    /*
    IEnumerator ProgressUpdae()
    {
        float fillSpeed = 0.5f;
        while (true)
        {
            yield return null;

            if (loadProgressBar.fillAmount >= 1) break;

            if (loadProgressBar.fillAmount < loadValue)
            {
                loadProgressBar.fillAmount = loadProgressBar.fillAmount + fillSpeed * Time.deltaTime;
            }
            else
            {
                loadProgressBar.fillAmount = loadValue;
            }
        }

        // On Load Complete...

        Debug.LogError("Load Complete");
    }
    */
    IEnumerator LoadSequence()
    {
        while(true)
        {
            yield return null;

            if(ready && loadQue.Count > 0)
            {
                ready = false;
                loadQue.Dequeue().Invoke();
            }

            if (isLoadComplete) break;
        }

        loadProgressBar.fillAmount = 1;

        yield return new WaitForSeconds(1.0f);

        LoadCompleteAction();
    }

    void LoadCompleteAction()
    {
        

        SceneMgr.Inst.LoadScene(SceneKind.Lobby);
    }
}
