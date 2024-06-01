using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] private Transform _tube;
    [SerializeField] private Transform _spawner;
    [SerializeField] private Ball _ballPrefab;

    private Ball _ballInTube;
    private Ball _ballInSpawner;

    [SerializeField] private CollapseManager _collapseManager;

    // Start is called before the first frame update
    void Start()
    {
        CreateBallInTube();
        StartCoroutine(MoveToSpawner());
    }

    private void CreateBallInTube()
    {
        int ballLevel = Random.Range(0, 3);
        _ballInTube = Instantiate(_ballPrefab, _tube.position, Quaternion.identity);
        _ballInTube.SetLevel(ballLevel);
        _ballInTube.SetToTube();
        _ballInTube.Init(_collapseManager);
    }

    private IEnumerator MoveToSpawner()
    {
        _ballInTube.transform.parent = _spawner;
        for (float t = 0; t < 1f; t += Time.deltaTime / 0.3f)
        {
            _ballInTube.transform.position = Vector3.Lerp(_tube.position, _spawner.position, t);
            yield return null;
        }
        _ballInTube.transform.localPosition = Vector3.zero;
        _ballInSpawner = _ballInTube;
        _ballInSpawner.DoRay = true;
        _ballInTube = null;
        CreateBallInTube();
    }

    private void Update()
    {
        if (_ballInSpawner)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Drop();
            }
        }
    }

    private void Drop()
    {
        _ballInSpawner.Drop();
        _ballInSpawner = null;
        if (_ballInTube)
        {
            StartCoroutine(MoveToSpawner());
        }
    }
}
