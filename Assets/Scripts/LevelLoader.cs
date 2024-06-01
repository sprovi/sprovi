using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText1;
    [SerializeField] private TMP_Text _levelText2;
    private void Start()
    {
        _levelText1.text = $"Level {(Progress.Instance.Level + 1).ToString()}";
    }

    public void StartLevel()
    {
        int level = Progress.Instance.Level;
        SceneManager.LoadScene(level + 1);
        _levelText2.text = $"Level {(Progress.Instance.Level + 1).ToString()}";
    }

    public void StartFromZeroLevel()
    {
        Progress.Instance.Level = 0;
        Progress.Instance.Save();
        StartLevel();
    }
}
