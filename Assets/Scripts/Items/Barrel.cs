using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : PassiveItem
{
    [SerializeField] private GameObject _dieEffect;

    public override void Affect()
    {
        base.Affect();
        if (_dieEffect)
        {
            Instantiate(_dieEffect, transform.position, Quaternion.Euler(-90, 0, 0));
        }
        ScoreManager.Instance.AddScore(ItemType, transform.position);
        Destroy(gameObject);
    }
}
