using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class CameraShake : MonoBehaviour
    {
        [SerializeField]
        private float springForce = default;
        [SerializeField]
        private float damping = default;
        [SerializeField]
        private float targetImpactShakeFactor = default;
        [SerializeField]
        private float playerImpactShakeFactor = default;

        private Player player;
        private Vector2 vec;

        public static CameraShake Instance;

        private void Awake()
        {
            Instance = this;
            player = FindObjectOfType<Player>();
        }


        public void ShakeEnemyWasHit(Transform enemy)
        {
            if(player == null) return;
            vec += (Vector2)(player.transform.position - enemy.transform.position).normalized * targetImpactShakeFactor;
        }

        public void ShakeGameLost()
        {
            vec += Vector2.up * playerImpactShakeFactor;
        }

        private void FixedUpdate()
        {
            Vector2 position = transform.position;

            vec += springForce * -position;
            vec *= damping;

            position += vec;

            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }
    }
