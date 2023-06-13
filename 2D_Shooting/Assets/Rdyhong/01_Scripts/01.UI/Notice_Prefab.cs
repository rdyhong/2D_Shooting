using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notice_Prefab : MonoBehaviour
{
    private Button backBgButton;
    private TextMeshProUGUI title;
    private TextMeshProUGUI content;
    private Action closeCallback;

    private void Awake()
    {
        title = transform.Find("BG/Title").GetComponent<TextMeshProUGUI>();
        content = transform.Find("BG/Content").GetComponent<TextMeshProUGUI>();

        backBgButton = transform.Find("BackBG").GetComponent<Button>();

        backBgButton.onClick.AddListener(() => {
            // Remove this
            gameObject.SetActive(false);
            NoticeMgr.isReady = true;
            closeCallback?.Invoke();
            closeCallback = null;

            NoticeMgr.Inst.ShowNotice();
        });
    }

    public void Notice(string _content, string _title = "Notice", Action _closeCallback = null)
    {
        title.text = _title;
        content.text = _content;
        closeCallback = _closeCallback;
    }
}
