using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Player3DScript player;
    public GameObject enemyPrefab;

    //score
    int score = 0;
    public Text scoreText;

    //EnemySpawn
    [SerializeField]
    float enemySpawnInterval = 1;
    float enemySpawnTimer;

    [SerializeField]
    float spawnLevelLimitLeft = -20f;
    [SerializeField]
    float spawnLevelLimitRight = 20f;
    [SerializeField]
    float enemySpawnDistance = 50f;

    // Use this for initialization
    void Start()
    {
        ResetScore();
        enemySpawnTimer = enemySpawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            enemySpawnTimer -= Time.deltaTime;

            if (enemySpawnTimer <= 0)
            {
                enemySpawnTimer = enemySpawnInterval;

                GameObject enemyInstance = Instantiate(enemyPrefab);
                enemyInstance.transform.SetParent(transform);
                enemyInstance.transform.position = new Vector3(
                    Random.Range(spawnLevelLimitLeft, spawnLevelLimitRight),
                    -2.5f,
                    player.transform.position.z + enemySpawnDistance
                    );

                enemyInstance.GetComponent<Enemy>().OnKill += OnEnemyKilled;
                Debug.Log("Jestem w UPDATE Game Controller OnEnemyKilled()");
            }

            //Delete Enemies
            foreach (Enemy enemy in GetComponentsInChildren<Enemy>())
            {
                if (player.transform.position.z >= enemy.transform.position.z + 10f)
                    Destroy(enemy.gameObject);
            }
        }
    }

    void OnEnemyKilled()
    {
        score += 1;
        scoreText.text = "Score: " + score;
        Debug.Log("Jestem w Game Controller OnEnemyKilled() ");
    }

    public void ResetScore()
    {
        //score = 0;
        scoreText.text = "Score: " + score;
        Debug.Log("Jestem w ResetScore");
    }
}
