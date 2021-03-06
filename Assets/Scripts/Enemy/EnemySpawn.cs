using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject boss;
    public float xPos;
    public float yPos;
    private int enemyCount;
    public int enemyMaxCount;
    public int enemyMinCount;
    public int totalEnemyBeforeBoss = 10;
    public float spawnInterval = 1f;
    private bool spawning = false;
    private bool bossSpawn = false;
    [SerializeField] private RectTransform spawnRange;

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
        //Instantiate(boss, new Vector3(xPos, yPos, 0), Quaternion.identity);
    }

    IEnumerator Spawn()
    {
        spawning = true;
        xPos = Random.Range(spawnRange.anchorMin.x, spawnRange.anchorMax.x);
        yPos = Random.Range(spawnRange.anchorMin.y, spawnRange.anchorMax.y);
        groupPos = new Vector3(xPos, yPos, 0);
        int count = Random.Range(enemyMinCount, enemyMaxCount);
        for(int i = 0; i < count; i++)
        {
            Instantiate(enemy, groupPos + new Vector3(Random.value, Random.Range(-2,2)), Quaternion.identity);
            enemyCount += 1;
        }
        yield return new WaitForSeconds(spawnInterval);
        spawning = false;
    }

    public void enemyDied()
    {
        enemyCount -= 1;
    }
}
