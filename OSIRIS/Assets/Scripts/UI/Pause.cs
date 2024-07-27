using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool isTimeFlow = true;

    public void PauseButtonClicked()
    {
        if (isTimeFlow)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        isTimeFlow = !isTimeFlow;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isTimeFlow)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }

            isTimeFlow = !isTimeFlow;
        }
    }
}
