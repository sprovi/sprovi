using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ActiveItem : Item
{
    public int Level;

    [SerializeField] protected TextMeshProUGUI _text;
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected SphereCollider _trigger;
    [SerializeReference] protected Rigidbody _rigidbody;

    protected CollapseManager _collapseManager;
    public bool Active = true;

    protected virtual void OnValidate()
    {
        SetLevel(Level);
    }

    public void Deactivate()
    {
        Active = false;
        _trigger.enabled = false;
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
        _rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    public void Activate()
    {
        Active = true;
        _trigger.enabled = true;
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public virtual void SetLevel(int level)
    {
        Level = level;
        int number = (int)Mathf.Pow(2, level + 1);
        _text.text = number.ToString();
    }

    public virtual void Init(CollapseManager collapseManager)
    {
        _collapseManager = collapseManager;
    }

    public virtual void DoEffect()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (Active)
        {
            if (other.attachedRigidbody)
            {
                if (other.attachedRigidbody.GetComponent<ActiveItem>() is ActiveItem otherActivateItem)
                {
                    if (otherActivateItem.Level == Level && otherActivateItem.Active)
                    {
                        _collapseManager.Collapse(this, otherActivateItem);
                    }
                }
            }
        }
    }
}
