using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private EnemyData data;

    Transform target;

    HealthBarScript healthView;

    private void Start()
    {
        healthView = GetComponentInChildren<HealthBarScript>();
        GetComponentInChildren<BulletShooter>().Init(data.BulletSpeed, data.BulletMaterial, data.SpawnDeltaTime);
    }

    public void Init(EnemyData _data) 
    {
        data = _data;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        data.CurrentHealth = data.MaxHealth;
    }

    public static Action<GameObject> OnEnemyOutOfBounds;

    private void FixedUpdate()
    {
        RotateToTarget(target);
    }

    void RotateToTarget(Transform target) 
    {
        Vector3 difVec = (target.transform.localPosition - transform.localPosition);

        transform.forward = -difVec.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        loseHealth();
    }

    private void loseHealth()
    {
        data.CurrentHealth -= 1;
        if (data.CurrentHealth <= 0)
            OnEnemyOutOfBounds(gameObject);
        healthView.UpdateHealthView(data.CurrentHealth, data.MaxHealth);
    }
}
