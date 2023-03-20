using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;

public class NoticeManager : Singleton<NoticeManager>
{
    private Queue<NoticeData> noticeQue = new Queue<NoticeData>();

    private Notice_Prefab noticePrefab = null;
    private Transform parentCanvas = null;
    public static bool isReady = true;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        // Set Parent Canvas Transform
        parentCanvas = GameObject.Find("NoticeCanvas").transform;

        LoadPopUpUI();
        StartCoroutine(CheckNotice());
    }

    IEnumerator CheckNotice()
    {
        // Null check for noticePrefab
        yield return new WaitUntil(() => noticePrefab != null);

        while (true)
        {
            yield return null;

            // Show Notice
            if (isReady && noticeQue.Count > 0)
            {
                isReady = false;
                NoticeData data = noticeQue.Dequeue();
                noticePrefab.gameObject.SetActive(true);
                noticePrefab.Notice(data.content, data.title);
            }
        }
    }

    private void LoadPopUpUI()
    {
        // Instantiate PopUp UI
        Addressables.InstantiateAsync("PopUp_Notice").Completed += handle => {
            noticePrefab = handle.Result.GetComponent<Notice_Prefab>();
            noticePrefab.gameObject.SetActive(false);
            // Set PopUp UI Rect, Parent Canvas
            RectTransform rt = noticePrefab.GetComponent<RectTransform>();
            rt.SetParent(parentCanvas);
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            Debug.Log("PopUp UI ::: Loaded");
        };
    }

    // Add Notice Data
    public void AddNotice(string _content, string _title = "Notice")
    {
        NoticeData data;
        data.content = _content;
        data.title = _title;
        noticeQue.Enqueue(data);
    }

}

public struct NoticeData
{
    public string title;
    public string content;
}
