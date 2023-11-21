using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    [SerializeField] GameObject deadExplosion;
    private Animator m_anim;
    private Player m_player;
    
    
    private void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(m_player == null) 
            Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            AudioManger.Instance.EnemyFlyDie();
            CameraShake.Instance.ShakeEnemyWasHit(transform);
            Archery2Manager.Instance.EnemyWasHit();
            Instantiate(deadExplosion , transform.position , quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}


