using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[System.Serializable]
public class ProgressData
{
    public int Level;
}

public class Progress : MonoBehaviour
{
    public int Level;

    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLevel(int level)
    {
        Level = level;
        Save();
    }

    public void Save()
    {
        ProgressData progressData = new ProgressData();
        progressData.Level = Level;

        string json = JsonUtility.ToJson(progressData);
        PlayerPrefs.SetString("Progress", json);
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("Progress"))
        {
            string jsonStringProgress = PlayerPrefs.GetString("Progress");
            ProgressData progressData = JsonUtility.FromJson<ProgressData>(jsonStringProgress);
            Level = progressData.Level;
        }
    }
}
