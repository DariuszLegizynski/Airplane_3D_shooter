﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponFireController : MonoBehaviour
{
    public AudioClip audioShot;

    [Header("Shooting")]
    public int gunAmmo;
    public float shootStartTime;
    public float shootInterval;
    public float gunShotRange;

    bool fired;

    public Transform gun;

    public GameObject Player3D;
    public Rigidbody player3DRigidbody;
    public Player3DScript playerScript;

    public ObjectPool projectilePool;

    float player3DSpeed;

    // Use this for initialization
    void Start ()
    {
        player3DRigidbody = Player3D.GetComponent<Rigidbody>();

        InvokeRepeating("PlayerGunFire", shootStartTime, shootInterval);
    }

    //Fire PlayerGun
    public void PlayerGunFire()
    {
        if (fired == true)
        {
            GameObject bullet = projectilePool.GetObject();

            bullet.transform.SetParent(transform.parent);

            AudioSource.PlayClipAtPoint(audioShot, transform.position);

            //to calculate the players velocity
            float playersVelocity = playerScript.currentVel.z * 50f;

            bullet.transform.position = transform.position;
            //bullet.transform.rotation = gun.rotation;

            bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, gunShotRange + playersVelocity);

            /*
            //other solution for shooting
            
            GameObject gunShotInstance = Instantiate(ShotPrefab, gun.position, gun.rotation);
            gunShotInstance.transform.SetParent(transform.parent);

            AudioSource.PlayClipAtPoint(audioShot, transform.position);

            float playersVelocity = playerScript.currentVel.z * 50f;

            gunShotInstance.transform.position = transform.position;
            gunShotInstance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, gunShotRange + playersVelocity);
            
            Destroy(gunShotInstance, 3f);
            */
        }
    }

    public void FireButton(bool _fired)
    {
        fired = _fired;
    }
}
