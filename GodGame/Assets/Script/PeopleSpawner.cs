using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _people;
    private GameObject _peopleInstantiated;

    private PeopleProperties _propertiesScript;

    [SerializeField]
    private float _spawnerTimer = 5f;

    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(_spawnerTimer);
        Spawn();
    }

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    /// <summary>
    /// spawn les unitées et leur défini des parametres
    /// </summary>
    private void Spawn()
    {
        _peopleInstantiated = Instantiate(_people);
        GameManager.Instance.EntitiesNumber++;
        _propertiesScript = _peopleInstantiated.GetComponent<PeopleProperties>();

        GameManager.Instance.Wanderers.Add(_peopleInstantiated);
        _propertiesScript.name = "John Doe";
        _propertiesScript.Job = 0;
        _propertiesScript.JobName = "Wanderer";

        StartCoroutine(Spawner());
    }

}
