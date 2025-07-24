using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 참조 오브젝트들
    public Transform enemyContainer;           // 방향별 Enemy 오브젝트들이 들어있는 컨테이너
    public Transform playerContainer;          // 방향별 플레이어 오브젝트들이 들어있는 컨테이너
    public TMP_Text comboText;                 // 화면에 표시되는 콤보 텍스트
    public GameObject GameClearUI;             // 게임 클리어 UI
    public GameObject GameOver_Option;         // 게임 오버 UI
    public GameObject enemyDead;               // 게임 클리어 시 보여줄 Enemy_Dead
    public Image Flash_Effect;                        // 섬광 이펙트용 이미지
    public Image Dead_Effect;                  // 사망 이벡트용 이미지

    // 입력 및 리듬 설정
    public float beatInterval = 5f;            // 한 턴마다의 제한 시간
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    // 공격 방향 시퀀스 (고정)
    private string[] directionSequence = new string[]
    {
        "Up", "Left", "Down", "Right", "Up"
    };

    // 현재 턴의 방향
    private string currentDirection = "";

    // 상태 변수들
    private bool canInput = false;             // 현재 입력 가능한지
    private bool gameover = false;
    private bool gameclear = false;

    // 입력 결과 상태
    private enum Result { None, Success, Fail }
    private Result resultState = Result.None;

    // 콤보 카운터
    private int comboCount = 0;

    // 게임 시작
    void Start()
    {
        comboText.text = "";
        GameClearUI.SetActive(false);

        ShowOnlyPlayerPose(""); // ✅ 기본 손자세 보이기

        StartCoroutine(PlayLoop());
    }


    // 키 입력 처리
    void Update()
    {
        if (!canInput || gameover) return;

        KeyCode correctKey = GetKeyForDirection(currentDirection);

        // 정답 입력 시
        if (Input.GetKeyDown(correctKey))
        {
            resultState = Result.Success;
            canInput = false;

            comboCount++;
            comboText.text = "Combo: " + comboCount;

            // 섬광 이펙트 출력
            StartCoroutine(ShowFlashEffect());

            // 손 포즈 보여주기
            ShowOnlyPlayerPose(currentDirection);
            StartCoroutine(RevertToStandPose());
        }
        // 틀린 키 입력 시
        else if (Input.anyKeyDown)
        {
            resultState = Result.Fail;
            canInput = false;
        }
    }

    // 전체 게임 진행 루프
    IEnumerator PlayLoop()
    {
        for (int i = 0; i < directionSequence.Length; i++)
        {
            currentDirection = directionSequence[i];
            Visible_Enemy(currentDirection);       // 해당 방향 Enemy 표시

            resultState = Result.None;
            canInput = true;

            float timer = 0f;
            while (timer < beatInterval)
            {
                if (resultState == Result.Success)
                    break;

                if (resultState == Result.Fail)
                {
                    StartCoroutine(GameOver());
                    yield break;
                }

                timer += Time.deltaTime;
                yield return null;
            }

            canInput = false;

            if (resultState == Result.None)
            {
                StartCoroutine(GameOver());
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
            Invisible_Enemy();                      // Enemy 비활성화
        }

        // 모든 공격 막기 성공 시
        StartCoroutine(GameClear());
    }

    // 게임 오버 처리
    IEnumerator GameOver()
    {
        if (gameover) yield break;

        gameover = true;
        GameOver_Option.SetActive(true);
        Invisible_Enemy();

        // 어두워지는 이펙트 실행
        StartCoroutine(ShowDeadEffect());

        yield return null;
    }


    // 게임 클리어 처리
    IEnumerator GameClear()
    {
        if (gameclear) yield break;

        gameclear = true;

        ShowOnlyEnemyDead();                    // 죽은 적 연출
        GameClearUI.SetActive(true);            // 클리어 UI 띄우기

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("ChattingScene");
    }

    // 특정 방향 Enemy만 활성화
    void Visible_Enemy(string direction)
    {
        foreach (Transform child in enemyContainer)
            child.gameObject.SetActive(child.name == "Enemy_" + direction);
    }

    // 모든 Enemy 비활성화
    void Invisible_Enemy()
    {
        foreach (Transform child in enemyContainer)
            child.gameObject.SetActive(false);
    }

    // Enemy_Dead만 보여주기
    void ShowOnlyEnemyDead()
    {
        foreach (Transform child in enemyContainer)
        {
            child.gameObject.SetActive(child.name == "Enemy_Dead");
        }
    }

    // 플레이어 사망 이펙트(어두워지는 효과)
    IEnumerator ShowDeadEffect()
    {
        float duration = 1f;
        float alpha = 0f;
        Color color = Dead_Effect.color;

        while (alpha < 0.9f)
        {
            alpha += Time.deltaTime / duration;
            color.a = Mathf.Clamp01(alpha);
            Dead_Effect.color = color;
            yield return null;
        }
    }

    // 섬광 이펙트 연출
    IEnumerator ShowFlashEffect()
    {
        float duration = 0.3f;
        float alpha = 1f;

        Color color = Flash_Effect.color;
        color.a = alpha;
        Flash_Effect.color = color;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / duration;
            color.a = Mathf.Clamp01(alpha);
            Flash_Effect.color = color;
            yield return null;
        }
    }

    // 플레이어 손동작 전환 함수
    void ShowOnlyPlayerPose(string direction)
    {
        foreach (Transform child in playerContainer)
        {
            if (direction == "")
                child.gameObject.SetActive(child.name == "Player_Stand");
            else
                child.gameObject.SetActive(child.name == "Player_" + direction);
        }
    }

    // 플레이어 손동작 기본 상태로 돌려주는 함수
    IEnumerator RevertToStandPose()
    {
        yield return new WaitForSeconds(0.5f);
        ShowOnlyPlayerPose("");
    }



    // 방향 문자열 → 키코드 변환
    KeyCode GetKeyForDirection(string dir)
    {
        return dir switch
        {
            "Up" => upKey,
            "Down" => downKey,
            "Left" => leftKey,
            "Right" => rightKey,
            _ => KeyCode.None
        };
    }

    // 다시하기 버튼에 연결
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 종료하기 버튼에 연결
    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
