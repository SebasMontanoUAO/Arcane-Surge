using System;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int enemiesKilled;
    public int wavesCleared;
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
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
    }
}
