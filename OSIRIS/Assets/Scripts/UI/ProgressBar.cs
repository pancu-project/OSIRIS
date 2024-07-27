using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;

    GameObject target;
    Slider slider;
    private float distance;
    private float currentTargetDistance;

    private void Awake()
    {
        distance = endPos.transform.position.x - startPos.transform.position.x;
        slider = GetComponent<Slider>();
        target = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        currentTargetDistance = target.transform.position.x - startPos.transform.position.x;
        
        slider.value = currentTargetDistance / distance;
    }
}
