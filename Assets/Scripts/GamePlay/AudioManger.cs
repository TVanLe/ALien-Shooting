using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour
{   
    [SerializeField]
    private AudioClip targetExplosion = default;
    [SerializeField]
    private AudioClip gameOver = default;
    [SerializeField]
    private AudioClip gameIntro = default;
    [SerializeField]
    private AudioClip targetSpawn = default;
    [SerializeField]
    private AudioClip targetAttack = default;

    private AudioSource audioSource;

    public static AudioManger Instance;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    
    public void NewGame()
    {
        audioSource.PlayOneShot(gameIntro);
    }

    public void EnemyDie()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f) + GameManager.Instance.GetCurScore() * 0.01f;
        audioSource.PlayOneShot(targetExplosion);
    }
    
    public void EnemyFlyDie()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f) + Archery2Manager.Instance.GetCurScore() * 0.01f;
        audioSource.PlayOneShot(targetExplosion);
    }
    
    public void GameLost()
    {
        audioSource.pitch = Random.Range(0.98f, 1.02f);
        audioSource.PlayOneShot(gameOver);
    }

    public void EnemySpawner()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(targetSpawn, 0.5f);
    }

    public void EnenmyStartToAttack()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(targetAttack, 0.5f);
    }
}
