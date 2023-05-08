using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : Singleton<FirebaseManager>
{
    DatabaseReference m_Reference;

    protected override void Awake()
    {
        base.Awake();

        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Start()
    {
        

        //WriteUserData("goni", "bbbb");
        //WriteUserData("1", "bbbb");
        //WriteUserData("2", "cccc");
    }

    public void ReadUserData()
    {
        if (!NetworkManager.CheckInternetConnect()) return;

        FirebaseDatabase.DefaultInstance.GetReference("users")
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    for (int i = 0; i < snapshot.ChildrenCount; i++)
                        Debug.Log(snapshot.Child(i.ToString()).Child("username").Value);

                }
            });
    }

    public void WriteUserData(string userId, string username)
    {
        if(!NetworkManager.CheckInternetConnect()) return;

        Debug.Log($"{userId} ::: {username}");

        m_Reference.Child("Users").Child(userId).Child("username").SetValueAsync(username);
    }
}
