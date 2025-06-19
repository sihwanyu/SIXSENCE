using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonHandler : MonoBehaviour
{
    // 전환할 씬 이름

    // 버튼 클릭 시 호출할 메서드
    public void StartButtonClicked()
    {
        SceneManager.LoadScene("StoryScene");
    }
}
