using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class TitleManager : Singleton<TitleManager>
{
    float loadValue = 0; // max = 1
    public static bool isLoadComplete = false;

    [SerializeField] Image loadProgressBar;

    private Queue<Action> loadQue = new Queue<Action>();

    protected override void Awake()
    {
        base.Awake();

        LoadStart();
    }

    public void AddLoadQue(Action _action)
    {
        loadQue.Enqueue(_action);
    }

    public void LoadStart()
    {
        loadProgressBar.fillAmount = 0;
        StartCoroutine(LoadSequence());
        StartCoroutine(ProgressUpdae());
    }

    IEnumerator ProgressUpdae()
    {
        float fillSpeed = 0.5f;
        while(true)
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

    IEnumerator LoadSequence()
    {
        yield return null;

        yield return new WaitForSeconds(0.2f);
        loadValue += 0.1f;
        yield return new WaitForSeconds(0.2f);
        loadValue += 0.1f;
        yield return new WaitForSeconds(0.2f);
        loadValue += 0.1f;
        yield return new WaitForSeconds(0.2f);
        loadValue += 0.1f;
        yield return new WaitForSeconds(0.2f);
        loadValue += 0.1f;
        yield return new WaitForSeconds(0.2f);
        loadValue += 0.1f;
        yield return new WaitForSeconds(0.2f);
        loadValue += 0.1f;
        yield return new WaitForSeconds(0.2f);
        loadValue += 0.1f;
        yield return new WaitForSeconds(0.2f);


        LoadCompleteAction();
    }

    void LoadCompleteAction()
    {
        loadValue = 1;
        isLoadComplete = true;
    }
}
