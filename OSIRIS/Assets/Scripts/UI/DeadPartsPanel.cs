using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class DeadParts
{
    public GameObject[] parts;
}

public class DeadPartsPanel : MonoBehaviour
{
    [SerializeField] List<DeadParts> StageParts;

    private void OnEnable()
    {
        for (int i = 0; i < DataManager.Instance.currentData.stageLevel - 1; i++)
        {
            foreach (GameObject part in StageParts[i].parts)
            {
                part.SetActive(true);
            }
        }
    }
}
