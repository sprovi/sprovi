using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : PassiveItem
{
    [SerializeField] private GameObject _dieEffect;
    private int _level = 2;
    [SerializeField] private Stone _stonePrefab;

    public override void Affect()
    {
        base.Affect();
        if (_level > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                CreateChildStone(_level - 1);
            }
        }
        else
        {
            ScoreManager.Instance.AddScore(ItemType, transform.position);
        }
        Die();
    }

    private void CreateChildStone(int level)
    {
        Stone newStone = Instantiate(_stonePrefab, transform.position, Quaternion.identity);
        newStone.SetLevel(level);
    }

    private void SetLevel(int level)
    {
        float scale = 1f;
        if (level == 2)
        {
            scale = 1f;
        }
        else if (level == 1)
        {
            scale = 0.7f;
        }
        else if (level == 0)
        {
            scale = 0.45f;
        }
        transform.localScale = Vector3.one * scale;
        _level = level;
    }

    private void Die()
    {
        if (_dieEffect)
        {
            Instantiate(_dieEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
