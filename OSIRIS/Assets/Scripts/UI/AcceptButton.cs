using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptButton : MonoBehaviour
{
    public void AcceptButtonClicked()
    {
        DataManager.Instance.DataClear();
    }
}
