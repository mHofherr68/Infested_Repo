using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreMenueManager : MonoBehaviour
{
    [Header("Store Navigation Settings")]

    [Space(16)]

    [SerializeField] private string backToScene;

    [SerializeField] private string websiteURL;

    public void WebsitePressed()
    {
        Application.OpenURL("file://" + Application.dataPath + websiteURL);
    }

    public void BackPressed()
    {
        SceneManager.LoadScene(backToScene);
    }
}
