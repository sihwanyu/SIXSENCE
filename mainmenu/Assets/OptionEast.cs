using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionEast : MonoBehaviour
{
    // 전환할 씬 이름
    public string Option;

    // 버튼 클릭 시 호출할 메서드
    public void SettingButtonClicked()
    {
        SceneManager.LoadScene("Option");
    }
}