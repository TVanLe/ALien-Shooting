using System;
using System.Collections;
using System.Collections.Generic;
using MadsBangH.ArcheryGame;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    private const float ReloadCoolDownSeconds = 0.5f;

    public bool isAllowShoot;
    [SerializeField] private Arrow m_arrowPrefab;
    [SerializeField] private GameObject m_deadExposion;
    [SerializeField] private Transform m_bow;
    [SerializeField] private Transform m_arrowOnBow;
    [SerializeField] private Animator m_bowAnim;
    [SerializeField] private float m_forceBow;
    [SerializeField] private float m_stretchAmount;
    [SerializeField] private float m_disLimitToPull;

    [SerializeField] private AudioClip pull;
    [SerializeField] private AudioClip shot;
    
    private float lastShotTime = 0f;
    private bool m_isPullBow ;
    private Vector3 m_startPullPoint;
    private Vector3 m_endPullPoint;

    private AudioSource m_audioSource;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        isAllowShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.GameLost();
            HandleDead();
        }
    }

    public void HandleDead()
    {
        Destroy(gameObject);
        Instantiate(m_deadExposion, transform.position, Quaternion.identity);
    }
    private void Update()
    {
        if(!isAllowShoot) return;
        
        if (Time.time > lastShotTime + ReloadCoolDownSeconds)
        {
            m_arrowOnBow.gameObject.SetActive(true);
        }
        Vector3 position2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = position2D - m_bow.position;
        float alpha = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        alpha += 180f;
        if (Input.GetMouseButtonDown(0))
        {
            m_startPullPoint = position2D;
            m_isPullBow = true;
            m_audioSource.pitch = 1.1f;
            PlaySFX(pull);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            m_isPullBow = false;
            ActionShot(alpha, m_stretchAmount);
        }

        if (m_isPullBow)
        {
            UpdateBowGraphics(alpha);
            HandlePull(m_startPullPoint, m_endPullPoint);
            m_endPullPoint = position2D;
        }
    }

    protected virtual void UpdateBowGraphics(float alpha)
    {
        m_bow.rotation = Quaternion.Euler(0 , 0 , alpha);
    }

    protected virtual void HandlePull(Vector3 posStart , Vector3 posEnd)
    {
        float disMin = Vector3.Distance(m_bow.position, posStart);
        float disMax = Vector3.Distance(m_bow.position, posEnd);
        if (disMax > disMin)
        {
            float distance = Vector3.Distance(posStart, posEnd);
            distance = Mathf.Clamp(distance, 0f, m_disLimitToPull);
            m_stretchAmount = distance / m_disLimitToPull;
        }
        else m_stretchAmount = 0;
        
        m_bowAnim.SetFloat("Stretch Amount",m_stretchAmount);
    }

    protected virtual void ActionShot(float alpha, float stretchAmount)
    {
        m_bowAnim.SetFloat("Stretch Amount",0f);
        m_arrowOnBow.gameObject.SetActive(false);
        lastShotTime = Time.time;
        Arrow arrowProjectile = Instantiate(m_arrowPrefab);
        arrowProjectile.SetPositionRotationAndSpeed(m_arrowOnBow.position, alpha, stretchAmount * m_forceBow + 5f);
        m_audioSource.Stop();
        m_audioSource.pitch = 0.4f + 0.8f * stretchAmount;
        PlaySFX(shot);
    }

    void PlaySFX(AudioClip clip)
    {
        m_audioSource.PlayOneShot(clip);
    }
}


