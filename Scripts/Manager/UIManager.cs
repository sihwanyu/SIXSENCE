using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingsPanel;

    void Start()
    {
        ShowMain(); // 앱 시작 시 메인 패널 표시
    }

    public void ShowSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void ShowMain()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}

