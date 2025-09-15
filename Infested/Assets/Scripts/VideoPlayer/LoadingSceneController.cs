using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class LoadingSceneController : MonoBehaviour
{
    [Header("Loading Scene Settings")]

    [Space(16)]

    public VideoPlayer videoPlayer; // Zieh dein VideoPlayer-Objekt hier rein

    [SerializeField] private string gameScene;

    private AsyncOperation asyncLoad;

    void Start()
    {
        // Szene im Hintergrund laden, aber noch nicht aktiv schalten
        StartCoroutine(LoadLevelAsync(gameScene));// ("Level_1"));

        // Wenn das Video fertig ist, aufrufen
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    IEnumerator LoadLevelAsync(string sceneName)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // wichtig, damit nicht sofort gewechselt wird
        yield return asyncLoad;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Szene aktivieren, wenn Video fertig
        if (asyncLoad != null)
            asyncLoad.allowSceneActivation = true;
    }
}
