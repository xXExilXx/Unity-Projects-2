using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    string scene;
    void Start()
    {
        scene = "SampleScene";
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }
}
