using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSettAppearance : MonoBehaviour
{
    [SerializeField] List<GameObject> appearSpot; // 인게임에서 세트가 나타나야할 위치
    [SerializeField] GameObject sett;
    [SerializeField] float gap; // 플레이어와 appearSpot 사이 거리

    private GameObject player;
    private SpriteRenderer settRenderer;
    private SetObstacles setObstacle;
    private CameraShakeEffect settAppearTime;

    private int index;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        settRenderer = sett.GetComponent<SpriteRenderer>();
        setObstacle = sett.GetComponent<SetObstacles>();
        settAppearTime = sett.GetComponent<CameraShakeEffect>();
    }

    private void Update()
    {
        if (appearSpot.Count <= index) return;

        float playerSpotGap = appearSpot[index].transform.position.x - player.transform.position.x;

        if ( 0 < playerSpotGap && playerSpotGap < gap && !settRenderer.enabled)
        {
            SoundManager.Instance.PlaySFXSound("Sett");

            StartCoroutine(SetSettAppearTime(index));
            sett.GetComponent<SetObstacles>().idx = index;
            settRenderer.enabled = true;
            setObstacle.enabled = true;
            index++;
        }
    }

    IEnumerator SetSettAppearTime(int index)
    {

        float time = 10f;

        if (index == 1 || index == 2)
        {
            time = 10f;
            sett.GetComponent<SettLaunchBullet>().enabled = true;
            settAppearTime.colorLerpDuration = time;
        }

        yield return new WaitForSeconds(time);

        settAppearTime.colorLerpDuration = 1.5f;
        sett.GetComponent<SettLaunchBullet>().enabled = false;
    }
}
