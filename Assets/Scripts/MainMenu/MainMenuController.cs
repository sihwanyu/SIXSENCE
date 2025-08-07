using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void GoToOptionScene()
    {
        SceneManager.LoadScene("Option");
    }
}

