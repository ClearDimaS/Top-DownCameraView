using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField]
    LevelDatabase levelDatabase;

    [SerializeField]
    Player Player;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField] 
    EnemySpawner enemySpawner;

    LevelData levelData;

    int currentLevelNumber = 0;

    private void Start()
    {
        levelData = levelDatabase.getNextLevelNumber(currentLevelNumber);
        InitComponents();
        Player.OnGameOver += gameOver;
        loadNextLevel();
    }

    void gameOver(bool ifWin) 
    {
        gameOverPanel.SetActive(true);

        Pause(true);

        if (ifWin) 
        {
            currentLevelNumber++;
            levelData = levelDatabase.getNextLevelNumber(currentLevelNumber);
        }
    }

    public void Pause(bool ifPause)
    {
        Time.timeScale = ifPause ? 0.0f : 1.0f;
    }

    void loadNextLevel() 
    {
        InitComponents();
        gameOverPanel.SetActive(false);
        Pause(false);
    }

    public void PlayLevel() 
    {
        loadNextLevel();
    }

    void InitComponents() 
    {
        enemySpawner.ReturnAll();
        enemySpawner.Init(levelData.TotalEnemies);
        Player.Init();
    }
}
