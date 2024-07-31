using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    ButtonSpriteActive button;

    private void Start()
    {
        button = GetComponent<ButtonSpriteActive>();
    }

    public void CloseButtonClicked()
    {
        button.SetBasicImage();
    }
}
