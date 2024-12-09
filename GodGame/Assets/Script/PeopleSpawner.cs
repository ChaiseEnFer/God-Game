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
        StartSpawn();
        StartSpawn();
        StartCoroutine(Spawner());
    }

    /// <summary>
    /// spawn les unitées et leur défini des parametres
    /// </summary>
    private void Spawn()
    {
        _peopleInstantiated = Instantiate(_people, transform.position, Quaternion.identity);
        GameManager.Instance.EntitiesNumber++;
        _propertiesScript = _peopleInstantiated.GetComponent<PeopleProperties>();
        GameManager.Instance.AllPeople.Add(_peopleInstantiated);
        GameManager.Instance.Wanderers.Add(_peopleInstantiated);
        _propertiesScript.EntityName = "John Doe";
        _propertiesScript.Job = 0;
        //_propertiesScript.JobName = "Wanderer";

        StartCoroutine(Spawner());
    }

    /// <summary>
    /// Set up and spawn pioneers with right properties
    /// </summary>
    private void StartSpawn()
    {
        _peopleInstantiated = Instantiate(_people, transform.position, Quaternion.identity);
        GameManager.Instance.EntitiesNumber++;
        _propertiesScript = _peopleInstantiated.GetComponent<PeopleProperties>();
        GameManager.Instance.AllPeople.Add(_peopleInstantiated);
        GameManager.Instance.Wanderers.Add(_peopleInstantiated);
        _propertiesScript.EntityName = "John Doe";
        _propertiesScript.Job = 0;
        //_propertiesScript.JobName = "Wanderer";

        _peopleInstantiated = Instantiate(_people, transform.position, Quaternion.identity);
        GameManager.Instance.EntitiesNumber++;
        _propertiesScript = _peopleInstantiated.GetComponent<PeopleProperties>();
        GameManager.Instance.AllPeople.Add(_peopleInstantiated);
        GameManager.Instance.FoodHarvesters.Add(_peopleInstantiated);
        _propertiesScript.EntityName = "John Doe";
        _propertiesScript.Job = 1;
        //_propertiesScript.JobName = "Food Harvester";

        _peopleInstantiated = Instantiate(_people, transform.position, Quaternion.identity);
        GameManager.Instance.EntitiesNumber++;
        _propertiesScript = _peopleInstantiated.GetComponent<PeopleProperties>();
        GameManager.Instance.AllPeople.Add(_peopleInstantiated);
        GameManager.Instance.Timbers.Add(_peopleInstantiated);
        _propertiesScript.EntityName = "John Doe";
        _propertiesScript.Job = 2;
        //_propertiesScript.JobName = "Timber";

        _peopleInstantiated = Instantiate(_people, transform.position, Quaternion.identity);
        GameManager.Instance.EntitiesNumber++;
        _propertiesScript = _peopleInstantiated.GetComponent<PeopleProperties>();
        GameManager.Instance.AllPeople.Add(_peopleInstantiated);
        GameManager.Instance.Miners.Add(_peopleInstantiated);
        _propertiesScript.EntityName = "John Doe";
        _propertiesScript.Job = 3;
        //_propertiesScript.JobName = "Miner";

        _peopleInstantiated = Instantiate(_people, transform.position, Quaternion.identity);
        GameManager.Instance.EntitiesNumber++;
        _propertiesScript = _peopleInstantiated.GetComponent<PeopleProperties>();
        GameManager.Instance.Masons.Add(_peopleInstantiated);
        GameManager.Instance.AllPeople.Add(_peopleInstantiated);
        _propertiesScript.EntityName = "John Doe";
        _propertiesScript.Job = 4;
        //_propertiesScript.JobName = "Mason";
    }

}
