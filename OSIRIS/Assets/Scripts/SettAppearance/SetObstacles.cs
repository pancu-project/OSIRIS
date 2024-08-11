using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Obstacles
{
    public GameObject[] obstacle;
}

public class SetObstacles : MonoBehaviour
{
    [SerializeField] List<Obstacles> obstacles; // 세트 출현 시 활성화 되어야할 장애물들
    private Rigidbody2D rigid;
    private Vector3 originalLocation; // 장애물의 초기 위치 값 저장
    public int idx { get; set; }

    private void OnEnable()
    {
        if (obstacles[idx] == null) return;

        foreach (GameObject obstacle in obstacles[idx].obstacle)
        {
            originalLocation = obstacle.transform.position;

            if (obstacle.activeSelf)
            {
                rigid = obstacle.GetComponent<Rigidbody2D>();
                SetObjectDeActivate(obstacle);
            }
            else
            {
                obstacle.SetActive(true);
                StartCoroutine(SetGameObjectLocation(obstacle));
            }
        }
    }

    private void SetObjectDeActivate(GameObject obstacle)
    {
        rigid.gravityScale = 1f;
        StartCoroutine(SetGameObjectActive(obstacle));
    }

    IEnumerator SetGameObjectActive(GameObject obstacle)
    {
        yield return new WaitForSeconds(1f);

        rigid.gravityScale = 0f;
        obstacle.SetActive(false);

        StartCoroutine(SetGameObjectLocation(obstacle));
    }

    IEnumerator SetGameObjectLocation(GameObject obstacle)
    {
        yield return new WaitForSeconds(5f);

        obstacle.transform.position = originalLocation;
        obstacle.SetActive(!obstacle.activeSelf);
    }
}
