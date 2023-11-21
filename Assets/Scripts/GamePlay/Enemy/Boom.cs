using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Boom : MonoBehaviour
{
    [SerializeField] private GameObject enemyFly;
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosionBoom;
    private Rigidbody2D m_rb;
    private EnemyFlySpawner enemyFlySpawner;
    private Player player;
    private Animator m_anim;
    private bool allowMove => !Archery2Manager.Instance.IsWaitingForFirstTargetShot;

    private void Awake()
    {
        m_rb = transform.parent.GetComponent<Rigidbody2D>();
        m_anim = transform.parent.GetComponent<Animator>();
        if(m_anim == null) Debug.Log("1");
        enemyFlySpawner = FindObjectOfType<EnemyFlySpawner>();
        player = FindObjectOfType<Player>();
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            HandleDead();
            if (!allowMove)
            {
                Archery2Manager.Instance.LoadScene(1f);
            }
        }
    }

    void HandleDead()
    {
        player.HandleDead();
        Archery2Manager.Instance.GameLost();
        AudioManger.Instance.GameLost();
        CameraShake.Instance.ShakeGameLost();
        Destroy(transform.parent.gameObject);
        Instantiate(explosionBoom, transform.parent.position, Quaternion.identity);
    }

    private void Update()
    {
        if(allowMove )
            m_rb.velocity = new Vector2(0, -MoveDownSpeed() * speed);

        if (!enemyFly.gameObject.activeSelf)
        {
            speed = 7;
        }

        CheckToBoomColliderLand();
    }

    private void CheckToBoomColliderLand()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.parent.position);

        if (viewportPosition.y < 0 && enemyFly.gameObject.activeSelf)
        {
            HandleDead();
        }
    }
    
    float MoveDownSpeed()
    {
        return Mathf.Min(2f,0.25f + 0.1f * Archery2Manager.Instance.GetCurScore());
    }
}
