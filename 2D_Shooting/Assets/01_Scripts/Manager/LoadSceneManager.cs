using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    [SerializeField]
    public Image bloackPanel;

    public string nextScene;
    public static bool rdyToLoadNextScene = false;
    private bool isOnLoad = false;

    public void LoadScene(string sceneName)
    {
        if (isOnLoad) return;

        isOnLoad = true;
        nextScene = sceneName;

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        // Wait until load scene
        while (!op.isDone)
        {
            yield return null;
            if (op.progress >= 0.9f)
            {
                Debug.Log($"{nextScene} ::: Load Scene Complete");
                break;
            }
        }

        // Wait for another job befor load scene
        yield return new WaitUntil(() => rdyToLoadNextScene);

        Debug.Log($"{nextScene} _ Scene Activate");
        rdyToLoadNextScene = false;
        op.allowSceneActivation = true;
        isOnLoad = false;

        yield return new WaitForSeconds(0.5f);

        bloackPanel.DOFade(0, 1);
    }
}
