using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFlySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField] private GameObject enemyFlyPrefab;
    [SerializeField] private float timeDelay;
    [SerializeField] private Transform firstPos;

    private void Awake()
    {
        LoadPointsSpawner();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawner());
    }

    private void Update()
    {
        timeDelay = Mathf.Max(1.5f, 7f * Mathf.Pow(0.9f, (float)Archery2Manager.Instance.GetCurScore()/5f));
    }

    void LoadPointsSpawner()
    {
        Transform par = transform.Find("SpawnPos");
        foreach (Transform chil in par)
        {
            points.Add(chil);
        }
    }
    public void LoadFirstEnemy()
    {
        Instantiate(enemyFlyPrefab, firstPos.position, Quaternion.identity);
    }

    IEnumerator Spawner()
    {
        int rand;
        int lastRand = 1;
        while (true)
        {
            yield return new WaitForSeconds(timeDelay);
            rand = Random.Range(0, points.Count);
            if(rand == lastRand) rand = Random.Range(0, points.Count);
            lastRand = rand;
            Instantiate(enemyFlyPrefab, points[rand].position, Quaternion.identity);
        }
    }
}
