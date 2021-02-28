using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIController : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject halfHeartPrefab;
    [SerializeField] private float HealthPerHeart = 20;
    private float _health;

    public void RenderHealth(float newHealth)
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        int fullHearts = (int)(newHealth / HealthPerHeart);
        for(int i = 0; i < fullHearts; i++)
        {
            GameObject.Instantiate(heartPrefab, transform);
        }
        if(newHealth - (fullHearts * HealthPerHeart) > 10)
        {
            GameObject.Instantiate(halfHeartPrefab, transform);
        }
    }

}
