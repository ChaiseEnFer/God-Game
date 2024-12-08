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
        Debug.Log("1");
        _peopleInstantiated = Instantiate(_people);
        Debug.Log("2");
        GameManager.Instance.EntitiesNumber++;
        Debug.Log("3");
        _propertiesScript = _peopleInstantiated.GetComponent<PeopleProperties>();
        Debug.Log("4");

        GameManager.Instance.Wanderers.Add(_peopleInstantiated);
        Debug.Log("5");
        _propertiesScript.name = "John Doe";
        Debug.Log("6");
        _propertiesScript.Job = 0;
        Debug.Log("7");
        _propertiesScript.JobName = "Wanderer";
        Debug.Log("8");

        StartCoroutine(Spawner());
    }

}
