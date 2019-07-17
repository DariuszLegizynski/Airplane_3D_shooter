using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileBehaviour : MonoBehaviour {

    public delegate void KilledHandler();
    public event KilledHandler OnKill;

    public GameObject treeDestroyPrefab;
    public GameObject enemyDestroyPrefab;
    public AudioClip audioFallingTree;
    public AudioClip enemyExplosionSound;

    //[SerializeField]
    //float treeFallingDurationSound = 1.2f;
    //[SerializeField]
    //float enemyExplosionDurationSound = 1.2f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.tag == "Obstacle")
        {
            GameObject treeExplosionInstance = Instantiate(treeDestroyPrefab);
            treeExplosionInstance.transform.position = transform.position;

            Destroy(treeExplosionInstance, 1.5f);
            Destroy(gameObject);
            Destroy(otherCollider.gameObject);
        }

        if (otherCollider.tag == "Tree")
        {
            GameObject treeExplosionInstance = Instantiate(treeDestroyPrefab);
            treeExplosionInstance.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(audioFallingTree, transform.position);

            Destroy(treeExplosionInstance, 1.5f);
            //Destroy(audioFallingTree, treeFallingDurationSound);
            Destroy(gameObject);
            Destroy(otherCollider.gameObject);
        }

        if (otherCollider.tag == "Enemy")
        {
            GameObject enemyExplosionInstance = Instantiate(enemyDestroyPrefab);
            enemyExplosionInstance.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(enemyExplosionSound, transform.position);

            Destroy(enemyExplosionInstance, 1.2f);
            //Destroy(enemyExplosionSound, enemyExplosionDurationSound);
            Destroy(gameObject);
            Destroy(otherCollider.gameObject);

            if(OnKill != null)
            {
                OnKill();
            }
        }
    }
}
