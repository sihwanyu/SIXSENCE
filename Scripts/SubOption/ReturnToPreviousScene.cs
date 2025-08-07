using UnityEngine;
using UnityEngine.SceneManagement;

public class SubOptionBackButton : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToPreviousScene();
        }
    }

    public void ReturnToPreviousScene()
    {
        string previousScene = SceneHistory.Pop();
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("이전 씬 정보가 없습니다.");
        }
    }
}




