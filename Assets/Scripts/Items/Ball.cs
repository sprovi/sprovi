using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : ActiveItem
{
    public float Radius;

    [SerializeField] private Transform _visualTransform;
    private Transform _pointer;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private BallSettings _ballSettings;

    public bool DoRay = false;
    public void IncreaseLevel()
    {
        Level++;
        SetLevel(Level);
        _trigger.enabled = false;
        _trigger.enabled = true;
    }

    public override void DoEffect()
    {
        base.DoEffect();
        IncreaseLevel();
        ScoreManager.Instance.AddScore(ItemType.Ball, transform.position, GetNumber());
        AffectPassiveItems(transform.position, Radius);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (DoRay)
        {
            _pointer.gameObject.SetActive(true);
            FindObjectOfType<Ray1>().CreateRay(_visualTransform, _pointer);
        }
        else
        {
            _pointer.gameObject.SetActive(false);
        }
    }
    public override void SetLevel(int level)
    {
        base.SetLevel(level);
        Radius = Mathf.Lerp(0.4f, 0.7f, level / 10f);
        Vector3 ballScale = Vector3.one * Radius * 2f;
        _visualTransform.localScale = ballScale;
        _collider.radius = Radius;
        _trigger.radius = Radius + 0.1f;

        _renderer.material = _ballSettings.BallMaterials[level];
    }

    public override void Init(CollapseManager collapseManager)
    {
        base.Init(collapseManager);
        _pointer = Instantiate(_visualTransform, _visualTransform.position, _visualTransform.rotation);
    }

    public void SetToTube()
    {
        _trigger.enabled = false;
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
        _rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    public void Drop()
    {
        _trigger.enabled = true;
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        transform.parent = null;
        _rigidbody.velocity = Vector3.down * 1.2f;
        DoRay = false;
    }

    private void AffectPassiveItems(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].attachedRigidbody)
            {
                PassiveItem passiveItem = colliders[i].attachedRigidbody.GetComponent<PassiveItem>();
                if (passiveItem)
                {
                    passiveItem.Affect();
                }
            }
        }
    }

    public int GetNumber()
    {
        return (int)Mathf.Pow(2, Level + 1);
    }
}
