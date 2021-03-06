using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool _paused;
    private bool _over;
    private List<EnemyMovement> em;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;

    [SerializeField] private int numberOfEnemiesToKill;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private EnemySpawn enemySpawner;
    [SerializeField] private DrawController dc;
    private void Awake()
    {
        if(Instance)
            Destroy(gameObject);
        Instance = this;
        em = new List<EnemyMovement>();
    }

    public void StartAction()
    {
        StartCoroutine(StartActionRoutine());
    }

    IEnumerator StartActionRoutine()
    {
        dc.StartDrawAnim();
        yield return new WaitForSeconds(1);
        enemySpawner.StartSpawning();
    }
    

    private int enemyDeathCount = 0;

    public void TogglePause()
    {
        _paused = !_paused;
        Time.timeScale = _paused ? 0 : 1;
        pauseMenu.SetActive(_paused);
    }

    public void OnEnemyDied(EnemyMovement en)
    {
        /*
        em.Remove(en);
        enemyDeathCount++;
        if (enemyDeathCount == 6)
        {
            GameObject.Instantiate(bossPrefab);
        }
        */
    }

    public void RegisterEnemy(EnemyMovement en)
    {
        em.Add(en);
    }

    public void OnPlayerDied()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnWin()
    {
       //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
