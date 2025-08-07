using UnityEngine;
using UnityEngine.SceneManagement;

public class SubOptionUI : MonoBehaviour
{
    public void OnSettingButtonClicked()
    {
        SceneManager.LoadScene("Option");
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("MainGame"); // 또는 돌아갈 씬 이름
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

