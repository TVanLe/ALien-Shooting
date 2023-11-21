using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> Points;
    [SerializeField] private GameObject enemyPrefab;

    private float timeDelay;
    private void Awake()
    {
        LoadPointsToSpawner();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawner());

    }
    
    public void LoadFirstEnemy()
    {
        Vector3 firstEnePos = Points[0].position;
        GameObject gameObj = Instantiate(enemyPrefab, firstEnePos, Quaternion.identity);
    }
    private void LoadPointsToSpawner()
    {
        foreach (Transform point in transform)
        {
            Points.Add(point);
        }
    }

    private void Update()
    {
        //Update Time Delay shorter
        timeDelay = Mathf.Max(0.7f, 4f * Mathf.Pow(0.9f, (float)GameManager.Instance.GetCurScore()/3f));
        
    }

    IEnumerator Spawner()
    {
        while (CanSpawner())
        {
            yield return new WaitForSeconds(timeDelay);
            int rand = Random.Range(0, Points.Count);
            Instantiate(enemyPrefab, Points[rand].position, Quaternion.identity);
        }
    }

    bool CanSpawner()
    {
        return true;
    }
}
