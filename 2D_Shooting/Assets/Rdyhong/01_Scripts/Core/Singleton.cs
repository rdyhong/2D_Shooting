using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    private static object syncRoot = new Object();

    private const string parentName = "DontDestroyObject";

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();
                    }
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(this.gameObject);
        }

        // Move singleton object to DontDeatroyObject child

        GameObject dontDesObj = GameObject.Find(parentName);

        if (dontDesObj == null)
        {
            dontDesObj = new GameObject(parentName);
            DontDestroyOnLoad(dontDesObj);
        }

        transform.SetParent(dontDesObj.transform);

        Debug.Log($"{transform.name} ::: Singleton Awake");
    }
}
