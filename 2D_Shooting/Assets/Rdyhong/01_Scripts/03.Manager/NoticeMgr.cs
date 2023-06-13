using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

public struct NoticeData
{
    public string title;
    public string content;
}

public class NoticeMgr : Singleton<NoticeMgr>
{
    private Queue<NoticeData> noticeDataQue = new Queue<NoticeData>();
    private Queue<Action> noticeCBQue = new Queue<Action>();

    private Notice_Prefab noticePrefab = null;
    private Transform parentCanvas = null;
    public static bool isReady = true;

    private Action completeCallback;

    public void Init(Action _completeCallback = null)
    {
        completeCallback = _completeCallback;

        // Set Parent Canvas Transform
        //parentCanvas = GameObject.Find("Canvas").transform;

        LoadPopUpUI();
        StartCoroutine(CheckNotice());
    }

    public void ShowNotice()
    {
        /*
        if(parentCanvas == null)
        {
            parentCanvas = GameObject.Find("Canvas").transform;
        }
        */
        if (isReady && noticeDataQue.Count > 0)
        {
            isReady = false;
            NoticeData data = noticeDataQue.Dequeue();
            noticePrefab.gameObject.SetActive(true);
            //noticePrefab.Notice(data.content, data.title, () => ShowNotice());
            noticePrefab.Notice(data.content, data.title, noticeCBQue.Dequeue());
            noticePrefab.transform.SetAsLastSibling();
        }
    }
    
    IEnumerator CheckNotice()
    {
        WaitUntil wait_Prefab = new WaitUntil(() => noticePrefab != null);
        WaitUntil wait_LobbyScene = new WaitUntil(() => SceneMgr.curSceneKind == SceneKind.Lobby);

        // Null check for noticePrefab
        yield return wait_Prefab;

        while (true)
        {
            yield return wait_LobbyScene;

            ShowNotice();

            // Show Notice
            /*
            if (isReady && noticeDataQue.Count > 0)
            {
                isReady = false;
                NoticeData data = noticeDataQue.Dequeue();
                noticePrefab.gameObject.SetActive(true);
                noticePrefab.Notice(data.content, data.title);
            }
            */
        }
    }
    

    private void LoadPopUpUI()
    {
        GameObject _canvasObj = GameObject.Find("DontDestroyCanvas");
        Canvas _canvas = _canvasObj.GetComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        _canvas.sortingOrder = 10;

        AddressableMgr.Inst.InstantiateObj<Notice_Prefab>("PopUp_Notice", (_ref) => 
        {
            noticePrefab = _ref;
            noticePrefab.gameObject.SetActive(false);

            // Set PopUp UI Rect, Parent Canvas
            RectTransform rt = noticePrefab.GetComponent<RectTransform>();
            rt.SetParent(_canvas.transform);
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            

            completeCallback?.Invoke();

            DebugMgr.Log("PopUp UI ::: Loaded");
        });
    }

    // Add Notice Data in Que
    public static void AddNotice(string _content, string _title = "UnTitle", Action _cb = null)
    {
        NoticeData data;
        data.content = _content;
        data.title = _title;
        Inst.noticeDataQue.Enqueue(data);
        Inst.noticeCBQue.Enqueue(_cb);
    }

}
