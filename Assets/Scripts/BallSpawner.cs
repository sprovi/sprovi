using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxX = 2.5f;

    private float _oldX;
    private float _x;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _pointer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _oldX = GetWorldMousePosition().x;
        }

        if (Input.GetMouseButton(0))
        {
            float x = GetWorldMousePosition().x;
            float delta = x - _oldX;
            _oldX = x;
            _x += delta;
            _x = Mathf.Clamp(_x, -_maxX, _maxX);
            transform.position = new Vector3(_x, transform.position.y, 0f);
        }
    }

    private Vector3 GetWorldMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition; // в пикселях относительно левого нижнего угла
        mousePosition.z = -_camera.transform.position.z;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        return worldPosition;
    }
}
