using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public string desktopScene;
    public string mobileScene;

    void Start()
    {
        string sceneToLoad = "";

        // Check the platform and assign the appropriate scene to load
        if (Application.isMobilePlatform)
        {
            sceneToLoad = mobileScene;
        }
        else
        {
            sceneToLoad = desktopScene;
        }

        // Load the assigned scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
