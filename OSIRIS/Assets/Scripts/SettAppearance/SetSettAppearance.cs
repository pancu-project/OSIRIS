using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSettAppearance : MonoBehaviour
{
    [SerializeField] List<GameObject> appearSpot; // �ΰ��ӿ��� ��Ʈ�� ��Ÿ������ ��ġ
    [SerializeField] GameObject sett;
    [SerializeField] float gap; // �÷��̾�� appearSpot ���� �Ÿ�

    private GameObject player;
    private SpriteRenderer settRenderer;
    private SetObstacles setObstacle;
    private int index;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        settRenderer = sett.GetComponent<SpriteRenderer>();
        setObstacle = sett.GetComponent<SetObstacles>();
    }

    private void Update()
    {
        if (appearSpot.Count <= index) return;

        float playerSpotGap = appearSpot[index].transform.position.x - player.transform.position.x;
        if ( 0 < playerSpotGap && playerSpotGap < gap && !settRenderer.enabled)
        {
            sett.GetComponent<SetObstacles>().idx = index;
            settRenderer.enabled = true;
            setObstacle.enabled = true;
            index++;
        }
    }
}
