using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform enemyContainer;
    public TMP_Text comboText;
    private int comboCount = 0;
    public GameObject GameOverUI;
    public GameObject GameClearUI;

    public float beatInterval = 5f;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    private string[] directions = { "Up", "Left", "Down", "Right" };
    private string currentDirection = "";

    private bool canInput = false;
    private bool gameover = false;
    private bool gameclear = false;

    private enum Result { None, Success, Fail }
    private Result resultState = Result.None;

    void Start()
    {
        comboText.text = "";
        GameOverUI.SetActive(false);
        GameClearUI.SetActive(false);
        StartCoroutine(PlayLoop());
    }

    void Update()
    {
        Debug.Log(gameclear);
        if (!canInput || gameover) return;

        KeyCode correctKey = GetKeyForDirection(currentDirection);


        if (Input.GetKeyDown(correctKey))
        {
            resultState = Result.Success;
            canInput = false;

            comboCount++;
            comboText.text = "Combo: " + comboCount;

            Debug.Log("성공 입력: " + correctKey);
        }

        else if (Input.anyKeyDown && !Input.GetKeyDown(correctKey))
        {
            resultState = Result.Fail;
            canInput = false;

            Debug.Log("틀린 키 입력");
        }
    }

    IEnumerator PlayLoop()
    {
        while (!gameover || !gameclear)
        {
            currentDirection = directions[Random.Range(0, directions.Length)];
            Visible_Enemy(currentDirection);

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
                Debug.Log("실패: 시간 초과");
                StartCoroutine(GameOver());
                yield break;
            }

            if(comboCount >= 5)
            {
                StartCoroutine(GameClear());
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
            Invisible_Enemy();
        }
    }

    IEnumerator GameOver()
    {
        if (gameover) yield break;

        gameover = true;
        GameOverUI.SetActive(true);
        Invisible_Enemy();
        yield return null;
    }

    IEnumerator GameClear()
    {
        if (gameclear) yield break;
        gameclear = true;
        
        GameClearUI.SetActive(true);
        Invisible_Enemy();
        SceneManager.LoadScene("ChattingScene");
    }

    void Visible_Enemy(string direction)
    {
        foreach (Transform child in enemyContainer)
        {
            child.gameObject.SetActive(child.name == "Enemy_" + direction);
        }
    }

    void Invisible_Enemy()
    {
        foreach (Transform child in enemyContainer)
        {
            child.gameObject.SetActive(false);
        }
    }

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
    public void Regame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
