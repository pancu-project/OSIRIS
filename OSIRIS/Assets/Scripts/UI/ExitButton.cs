using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour
{
    public void OnExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        // Application.Quit();
    }
}
