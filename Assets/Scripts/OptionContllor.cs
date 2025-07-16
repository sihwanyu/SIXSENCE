using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionContllor : MonoBehaviour
{
    private bool OnOff = true;
    GameObject op;
    void Start()
    {
        op = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OnOff)
            {
                op.SetActive(true);
                Time.timeScale = 0;
                OnOff = false;
            }
            else
            {
                op.SetActive(false);
                Time.timeScale = 1;
                OnOff = true;
            }
        }
        else return;
    }
}
