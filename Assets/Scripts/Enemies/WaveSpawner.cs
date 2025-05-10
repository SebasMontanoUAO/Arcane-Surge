using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<Wave> waves;
    public List<Transform> spawnPointsTransforms;
    public Dictionary<int, Vector3> spawnPoints = new Dictionary<int, Vector3>();
    public TextMeshProUGUI waveCount;

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool isSpawning = false;
    private bool isPaused = false;
    private Coroutine currentWaveCoroutine;

    [Header("Debug")]
    public bool debugSpawnPoints = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeSpawnPoints();
        StartCoroutine(NextWave());
    }

    private void InitializeSpawnPoints()
    {
        spawnPoints.Add(0, spawnPointsTransforms[0].position);
        spawnPoints.Add(1, spawnPointsTransforms[1].position);
        spawnPoints.Add(2, spawnPointsTransforms[2].position);
        spawnPoints.Add(3, spawnPointsTransforms[3].position);

        if (debugSpawnPoints)
        {
            foreach (var point in spawnPoints)
            {
                Debug.DrawRay(point.Value, Vector3.up * 5, Color.red, 10f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        waveCount.text = (currentWaveIndex+1).ToString();
    }

    public void PauseSpawning(bool pause)
    {
        isPaused = pause;

        if (pause)
        {
            // Si está pausando, detener la corrutina actual
            if (currentWaveCoroutine != null)
            {
                StopCoroutine(currentWaveCoroutine);
            }
        }
        else
        {
            // Si está reanudando, reiniciar la oleada actual
            if (!isSpawning && currentWaveIndex < waves.Count)
            {
                currentWaveCoroutine = StartCoroutine(NextWave());
            }
        }
    }

    IEnumerator NextWave()
    {
        if (currentWaveIndex >= waves.Count) yield break;

        Wave wave = waves[currentWaveIndex];
        isSpawning = true;

        Debug.Log($"Oleada {currentWaveIndex + 1} iniciada!");

        for (int i = 0; i < wave.enemyCount; i++)
        {
            if (isPaused)
            {
                yield return new WaitWhile(() => isPaused);
            }

            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        isSpawning = false;
        if (!isPaused)
        {
            yield return new WaitForSeconds(wave.waveDelay);
            currentWaveIndex++;
            currentWaveCoroutine = StartCoroutine(NextWave());
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        int randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Count);
        Vector3 spawnPos = spawnPoints[randomSpawnPoint];

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().OnDeath += OnEnemyDied;
        enemiesAlive++;
    }
    private void OnEnemyDied()
    {
        enemiesAlive--;
        // Posible drop de habilidad aquí (20% de chance, por ejemplo)
        if (UnityEngine.Random.Range(0f, 1f) < 0.2f)
        {
            SpawnAbilityPickup();
        }
    }
    private void SpawnAbilityPickup()
    {
        Debug.Log("¡Habilidad dropeda!");
    }
}
