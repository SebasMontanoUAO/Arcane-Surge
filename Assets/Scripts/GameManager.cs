using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public int enemiesKilled;
    public int wavesCleared;
    public bool keySpawned;
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private int wavesRequiredForKey = 3;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private Transform keySpawnPosition;

    private GameData gameData;
    private string saveFilePath;

    public static GameManager Instance { get => instance; }
    public int EnemiesKilled => gameData.enemiesKilled;
    public int WavesCleared => gameData.wavesCleared;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            InitializeSaveData();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnKey();
        }
    }
    private void InitializeSaveData()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "GameSaveData.json");
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);
            gameData = JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            gameData = new GameData
            {
                enemiesKilled = 0,
                wavesCleared = 0
            };
            SaveData();
        }
    }

    private void SaveData()
    {
        string jsonData = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, jsonData);
    }

    public void EnemyKilled()
    {
        gameData.enemiesKilled++;
        SaveData();
        Debug.Log($"Enemigos eliminados: {gameData.enemiesKilled}");
    }

    public void WaveCleared()
    {
        gameData.wavesCleared++;
        SaveData();
        Debug.Log($"Oleadas completadas: {gameData.wavesCleared}");

        if (gameData.wavesCleared >= wavesRequiredForKey &&
            !gameData.keySpawned)
        {
            SpawnKey();
            gameData.keySpawned = true;
            Debug.Log("Spawn Key");
        }
    }

    private void SpawnKey()
    {
        if (keyPrefab != null)
        {
            GameObject key = Instantiate(keyPrefab, keySpawnPosition.position, Quaternion.identity);

            key.AddComponent<KeyCollectible>();
        }
    }
}
