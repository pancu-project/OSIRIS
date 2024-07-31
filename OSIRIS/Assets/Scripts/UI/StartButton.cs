using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public void StartButtonClicked()
    {
        SceneManager.LoadScene("MapScene");
    }
}
