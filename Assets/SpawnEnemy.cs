﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wave
{
	public GameObject enemyPrefab;
	public float spawnInterval = 2;
	public int maxEnemies = 20;
}

public class SpawnEnemy : MonoBehaviour
{
	public Wave[] waves;
	public int timeBetweenWaves = 5;

	public GameObject[] waypoints;
	public GameObject testEnemyPrefab;

	private GameManagerBehavior gameManager;
	private float lastSpawnTime;
	private int enemiesSpawned = 0;


	void Start()
	{
		lastSpawnTime = Time.time;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
	}

	void Update()
	{
		int currentWave = gameManager.Wave;
		if (currentWave < waves.Length)
		{
			float timeInterval = Time.time - lastSpawnTime;
			float spawnInterval = waves[currentWave].spawnInterval;
			if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || timeInterval > spawnInterval) &&
			    enemiesSpawned < waves[currentWave].maxEnemies)
			{
				lastSpawnTime = Time.time;
				GameObject newEnemy = (GameObject) Instantiate(waves[currentWave].enemyPrefab);
				newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
				enemiesSpawned++;
			}
			if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				gameManager.Wave++;
				gameManager.Gold = Mathf.RoundToInt(gameManager.Gold*1.1f);
				enemiesSpawned = 0;
				lastSpawnTime = Time.time;
			}
			else
			{
				gameManager.gameOver = true;
				//GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
				//gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
			}
		}
	}
}
