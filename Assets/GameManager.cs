using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private List<EnemyMovement> em;
    private void Awake()
    {
        if(Instance)
            Destroy(gameObject);
        Instance = this;
        em = new List<EnemyMovement>();
    }

    public void OnEnemyDied(EnemyMovement en)
    {
        em.Remove(en);
        if (em.Count == 0)
        {
            OnWin();
        }
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
