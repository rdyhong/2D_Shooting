using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Content_RoomPlayerInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTmp;
    [SerializeField] TextMeshProUGUI readyTmp;
    [SerializeField] GameObject readyObj;

    public void Init(Photon.Realtime.Player _player)
    {
        Util.WidthMatchToParent(transform.GetComponent<RectTransform>(), transform.parent.parent.GetComponent<RectTransform>());

        if(_player.IsMasterClient)
        {
            readyTmp.text = "Master";
            readyObj.SetActive(true);
        }
        else
        {
            readyObj.SetActive(false);
        }

        nameTmp.text = _player.NickName;
    }

    public void Recycle()
    {
        ObjectPool.Recycle(this);
    }
}
