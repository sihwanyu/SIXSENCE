using UnityEngine;
using UnityEngine.SceneManagement;

public class scenecontroller2 : MonoBehaviour
{
    public string nextSceneName = "scene3"; // 이동할 씬 이름

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}