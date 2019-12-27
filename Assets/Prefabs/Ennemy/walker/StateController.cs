using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
  public enum States { idle, patrol, hunt };
  public States state;
  private npcMove npcMove;

  void Awake()
  {
    npcMove = GetComponent<npcMove>();
  }

  public void Changestate(States value, GameObject target = null)
  {
    state = value;

    switch (state)
    {
      default:
      case States.idle:
        SetIdle();
        break;
      case States.hunt:
        SetHunt();
        break;
      case States.patrol:
        SetPatrol();
        break;
    }

    void SetIdle()
    {
      gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    void SetPatrol()
    {
      gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    void SetHunt()
    {
      gameObject.GetComponent<Renderer>().material.color = Color.yellow;

      if (target != null)
      {
        npcMove.SetDestination(target);
      }
      else
      {
        Debug.LogAssertion("target undefined");
      }
    }
  }
  private void Update()
  {
    
  }
}
