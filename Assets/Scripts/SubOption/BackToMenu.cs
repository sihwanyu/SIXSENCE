using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

