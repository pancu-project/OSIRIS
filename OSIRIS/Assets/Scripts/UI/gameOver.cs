using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    public void CloseButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MapScene");     
    } 
}
