using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcMove : MonoBehaviour
{
  [SerializeField]
  Transform _destination;
  NavMeshAgent navMeshAgent;
  public float minDistanceToTarget = 1;

  public GameObject[] waypoints;


  [Tooltip("Durée entre chaque recherche \nde la position de la cible (_Destination) en frame")]
  public int intervalTime = 1;

  StateController state;

  void Awake()
  {

    state = GetComponent<StateController>();
    waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

    navMeshAgent = GetComponent<NavMeshAgent>();
  }


  private void Update()
  {
    if (Time.frameCount % intervalTime == 0)
    {
      if (navMeshAgent.remainingDistance <= minDistanceToTarget || navMeshAgent.remainingDistance >= 999999)
      {
        pickNewWaypoint();
        UpdateDestination();
      }
    }
  }

  public void UpdateDestination()
  {
    if (_destination == null)
    {
      Debug.Log("no target define");
      return;
    }

    Vector3 targetVector = _destination.position;
    navMeshAgent.SetDestination(targetVector);

  }

  public void SetDestination(GameObject target)
  {
    if (target != null)
    {
      Debug.Log("no target define");
      return;
    }
    Vector3 targetVector = target.transform.position;
    navMeshAgent.SetDestination(targetVector);
    _destination = target.transform;

  }

  void pickNewWaypoint()
  {
    _destination = waypoints[Random.Range(0, waypoints.Length)].transform;
  }
}
