using UnityEngine;
using UnityEngine.SceneManagement;

public class scenecontroller1 : MonoBehaviour
{
    public string nextSceneName = "scene2"; // 이동할 씬 이름

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}