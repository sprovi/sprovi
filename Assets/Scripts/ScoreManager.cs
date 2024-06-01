using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TaskIcon _taskIconPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private FlyingIcon _flyingIconPrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameManager _gameManager;

    private TaskIcon[] TaskIcons;

    public static ScoreManager Instance;
    [SerializeField] private ItemIcons ItemIcons;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Level _level = FindObjectOfType<Level>();
        TaskIcons = new TaskIcon[_level.Tasks.Length];
        for (int i = 0; i < _level.Tasks.Length; i++)
        {
            TaskIcon newTaskIcon = Instantiate(_taskIconPrefab, _parent);
            newTaskIcon.Setup(_level.Tasks[i].ItemType, _level.Tasks[i].Number);
            TaskIcons[i] = newTaskIcon;
        }
    }

    public void AddScore(ItemType itemType, Vector3 position, int ballNumber = -1)
    {
        for (int i = 0; i < TaskIcons.Length; i++)
        {
            if (TaskIcons[i].ItemType == itemType)
            {
                if (TaskIcons[i].CurrentScore != 0)
                {
                    StartCoroutine(FlyAnimation(TaskIcons[i], position));
                }
            }
            else if (itemType == ItemType.Ball && ballNumber == (int)TaskIcons[i].ItemType)
            {
                StartCoroutine(FlyAnimation(TaskIcons[i], position, ballNumber));
            }
        }
    }

    private IEnumerator FlyAnimation(TaskIcon taskIcon, Vector3 position, int ballNumber = -1)
    {
        FlyingIcon newFlyingIcon = Instantiate(_flyingIconPrefab, _parent);
        Sprite sprite;

        if (taskIcon.ItemType != ItemType.Ball && taskIcon.ItemType != ItemType.Ball256 && taskIcon.ItemType != ItemType.Ball512)
        {
            sprite = ItemIcons.GetSprite(taskIcon.ItemType);
            
        }
        else
        {
            sprite = ItemIcons.GetSprite((ItemType)ballNumber);
        }

        newFlyingIcon.Setup(sprite);
        Vector3 a = _camera.WorldToScreenPoint(position);
        Vector3 b = taskIcon.transform.position;

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            newFlyingIcon.transform.position = Vector3.Lerp(a, b, t);
            yield return null;
        }
        Destroy(newFlyingIcon.gameObject);
        taskIcon.AddOne();
        CheckWin();
    }

    private void CheckWin()
    {
        for (int i = 0; i < TaskIcons.Length; i++)
        {
            if (TaskIcons[i].CurrentScore != 0)
            {
                return;
            }
        }
        _gameManager.Win();
        Debug.Log("Win!");
    }
}