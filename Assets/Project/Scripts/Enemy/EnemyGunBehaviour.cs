﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunBehaviour : MonoBehaviour {

    public GameObject bulletPrefab;

    [SerializeField]
    float shootingInterval = 1.5f;
    [SerializeField]
    float bulletSpreadXMin = 0.2f;
    [SerializeField]
    float bulletSpreadXMax = 6f;
    [SerializeField]
    float bulletSpreadYMin = 0.2f;
    [SerializeField]
    float bulletSpreadYMax = 2f;
    [SerializeField]
    float bulletSpeedZ = 5f;
    [SerializeField]
    float bulletLifeTime = 4f;

    float shootingTimer;
    float bulletSpreadX;
    float bulletSpreadY;

    public AudioClip enemyProjectileShotSound;

    //[SerializeField]
    //float enemyProjectileDurationSound = 3f;

    // Use this for initialization
    void Start ()
    {
        shootingTimer = Random.Range(0f, shootingInterval);
    }
	
	// Update is called once per frame
	void Update ()
    {
        shootingTimer -= Time.deltaTime;
        bulletSpreadX = Random.Range(bulletSpreadXMin, bulletSpreadXMax);
        bulletSpreadY = Random.Range(bulletSpreadYMin, bulletSpreadYMax);

        if (shootingTimer <= 0f)
        {
            shootingTimer = shootingInterval;

            GameObject bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.transform.SetParent(transform.parent);
            bulletInstance.transform.position = transform.position;
            bulletInstance.GetComponent<Rigidbody>().velocity = new Vector3(bulletSpreadX, bulletSpreadY, -bulletSpeedZ);
            AudioSource.PlayClipAtPoint(enemyProjectileShotSound, transform.position);
            Destroy(bulletInstance, bulletLifeTime);
        }
    }
}
