using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    [SerializeField] GameObject blurBackground;
    SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (sprite.enabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.stage = gameObject.name;
                SceneManager.LoadScene("PlayerSelectScene");
            }
        }
    }

    private void OnMouseEnter()
    {
        blurBackground.SetActive(true);
        sprite.enabled = true;
    }

    private void OnMouseExit()
    {
        blurBackground.SetActive(false);
        sprite.enabled = false;
    }
}
