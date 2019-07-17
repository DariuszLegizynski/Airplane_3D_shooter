using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public delegate void KilledHandler();
    public event KilledHandler OnKill;

    [SerializeField]
    float enemySpeed = -1f;


    // Use this for initialization
    void Start ()
    {
        GetComponent<Rigidbody>().velocity = new Vector3 (0f, 0f, enemySpeed);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "PlayerBullet" || otherCollider.tag == "Player")
        {
            Destroy(gameObject);
            Destroy(otherCollider.gameObject);

            if (OnKill != null)
            {
                Debug.Log("Jestem w Enemy -> onKill()");
                OnKill();
            }
        }
    }
}
