using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineMeduse : StateMachineMain
{

  Rigidbody rb;
  public float thrust = 10;
  public float maxSpeed = 5;
  [BoxGroup("Bomb"), SerializeField] GameObject bomb;
  [BoxGroup("Bomb"), SerializeField] GameObject fakeBomb;
  [BoxGroup("Bomb"), SerializeField] float lauchDistance = 5f;
  [BoxGroup("Bomb")] public float reloadDuration = 15f;
  [BoxGroup("Bomb"), SerializeField] bool haveAmmunition = true;

  public override void Awake()
  {
    base.Awake();
    rb = GetComponent<Rigidbody>();
  }

  public override void Hunt()
  {
    if (haveAmmunition)
    {
      if (target)
      {
        if (Time.frameCount % 10 == 0)
        {
          float xx = Mathf.Clamp(target.transform.position.x - transform.position.x, -maxSpeed, maxSpeed);
          float zz = Mathf.Clamp(target.transform.position.z - transform.position.z, -maxSpeed, maxSpeed);
          Vector3 direction = new Vector3(xx, 0, zz);

          rb.AddForce(direction * thrust);

          // Quand il est arrivé jeter la bombe
          if (Mathf.Abs(xx) <= lauchDistance && Mathf.Abs(zz) <= lauchDistance)
          {
            StartCoroutine(LaunchBomb());
          }
        }
      }
    }
  }

  IEnumerator LaunchBomb()
  {
    haveAmmunition = false;

    yield return new WaitForSeconds(1f);
    Instantiate(bomb, fakeBomb.transform.position, fakeBomb.transform.rotation);
    StartCoroutine(Reload());
  }

  [Button("Reload Ammunition")]
  IEnumerator Reload()
  {
    fakeBomb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    yield return new WaitForSeconds(reloadDuration);
    haveAmmunition = true;
    fakeBomb.transform.localScale = new Vector3(1f, 1f, 1f);
  }


  public override void Patrol()
  {
    var temp = GameObject.FindGameObjectWithTag("Player");
    if (temp != null)
    {
      target = temp;
      StateAnimator.SetBool("PlayerDetected", true);
      return;
    }
    StateAnimator.SetBool("PlayerDetected", false);
  }

  public override void Dead()
  {
    base.Dead();
    rb.useGravity = true;
    rb.constraints = RigidbodyConstraints.None;
  }
}
