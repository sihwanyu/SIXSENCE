using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    public void OnSettingButtonClicked()
    {
        SceneManager.LoadScene("Option");
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Loby");  // 필요시 다른 씬 이름으로 변경
    }

    public void OnQuitButtonClicked()
    {
        // 기존: Application.Quit();
        // 새로: 메인 메뉴로 이동
        SceneManager.LoadScene("MainMenu");
    }
}

