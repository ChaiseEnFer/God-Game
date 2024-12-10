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
    private GameObject MineTarget;

    [SerializeField]
    private GameObject HarvestTarget;

    [SerializeField]
    private GameObject ForestTarget;

    [SerializeField]
    //private int _timeLife = 0;

    private PeopleProperties _propertiesScript;

    public Vector3 TargetPs = Vector3.zero;

    private Coroutine _currentCoroutine = null;

    public bool CanMove = true;
    public bool HasAHouse = false;

    public bool IsDestinationSet = false;

    private IEnumerator WaitBeforeMove()
    {
        yield return new WaitForSeconds(_chrono);
        _currentCoroutine = null;
        _people.SetDestination(TargetPs);
        _people.isStopped = false;
    }

    private void Start()
    {
        MineTarget = GameObject.FindGameObjectWithTag("MineTarget");
        ForestTarget = GameObject.FindGameObjectWithTag("ForestTarget");
        HarvestTarget = GameObject.FindGameObjectWithTag("HarvestTarget");
        _propertiesScript = gameObject.GetComponent<PeopleProperties>();
        Move();
    }

    private void Update()
    {
        Move();
        Debug.Log(GameManager.Instance.IsDayRunning);
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
        if (!IsDestinationSet)
        {
            SetDayDestination();
        }

        if (CanMove)
        {
            if (GameManager.Instance.IsDayRunning || !HasAHouse)
            {
                switch (_propertiesScript.Job)
                {
                    case 0:
                        RandomMoving();
                        break;

                    case 1:
                        RegularMoving();
                        break;

                    case 2:
                        RegularMoving();
                        break;

                    case 3:
                        RegularMoving();
                        break;

                    case 4:
                        RandomMoving();
                        break;

                }
            }
            else if (GameManager.Instance.IsDayRunning)
            {
                RegularMoving();
            }
            else
            {
                RandomMoving();
            }
        }
        else
        {
            RegularMoving();
        }
    }

    private void SetDayDestination()
    {
        switch (_propertiesScript.Job)
        {
            case 1:
                TargetPs = new Vector3(HarvestTarget.transform.position.x + Random.Range(-15, 15), HarvestTarget.transform.position.y, HarvestTarget.transform.position.z + Random.Range(-15, 15));
                break;

            case 2:
                TargetPs = new Vector3(ForestTarget.transform.position.x + Random.Range(-15, 15), ForestTarget.transform.position.y, ForestTarget.transform.position.z + Random.Range(-15, 15));
                break;

            case 3:
                TargetPs = new Vector3(MineTarget.transform.position.x + Random.Range(-30, 30), MineTarget.transform.position.y, MineTarget.transform.position.z + Random.Range(-30, 30));
                break;
        }
        IsDestinationSet = true;
    }

    private void RegularMoving()
    {
        _people.SetDestination(TargetPs);
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
