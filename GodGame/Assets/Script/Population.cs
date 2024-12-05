using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class Population : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _people;

    [SerializeField]
    private GameObject _positionPeople;

    [SerializeField]
    private float _seuil = 1.05f;

    [SerializeField]
    private int _radius = 20;

    [SerializeField]
    private float _chrono = 0.5f;

    private bool _isStartTimer = false;
    private int _posX = 0;

    private int _posZ = 0;

    private Vector3 _targetPs = Vector3.zero;

    private Coroutine _currentCoroutine = null;  

    private void Start()
    {
        Deplacement();
    }

    private void Update()
    {
        Deplacement();
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
            if (_currentCoroutine == null) 
            {
                _currentCoroutine = StartCoroutine(WaitBeforeMove());
            }
            return;
        }

        _targetPs = RandomNavmeshLocation(_radius);

        _people.isStopped = true;
        _people.ResetPath();

        if (_currentCoroutine == null)
        {
            _currentCoroutine = StartCoroutine(WaitBeforeMove());
        }
    }

    private IEnumerator WaitBeforeMove()
    {
        yield return new WaitForSeconds(_chrono);
        _currentCoroutine = null;
        _people.SetDestination(_targetPs);
        _people.isStopped = false;
    }
}
