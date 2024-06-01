using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Ray1 : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _column;
    //[SerializeField] private Transform _sphere;

    private void Update()
    {
        
    }

    public void CreateRay(Transform where, Transform pointer)
    {
        Ray ray = new Ray(where.position, Vector3.down);
        RaycastHit hit;
        if (Physics.SphereCast(ray, pointer.localScale.x / 2, out hit, 20f, _layerMask, QueryTriggerInteraction.Ignore))
        {
            _column.position = new Vector3(where.position.x, where.position.y, where.position.z + 1f);
            _column.localScale = new Vector3(pointer.localScale.x, (hit.distance / 2) - 0.09f, _column.localScale.z);
            pointer.position = new Vector3(where.position.x, where.position.y, where.position.z + 2f) + Vector3.down * hit.distance;
            //_sphere.position = new Vector3(where.position.x, where.position.y, where.position.z + 1f) + Vector3.down * (hit.distance - 0.05f);
        }
    }
}
