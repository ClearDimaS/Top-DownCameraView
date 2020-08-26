using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Enemies settings List")]
    [SerializeField] private EnemyDataBase enemySettings;

    [Tooltip("EnemieBase prefab")]
    [SerializeField] private GameObject enemiePrefab;

    [Tooltip("Time between the sapwns")]
    [SerializeField] private float spawnDeltaTime;

    public static Dictionary<GameObject, Enemy> Enemies;
    private Queue<GameObject> currentEnemies;

    public void Init(int poolCount)
    {
        Enemies = new Dictionary<GameObject, Enemy>();
        currentEnemies = new Queue<GameObject>();

        for (int i = 0; i < poolCount; i++)
        {
            if(i >= currentEnemies.Count)
                InstantiateEnemy();
            Spawn();
        }

        Enemy.OnEnemyOutOfBounds += ReturnEnemy;
    }

    private void InstantiateEnemy()
    {
        GameObject prefab = Instantiate(enemiePrefab, transform, transform);
        prefab.transform.SetParent(transform);
        prefab.transform.localScale = new Vector3(1, 1, 1);
        Enemy script = prefab.GetComponent<Enemy>();
        script.transform.localPosition =  new Vector3(0, 0, 0);
        prefab.SetActive(false);
        Enemies.Add(prefab, script);
        currentEnemies.Enqueue(prefab);
    }

    private void Spawn() 
    {
        GameObject enemy = currentEnemies.Dequeue();
        Enemy script = Enemies[enemy];
        enemy.SetActive(true);

        // Choosing random EnemyData
        script.Init(enemySettings.GetRandomElement());

        // Generation
        float yPos;
        float xPos;

        xPos = Random.Range(ScreenViewManager.instance.screenBorders[0], ScreenViewManager.instance.screenBorders[1]);
        yPos = Random.Range(ScreenViewManager.instance.screenBorders[2], ScreenViewManager.instance.screenBorders[3]);

        enemy.transform.position = new Vector3(xPos, yPos, 100.0f);
    }

    private void ReturnEnemy(GameObject _enemy) 
    {
        _enemy.transform.position = transform.position;
        _enemy.SetActive(false);
        currentEnemies.Enqueue(_enemy);
    }

    public void ReturnAll()
    {
        if (Enemies != null) 
        {
            foreach (KeyValuePair<GameObject, Enemy> keyValuePair in Enemies)
            {
                ReturnEnemy(keyValuePair.Key);
            }
        }
    }
}
