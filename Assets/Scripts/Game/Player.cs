using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private PlayerData data;

    public PlayerControls playerInput;

    [SerializeField]
    Vector3 startPos;

    [SerializeField]
    PlayerDataBase playerDataBase;

    Transform target;

    HealthBarScript healthView;

    public Action<bool> OnGameOver;

    private void Start()
    {
        data = playerDataBase.GetRandomElement();
        healthView = GetComponentInChildren<HealthBarScript>();
        data.CurrentHealth = data.MaxHealth;
        healthView.UpdateHealthView(data.CurrentHealth, data.MaxHealth);
        GetComponentInChildren<BulletShooter>().Init(data.BulletSpeed, data.BulletMaterial, data.SpawnDeltaTime);
    }

    public void Init()
    {
        data = playerDataBase.GetRandomElement();

        data.CurrentHealth = data.MaxHealth;

        transform.position = new Vector3(0.0f, 0.0f, 100.0f);

        tryGetNearestTarget(ref target);

        healthView.UpdateHealthView(data.CurrentHealth, data.MaxHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null) 
        {
            if (target.gameObject.activeSelf == false)
            {
                if (tryGetNearestTarget(ref target) == false)
                {
                    OnGameOver?.Invoke(true);
                }
            }
            else 
            {
                RotateToTarget(target);
            }
        }

        tryMovePlayer();
    }

    private void tryMovePlayer()
    {
        Vector3 desiredMove = transform.position + playerInput.InputPlayerMoveSpeed(transform) * Time.deltaTime * data.ObjectSpeed;

        if (ScreenViewManager.instance.IsInsideBounds(desiredMove, 0.0f)) 
        {
            transform.position = desiredMove;
        } 
    }

    void RotateToTarget(Transform target)
    {
        if (target != null)
        {
            Vector3 difVec = (target.transform.localPosition - transform.localPosition);

            transform.up = difVec.normalized;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        loseHealth();
    }

    private void loseHealth()
    {
        data.CurrentHealth -= 1;
        if (data.CurrentHealth <= 0)
            OnGameOver?.Invoke(false);
        healthView.UpdateHealthView(data.CurrentHealth, data.MaxHealth);
    }

    float minDistance = 1000000.0f;
    private bool tryGetNearestTarget(ref Transform nearestTransform)
    {
        bool found = false;
        minDistance = 1000000.0f;
        foreach (KeyValuePair<GameObject, Enemy> ObjEnemyPair in EnemySpawner.Enemies)
        {
            if (ObjEnemyPair.Key.activeSelf) 
            {
                float curDistance = Vector3.Distance(transform.position, ObjEnemyPair.Value.transform.position);
                if (curDistance < minDistance)
                {
                    found = true;
                    nearestTransform = ObjEnemyPair.Value.transform;
                    minDistance = curDistance;
                }
            }
        }

        if (found == false)
            return false;
        else
            return true;
    }
}
