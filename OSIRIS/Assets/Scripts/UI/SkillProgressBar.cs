using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillProgressBar : MonoBehaviour
{
    private Slider skillSlider;
   

    private void Start()
    {
        skillSlider = GetComponent<Slider>();
        if (skillSlider == null)
        {
            Debug.LogError("Slider component not found on " + gameObject.name);
        }
    }

    public void falseActive()
    {
        //this.gameObject.SetActive(false);
    }

    public void trueActive()
    {

        //this.gameObject.SetActive(true);
    }

    public void StartProgressBar(float duration)
    {
        skillSlider.maxValue = duration;
        skillSlider.value = duration;
        skillSlider.gameObject.SetActive(true);
        StartCoroutine(UpdateProgressBar(duration));
    }

    private IEnumerator UpdateProgressBar(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            skillSlider.value = duration - elapsedTime;
            yield return null;
        }

        EndProgressBar();
    }
    
    private void EndProgressBar()
    {
       skillSlider.gameObject.SetActive(false);
    }

}
