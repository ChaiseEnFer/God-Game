using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _people;

    [SerializeField]
    private float _spawnerTimer = 5f;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        Instantiate(_people);
        yield return new WaitForSeconds(_spawnerTimer);
        StartCoroutine(Spawner());
    }
}
