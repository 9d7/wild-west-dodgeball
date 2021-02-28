using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public int xPos;
    public int yPos;
    private int enemyCount;
    public int enemyMaxCount;
    public float spawnInterval = 1f;
    private bool spawning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = 0;
        //StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if((enemyCount < enemyMaxCount) && (!spawning))
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        spawning = true;
        xPos = Random.Range(-20, 40);
        yPos = Random.Range(-5, 10);
        Instantiate(enemy, new Vector3(xPos, yPos, 0), Quaternion.identity);
        enemyCount += 1;
        yield return new WaitForSeconds(spawnInterval);
        spawning = false;
    }

    public void enemyDied()
    {
        Debug.Log(enemyCount);
        enemyCount -= 1;
    }
}
