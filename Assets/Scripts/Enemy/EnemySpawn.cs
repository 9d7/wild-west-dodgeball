using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject boss;
    public int xPos;
    public int yPos;
    private int enemyCount;
    public int enemyMaxCount;
    public int totalEnemyBeforeBoss = 10;
    public float spawnInterval = 1f;
    private bool spawning = false;
    private bool bossSpawn = false;

    public int enemyGroupNum = 3;
    public int enemyRound = 3;

    private Vector3 groupPos;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = 0;
        //StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyRound > 0)
        {
            if ((enemyCount == 0) && (!spawning))
            {
                enemyRound--;
                StartCoroutine(Spawn());
            }
        } else
        {
            if (!bossSpawn)
            {
                bossSpawn = true;
                spawnBoss();
            }
        }
    }

    void spawnBoss()
    {
        Debug.Log("BOSS");
        xPos = Random.Range(0, 20);
        yPos = Random.Range(0, 5);
        Instantiate(boss, new Vector3(xPos, yPos, 0), Quaternion.identity);
    }

    IEnumerator Spawn()
    {
        spawning = true;
        xPos = Random.Range(-20, 40);
        yPos = Random.Range(-5, 10);
        groupPos = new Vector3(xPos, yPos, 0);
        for(int i = 0; i < enemyGroupNum; i++)
        {
            Instantiate(enemy, groupPos + new Vector3(Random.value, Random.Range(-4,4)), Quaternion.identity);
            enemyCount += 1;
        }
        yield return new WaitForSeconds(spawnInterval);
        spawning = false;
    }

    public void enemyDied()
    {
        //Debug.Log(enemyCount);
        enemyCount -= 1;
    }
}
