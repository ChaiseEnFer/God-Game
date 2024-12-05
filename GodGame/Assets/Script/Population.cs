using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Population : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _people;

    [SerializeField]
    private GameObject _positionPeople;

    [SerializeField]
    private float _seuil = 1.05f;
    
    [SerializeField]
    private int _radius = 10;

    [SerializeField]
    private float _currentTime = 0f;

    private int _posX = 0;

    private int _posZ = 0;

    private bool _isStartedTimer = false;  

    private Vector3 _targetPs = Vector3.zero;

    private void Start()
    {
        Deplacement();
    }

    private void Update()
    {

        if (_isStartedTimer)
        {
            if (_currentTime >= 1.5f)
            {
                Deplacement();
                _currentTime = 0f;
                _isStartedTimer = false;
            }
            else
            {
                _currentTime += Time.deltaTime;
            }
        }

       
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private void Deplacement()
    {
        if (_people.remainingDistance > _seuil)
        {
            _isStartedTimer = true;
            return;
        }


        _targetPs = RandomNavmeshLocation(_radius);
        _people.SetDestination(_targetPs);

    }
}
