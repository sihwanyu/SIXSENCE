using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoLoby : MonoBehaviour
{
    public void OnSettingButtonClicked()
    {
        SceneManager.LoadScene("Option");
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Loby");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}


