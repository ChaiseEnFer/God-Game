using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class Population : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _people;

    [SerializeField]
    private GameObject _peopleLife;

    [SerializeField]
    private float _seuil = 1.05f;

    [SerializeField]
    private int _radius = 20;

    [SerializeField]
    private float _chrono = 0.5f;

    [SerializeField]
    //private int _timeLife = 0;

    private PeopleProperties _propertiesScript;

    public Vector3 TargetPs = Vector3.zero;

    private Coroutine _currentCoroutine = null;
    public bool HasAHouse;

    private IEnumerator WaitBeforeMove()
    {
        yield return new WaitForSeconds(_chrono);
        _currentCoroutine = null;
        _people.SetDestination(TargetPs);
        _people.isStopped = false;
    }

    private void Start()
    {
        _propertiesScript = gameObject.GetComponent<PeopleProperties>();
        Move();
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// Fonction qui retourne un point aléatoire naviguable autour de l'entité
    /// </summary>
    /// <param name="radius">rayon de la recherche de point</param>
    /// <returns>Vector3 : point naviguable cible</returns>
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

    /// <summary>
    /// Fonction de déplacement automatique des peoples
    /// </summary>
    private void Move()
    {
        if (GameManager.Instance.IsDayRunning || !HasAHouse)
        {
            switch (_propertiesScript.Job) //terminer les jobs quand map faite et navmeshée
            {
                case 0:
                    RandomMoving();
                    break;

                default:
                    break;
            }
        }
        else
        {
            _people.SetDestination(TargetPs);
            //StopAllCoroutines();
        }
    }

    private void RandomMoving()
    {
        if (_people.remainingDistance > _seuil)
        {
            _currentCoroutine ??= StartCoroutine(WaitBeforeMove());
            return;
        }
        TargetPs = RandomNavmeshLocation(_radius);

        _people.isStopped = true;
        _people.ResetPath();

        _currentCoroutine ??= StartCoroutine(WaitBeforeMove());
    }
}
