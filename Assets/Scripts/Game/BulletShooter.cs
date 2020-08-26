using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{

    [Tooltip("Bullets settings List")]
    [SerializeField] private BulletDataBase bulletSettings;

    [Tooltip("Objects ammount in the pool")]
    [SerializeField] private int poolCount;

    [Tooltip("BulletBase prefab")]
    [SerializeField] private GameObject bulletPrefab;

    private float spawnDeltaTime;

    [Tooltip("Point of shooting")]
    [SerializeField] private Transform shootingPoint;

    public Dictionary<GameObject, Bullet> Bullets;
    private Queue<GameObject> currentBullets;

    float bulletSpeed;
    Material bulletMaterial;

    public void Init(float _bulletSpeed, Material _bulletMaterial, float _spawnDeltaTime)
    {
        spawnDeltaTime = _spawnDeltaTime;
        bulletSpeed = _bulletSpeed;
        bulletMaterial = _bulletMaterial;
        _spawnDeltaTime = _spawnDeltaTime;

        Bullets = new Dictionary<GameObject, Bullet>();
        currentBullets = new Queue<GameObject>();

        for (int i = currentBullets.Count; i < poolCount; i++)
        {
            InstantiateBullet();
        }

        Bullet.OnBulletOutOfBounds += ReturnBullet;

        StartCoroutine(Spawn());
    }

    private void InstantiateBullet() 
    {
        GameObject prefab = Instantiate(bulletPrefab);
        prefab.GetComponent<MeshRenderer>().material = bulletMaterial;
        prefab.transform.SetParent(transform.parent.transform.parent.transform);
        prefab.transform.localScale = new Vector3(1, 1, 1);
        Bullet script = prefab.GetComponent<Bullet>();
        prefab.SetActive(false);
        Bullets.Add(prefab, script);
        currentBullets.Enqueue(prefab);
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDeltaTime);
            if (currentBullets.Count > 0)
            {
                GameObject bullet = currentBullets.Dequeue();
                Bullet script = Bullets[bullet];
                bullet.SetActive(true);

                BulletData bulletData = bulletSettings.GetRandomElement();
                // Choosing random BulletData
                script.Init(bulletSpeed);

                // Generation
                bullet.transform.rotation = shootingPoint.transform.rotation;
                bullet.transform.position = new Vector3(shootingPoint.position.x, shootingPoint.position.y, 100.0f);
            }
        }
    }

    private void ReturnBullet(GameObject bullet)
    {
        if (Bullets.ContainsKey(bullet)) 
        {
            bullet.transform.position = transform.position;
            bullet.SetActive(false);
            currentBullets.Enqueue(bullet);
        }
    }

    private void ReturnAll()
    {
        foreach (KeyValuePair<GameObject, Bullet> keyValuePair in Bullets)
        {
            ReturnBullet(keyValuePair.Key);
        }
    }
}
