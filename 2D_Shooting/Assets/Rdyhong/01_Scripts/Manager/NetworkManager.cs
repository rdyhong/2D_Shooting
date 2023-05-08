using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Singleton<NetworkManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    // Check Internet Connection
    public static bool CheckInternetConnect(bool _addNotice = false)
    {
        bool _isConnect = false;
        string _content = string.Empty;
        
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            _content = "Disconnected";
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            _content = "Data Connected";
            _isConnect = true;
        }
        else
        {
            _content = "WIFI Connected";
            _isConnect = true;
        }

        if (_addNotice)
        {
            NoticeManager.Instance.AddNotice(_content, "Network");
        }

        if(!_isConnect) Debug.LogError(_content);

        return _isConnect;
    }
}
