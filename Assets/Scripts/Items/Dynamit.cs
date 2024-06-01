using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Dynamit : ActiveItem
{
    [SerializeField] private float _affectRadius = 1.5f;
    [SerializeField] private float _forceValue = 500f;
    [SerializeField] private GameObject _affectArea;
    [SerializeField] private GameObject _affectPrefab;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _affectArea.SetActive(false);
    }

    private IEnumerator AffectProcess()
    {
        _affectArea.SetActive(true);
        _animator.enabled = true;
        yield return new WaitForSeconds(1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _affectRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;
            if (rigidbody)
            {
                Vector3 to = (rigidbody.transform.position - transform.position).normalized;
                rigidbody.AddForce(to * _forceValue + Vector3.up * _forceValue * 0.5f);

                PassiveItem passiveItem = rigidbody.GetComponent<PassiveItem>();
                if (passiveItem)
                {
                    passiveItem.Affect();
                }
            }
        }

        Instantiate(_affectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override void DoEffect()
    {
        base.DoEffect();
        StartCoroutine(AffectProcess());
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        _affectArea.transform.localScale = Vector3.one * _affectRadius * 2f;
    }
}
