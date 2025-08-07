using UnityEngine;
using UnityEngine.SceneManagement;

public class EscToSubOption : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneHistory.Push(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("SubOptionScene");
            string currentScene = SceneManager.GetActiveScene().name;
            SceneHistory.Push(currentScene);  // 현재 씬 저장
            SceneManager.LoadScene("SubOption");  // 옵션 씬으로 이동
        }
    }
}





