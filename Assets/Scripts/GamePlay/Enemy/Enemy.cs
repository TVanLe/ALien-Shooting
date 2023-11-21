using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    [SerializeField] GameObject deadExplosion;
    private Rigidbody2D m_rb;
    private Animator m_anim;
    private Player m_player;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_player = FindObjectOfType<Player>();
    }

    bool AllowShoot() => !GameManager.Instance.IsWaitingForFirstTargetShot;
    private void Start()
    {
        if(AllowShoot())
            StartCoroutine(AppearAndAttackCoroutine());
    }

    private void Update()
    {
        if(m_player == null) 
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(HandleGraphicsBeforeDie());
            GameManager.Instance.GameLost();
            AudioManger.Instance.GameLost();
            CameraShake.Instance.ShakeGameLost();
        }

        if (other.CompareTag("Arrow"))
        {
            StartCoroutine(HandleGraphicsBeforeDie());
            GameManager.Instance.EnemyWasHit();
            AudioManger.Instance.EnemyDie();
            CameraShake.Instance.ShakeEnemyWasHit(transform);
        }
    }

    IEnumerator HandleGraphicsBeforeDie()
    {
        Instantiate(deadExplosion, m_rb.position, Quaternion.identity);  
        Destroy(gameObject);
        yield return null;
    }
    IEnumerator AppearAndAttackCoroutine()
    {
        yield return new WaitForSeconds(DelayBeforeAttack());
        m_anim.SetTrigger("Indicate Attack"); 
        m_rb.velocity = (m_player.transform.position - m_rb.transform.position).normalized * AttackMovementSpeed();
    }
    
    float AttackMovementSpeed()
    {
        return Mathf.Min(6f,0.25f + 0.1f * GameManager.Instance.GetCurScore());
    }

    float DelayBeforeAttack()
    {
        return Mathf.Max(0.3f, 5f * Mathf.Pow(0.9f, GameManager.Instance.GetCurScore()));
    }  
    
}
