using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    Slider soundSlider;
    [SerializeField] GameObject muteImage;

    private void Awake()
    {
        soundSlider = GetComponent<Slider>();
        soundSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void Start()
    {
        soundSlider.value = DataManager.Instance.currentData.volume;
    }

    void ChangeVolume(float value)
    {
        SoundManager.Instance.ChangeVolume(value);
        muteImage.SetActive(value == 0 ? true : false);

        DataManager.Instance.currentData.volume = value;
        DataManager.Instance.SaveData();
    }
}
