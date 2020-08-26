using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveObject
{
    [Tooltip("Bullet speed")]
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed
    {
        get { return bulletSpeed; }
        protected set { }
    }

    [Tooltip("Bullet Color")]
    [SerializeField] private Material bulletMaterial;
    public Material BulletMaterial
    {
        get { return bulletMaterial; }
        protected set { }
    }

    [Tooltip("Time gap between bullets")]
    [SerializeField] private float spawnDeltaTime;
    public float SpawnDeltaTime
    {
        get { return spawnDeltaTime; }
        protected set { }
    }

    [Tooltip("Maximum health")]
    [SerializeField] private int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        protected set { }
    }

    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
}
