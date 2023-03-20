using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableManager : Singleton<AddressableManager>
{
    public void LoadAsset<T>(string address, T ret)
    {
        //Addressables.InstantiateAsync(address);
        Addressables.LoadAssetAsync<T>(address).Completed += handle => {
            //result = handle.Result;
            ret = handle.Result;
        };

        /*
        Addressables.InstantiateAsync<T>(address).Completed += handle =>
        {
            ret = handle.Result;

        };
        */
        
        //return _result;
    }

}
