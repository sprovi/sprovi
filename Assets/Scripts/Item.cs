using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    None,
    Ball,
    Ball256 = 256,
    Ball512 = 512,
    Dynamit,
    Barrel,
    Stone,
    Box,
}

public class Item : MonoBehaviour
{
    public ItemType ItemType;
}

