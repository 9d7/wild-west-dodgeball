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
    
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = 0;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while (enemyCount < enemyMaxCount)
        {
            xPos = Random.Range(-30, 30);
            yPos = Random.Range(-7, 10);
            Instantiate(enemy, new Vector3(xPos, yPos, 0), Quaternion.identity);
            yield return new WaitForSeconds(1);
            enemyCount += 1;
        }
    }
}
