using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

[System.Serializable]
public class Dialogues
{
    public string name;
    public string d_Text;
}

[System.Serializable]
public class DialoguesList
{
    public Dialogues[] dialogues;
}

public class StorySystem : MonoBehaviour
{
    public TextMeshProUGUI _nameText;
    public TextMeshProUGUI _dialogueText;
    public GameObject _nextBox;
    public GameObject _skip;
    public GameObject _chatBox; 
    public GameObject _npc;
    public TextAsset _jsonFile;
    public SceneAsset _nextScene;

    private DialoguesList dialoguesList;
    private int dialogueIndex = 0;
    private string json;
    private string scene;
    private float printSpeed = 0.025f;
    private bool end = false;
    private bool next = true;
    public void Awake()
    {
        json = _jsonFile.name;
        scene = _nextScene.name;
    }
    public void Start()
    {
        //LoadJson("Story1");//JSON파일 이름 입력
        LoadJson(json);
        StartUi();
        NpcSystem();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && next)
        {
            _nextBox.SetActive(false);
            next = false;
            if (end)
            {
                EndDialogue();
            }
            else
            {
                _dialogueText.text = "";
                _chatBox.SetActive(true);
                _skip.SetActive(true);
                StartCoroutine(TextPrint(printSpeed));
            }
        }

    }

    public void LoadJson(string jsonFileName)
    {
        TextAsset jsonText = Resources.Load<TextAsset>(jsonFileName);

        if (jsonText != null)
        {
            string jsonString = jsonText.text;
            dialoguesList = JsonUtility.FromJson<DialoguesList>(jsonString);
        }
    }
    public void NpcSystem()
    {
        if (json == "Story1")
        {
            _npc.SetActive(false);
        }
    }
    IEnumerator TextPrint(float d)
    {
        int corou_count = 0;
        Dialogues dialogues = dialoguesList.dialogues[dialogueIndex];
        _nameText.text = dialogues.name;
        while (corou_count != dialogues.d_Text.Length)
        {
            if (corou_count < dialogues.d_Text.Length)
            {
                _dialogueText.text += dialogues.d_Text[corou_count].ToString();
                corou_count++;
            }
            yield return new WaitForSeconds(printSpeed);
        }
        _nextBox.SetActive(true);
        if (dialogueIndex < dialoguesList.dialogues.Length - 1) dialogueIndex++;
        else end = true;
        next = true;
    }
    public void StartUi()
    {
        _chatBox.SetActive(false);
        _nextBox.SetActive(false);
        _skip.SetActive(false);
        _npc.SetActive(false);
    }
    // 스킵 버튼 누를 시 게임 씬으로 이동
    public void EndDialogue()
    {
        ResetDialogue();
        StopAllCoroutines();
        SceneManager.LoadScene(scene);
    }

    public void ResetDialogue()
    {
        _dialogueText.text = "";
        _nameText.text = "";
        _chatBox.SetActive(false);
        _nextBox.SetActive(false);
        _skip.SetActive(false);
        _npc.SetActive(false);
    }
}
