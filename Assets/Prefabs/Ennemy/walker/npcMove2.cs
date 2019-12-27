using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateController2))]
[RequireComponent(typeof(NavMeshAgent))]
public class npcMove2 : MonoBehaviour
{
  private StateController2 state;
  NavMeshAgent navMeshAgent;
  private GameObject[] waypoints;



  [Tooltip("Durée entre chaque recherche \nde la position de la cible (_Destination) en frame")]
  public int intervalTime = 1;
  //public float minDistanceToTarget = 1;
  private Transform _destination;

  public GameObject bullet;
  public float launchPower = 1000;
  public int interval = 10;
  public GameObject cannon;
  public bool toggleMove = true;
  void Awake()
  {
    state = GetComponent<StateController2>();
    navMeshAgent = GetComponent<NavMeshAgent>();


    waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
  }

  void Update()
  {
    if (Game.IsPaused) return;

    if (Time.frameCount % intervalTime == 0)
    {
      switch (state.state)
      {
        case StateController2.States.idle:
          idle();
          break;
        case StateController2.States.patrol:
          patrol();
          break;
        case StateController2.States.hunt:
          hunt();
          break;
      }
    }

  }

  private void hunt()
  {
    Transform _destination = GameObject.FindGameObjectWithTag("Player").transform;
    Vector3 targetVector = _destination.position;
    if (toggleMove)
    {
      navMeshAgent.SetDestination(targetVector);
    }
    if (Time.frameCount % interval == 0)
    {
      cannon.transform.LookAt(_destination);
      

      Vector3 direction = (_destination.transform.position - transform.position).normalized;
      RaycastHit hit;
      Ray ray = new Ray(transform.position, direction);
      if (Physics.Raycast(ray, out hit))
      {
        if (hit.collider.CompareTag("Player"))
        {
          var bull = Instantiate(bullet, cannon.transform.position, cannon.transform.rotation);
          bull.GetComponent<Rigidbody>().AddForce(bull.transform.forward * launchPower);
          Destroy(bull, 5f);
        }
      }
    }
  }

  private void idle()
  {
  }

  private void patrol()
  {

    //if (navMeshAgent.remainingDistance <= minDistanceToTarget || navMeshAgent.remainingDistance >= 999999)
    if (navMeshAgent.remainingDistance >= 999)
    {
      _destination = waypoints[UnityEngine.Random.Range(0, waypoints.Length)].transform;
      Vector3 targetVector = _destination.position;
      navMeshAgent.SetDestination(targetVector);

    }

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


}
