using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static DialogueList;

[System.Serializable] //직접 만든 class에 접근할 수 있도록 해줌. 
public class DialogueList
{
    public enum dialoguename
    { 
        청운객,
        흑혈수라,
        나레이션
    }
    [SerializeField]
    public dialoguename names;
    [TextArea]
    public string dialogue;
}
public class Dialogue : MonoBehaviour
{
    public DialogueList[] dialogue;
    TextMeshProUGUI text;
    TextMeshProUGUI nametext;
    GameObject chat_Box;
    GameObject next_Box;
    GameObject skip_Box;
    GameObject enemy_Image;
    private int count = 0;
    private float printSpeed = 0.025f;
    private bool end = false;
    private bool next = true;

    void Start()
    {
        //Debug.Log(); //Debug
        text = this.GetComponent<TextMeshProUGUI>();
        nametext = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        enemy_Image = GameObject.Find("EnemyImage");
        chat_Box = transform.GetChild(1).gameObject;
        next_Box = transform.GetChild(2).gameObject;
        skip_Box = transform.GetChild(3).gameObject;
        StartBox();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && next)
        {
            next_Box.SetActive(false);
            next = false;
            if (end)
            {
                EndDialogue();
            }
            else
            {
                text.text = "";
                chat_Box.SetActive(true);
                skip_Box.SetActive(true);
                StartCoroutine(TextPrint(printSpeed));
            }
        }
    }
    IEnumerator TextPrint(float d)
    {
        int corou_count = 0;
        nametext.text = dialogue[count].names.ToString();
        while (corou_count != dialogue[count].dialogue.Length)
        {
            if (corou_count < dialogue[count].dialogue.Length)
            {
                text.text += dialogue[count].dialogue[corou_count].ToString();
                corou_count++;
            }
            yield return new WaitForSeconds(printSpeed);
        }
        next_Box.SetActive(true);
        if (count < dialogue.Length - 1) count++;
        else end = true;
        next = true;
    }

// 스킵 버튼 누를 시 게임 씬으로 이동
    public void EndDialogue()
    {
        ResetDialogue();
        StopAllCoroutines();
        SceneManager.LoadScene("GameScene");
    }


    public void ResetDialogue()
    {
        text.text = "";
        nametext.text = "";
        chat_Box.SetActive(false);
        next_Box.SetActive(false);
        skip_Box.SetActive(false);
        enemy_Image.SetActive(false);
    }
    public void StartBox()
    {
        chat_Box.SetActive(false);
        next_Box.SetActive(false);
        skip_Box.SetActive(false);
    }
}