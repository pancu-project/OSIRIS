using System.Collections;
using UnityEngine;

public class CameraShakeEffect : MonoBehaviour
{
    [Header("Set Color Lerp")]
    [SerializeField] float duration = 5f;
    [SerializeField] float smoothness = 0.02f;

    [Header("Set Camera Shaking")]
    [SerializeField] private float m_roughness; //거칠기 정도
    [SerializeField] private float m_magnitude; //움직임 범위

    GameObject mainCamera;
    GameObject player;
    SpriteRenderer settRenderer;
    SetObstacles setObstacle;
    private bool isCameraShaking;

    private void Awake()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
        settRenderer = GetComponent<SpriteRenderer>();
        setObstacle = GetComponent<SetObstacles>();
    }

    private void Update()
    {
        if (settRenderer.enabled && !isCameraShaking)
        {
            isCameraShaking = true;

            StartCoroutine(Shake(1.5f));
            settRenderer.color = Color.white;
        }
    }

    IEnumerator Shake(float duration)
    {
        float halfDuration = duration / 2;
        float elapsed = 0f;
        float tick = Random.Range(-7f, 7f);

        // 게임 오브젝트의 원래 위치 저장
        Vector3 cameraOriginalPosition = mainCamera.transform.position; // 카메라
        Vector3 settOriginalPosition = transform.position; // 세트

        float cameraPlayerGap = cameraOriginalPosition.x - player.transform.position.x;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime / halfDuration;

            tick += Time.deltaTime * m_roughness;
            mainCamera.transform.position = cameraOriginalPosition + new Vector3(
                Mathf.PerlinNoise(tick, 0) - .5f,
                Mathf.PerlinNoise(0, tick) - .5f,
                0f) * m_magnitude * Mathf.PingPong(elapsed, halfDuration);

            // 흔들림 효과로 카메라 속도가 느려짐에 따른 플레이어 위치 설정
            player.transform.position = new Vector3(cameraOriginalPosition.x - cameraPlayerGap, player.transform.position.y);
            transform.position = settOriginalPosition;

            yield return null;
        }

        StartCoroutine("LerpColor");
    }

    IEnumerator LerpColor()
    {
        yield return new WaitForSeconds(1.5f);

        float progress = 0;
        float increment = smoothness / duration;

        while (progress < 1)
        {
            settRenderer.color = Color.Lerp(Color.white, Color.clear, progress);
            progress += increment;

            yield return new WaitForSeconds(smoothness);
        }

        settRenderer.enabled = false;
        setObstacle.enabled = false;
        isCameraShaking = false;
    }
}