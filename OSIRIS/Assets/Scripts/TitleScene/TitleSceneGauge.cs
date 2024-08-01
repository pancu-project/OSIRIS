using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneGauge : MonoBehaviour
{
    private Slider slider;
    [SerializeField] float timeSpeed = 0.2f;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value += timeSpeed * Time.deltaTime;

        if (slider.value == 1)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
