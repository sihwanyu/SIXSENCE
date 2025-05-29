using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static DialogueList;

[System.Serializable] //직접 만든 class에 접근할 수 있도록 해줌. 
public class DialogueList
{
    public enum dialoguename
    { 
        청운객,
        흑혈수라
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
    Color next_Color;
    private int count = 0;
    private float printSpeed = 0.025f;
    private bool end = false;
    private bool next = true;
    private float time;

    void Start()
    {
        //Debug.Log(); //Debug
        text = this.GetComponent<TextMeshProUGUI>();
        nametext = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        chat_Box = transform.GetChild(1).gameObject;
        //next_Box = transform.GetChild(2).gameObject;
        //next_Color = next_Box.GetComponent<Image>().color;
        chat_Box.SetActive(false);
        //next_Box.SetActive(false);
    }

    void Update()
    {
        //BlinkAnim();
        if (Input.GetKeyDown(KeyCode.Space) && next)
        {
            next = false;
            if (end)
            {
                EndDialogue();
            }
            else
            {
                text.text = "";
                chat_Box.SetActive(true);
                next_Box.SetActive(false);
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
        if (count < dialogue.Length - 1) count++;
        else end = true;
        next = true;
    }
    public void EndDialogue()
    {
        ResetDialogue();
        StopAllCoroutines();
        Debug.Log("종료"); //Debug
    }
    public void ResetDialogue()
    {
        text.text = "";
        nametext.text = "";
        chat_Box.SetActive(false);
    }
    public void BlinkAnim()
    {
        next_Box.SetActive(true);

        if (time < 0.5f)
        {
            next_Color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            next_Color = new Color(1, 1, 1, time);
            if (time > 1f)
            {
                time = 0;
            }
        }
        time += Time.unscaledDeltaTime;
    }
}