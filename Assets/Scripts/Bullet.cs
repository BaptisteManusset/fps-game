using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

  [Tag] public string targets;
  [Tag] public string shooter;
  public float damage = 1;
  [SerializeField] bool glue = false;
  private Rigidbody rb;

  private void Awake()
  {
    rb = GetComponent<Rigidbody>();
  }



  private void OnTriggerEnter(Collider other)
  {



    if (other.tag != null)
    {
      if (other.CompareTag(shooter))
      {
        return;
      }

      if (other.CompareTag(targets))
      {
        switch (other.tag)
        {
          #region player
          case "Player":
            other.GetComponent<PlayerState>()?.takeDamage(damage);
            break;
          #endregion

          #region Ennemy
          case "Ennemy":
            StateController2 hl = other.gameObject.GetComponent<StateController2>();
            if (hl != null)
            {
              hl.takeDamage(damage);
            }
            StateMachineMain smm = other.gameObject.GetComponent<StateMachineMain>();
            if (smm != null)
            {
              smm.takeDamage(damage);
            }
            break;
          #endregion
          default:
            break;
        }

      }



      if (glue)
      {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
      }
    }
  }
}
