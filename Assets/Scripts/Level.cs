using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public ItemType ItemType;
    public int Number;
}
public class Level : MonoBehaviour
{
    public Task[] Tasks;
}
