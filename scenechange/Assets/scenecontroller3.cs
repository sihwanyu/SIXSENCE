using UnityEngine;
using UnityEngine.SceneManagement;

public class scenecontroller3 : MonoBehaviour
{
    public string nextSceneName = "scene1"; // 이동할 씬 이름

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}