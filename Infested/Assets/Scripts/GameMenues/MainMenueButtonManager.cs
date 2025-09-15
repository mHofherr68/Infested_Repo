using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenueButtonManager : MonoBehaviour
{
    [Header("Main Navigation Settings")]

    [Space(16)]

    [SerializeField] private string websiteURL;

    [SerializeField] private string gameScene;

    [SerializeField] private string storeScene;

    [SerializeField] private string optionsScene;

    [SerializeField] private string helpsiteURL;

    public void WebsitePressed()
    {
        Application.OpenURL("file://" + Application.dataPath + websiteURL);
    }

    public void LoadGamePressed()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void StorePressed()
    {
        SceneManager.LoadScene(storeScene);    
    }

    public void OptionsPressed()
    {
        SceneManager.LoadScene(optionsScene);
    }

    public void HelpPressed()
    {
        Application.OpenURL("file://" + Application.dataPath + helpsiteURL);
    }

    public void QuitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
