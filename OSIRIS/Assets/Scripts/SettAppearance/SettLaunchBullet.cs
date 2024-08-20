using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettLaunchBullet : MonoBehaviour
{
    [SerializeField] float launchSpeed = 2f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    private void OnEnable()
    {
        Invoke("Launch", 2f);
    }

    private void Launch()
    {
        Instantiate(bullet, gun.position, transform.rotation);
        launchSpeed = Random.Range(.5f, 3f);

        if (!this.enabled) return;

        Invoke("Launch", launchSpeed);
    }
}