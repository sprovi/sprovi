using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : PassiveItem
{
    public int Health = 2;
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private GameObject _breakEffectPrefab;
    [SerializeField] private Animator _animator;

    private void OnValidate()
    {
        SetLevel(Health);
    }
    public override void Affect()
    {
        base.Affect();
        Health -= 1;
        Instantiate(_breakEffectPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
        _animator.SetTrigger("Shake");
        if (Health < 0)
        {
            Die();
        }
        else
        {
            SetLevel(Health);
        }
    }
    public void SetLevel(int value)
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].SetActive(i <= value);
        }
    }

    private void Die()
    {
        FindObjectOfType<ScoreManager>().AddScore(ItemType, transform.position);
        Destroy(gameObject);
    }
}
