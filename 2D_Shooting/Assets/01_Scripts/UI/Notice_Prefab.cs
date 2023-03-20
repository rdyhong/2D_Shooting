using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notice_Prefab : MonoBehaviour
{
    private Button backBgButton;
    private TextMeshProUGUI title;
    private TextMeshProUGUI content;

    private void Awake()
    {
        title = transform.Find("BG/Title").GetComponent<TextMeshProUGUI>();
        content = transform.Find("BG/Content").GetComponent<TextMeshProUGUI>();

        backBgButton = transform.Find("BackBG").GetComponent<Button>();

        backBgButton.onClick.AddListener(() => {
            // Remove this
            gameObject.SetActive(false);
            NoticeManager.isReady = true;
        });
    }

    public void Notice(string _content, string _title = "Notice")
    {
        title.text = _title;
        content.text = _content;
    }
}
