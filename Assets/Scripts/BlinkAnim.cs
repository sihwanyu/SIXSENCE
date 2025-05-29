using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnim : MonoBehaviour
{
    float time;
    void Update()
    {
        if (time < 0.8f)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            GetComponent<Image>().color = new Color(1, 1, 1, time);
            if (time > 1.5f)
            {
                time = 0;
            }
        }

        time += Time.unscaledDeltaTime;

    }
}
