using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class StateMachineMain : MonoBehaviour
{

  [HideInInspector] public Animator StateAnimator;
  [HideInInspector] public AudioSource audioSource;


  public enum States { idle, patrol, hunt, dead };
  public States state;
  [ProgressBar("", 5)] public float health = 5;
  public GameObject target;
  bool isDead = false;
  [BoxGroup("Sound")] public AudioClip soundDeath;



  public virtual void Awake()
  {
    StateAnimator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
  }

  void FixedUpdate()
  {
    if (!isDead)
    {

      if (StateAnimator.GetCurrentAnimatorStateInfo(0).IsName("dead"))
      {
        state = States.dead;
        Dead();
      }
      else if (StateAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
      {
        state = States.idle;
        Idle();
      }
      else if (StateAnimator.GetCurrentAnimatorStateInfo(0).IsName("patrol"))
      {
        state = States.patrol;
        Patrol();
      }
      else if (StateAnimator.GetCurrentAnimatorStateInfo(0).IsName("hunt"))
      {
        state = States.hunt;
        Hunt();
      }
    }
  }

  public virtual void Hunt()
  { }

  public virtual void Patrol()
  { }

  public virtual void Idle()
  {
    StateAnimator.SetTrigger("PatrolRequired");
  }

  public virtual void Dead()
  {
    audioSource.clip = soundDeath;
    if (!audioSource.isPlaying)
      audioSource.Play();
  }


  public void SetDestination(GameObject target)
  { }

  public void takeDamage(float damage)
  {
    updateHealth(damage);
  }


  private void updateHealth(float damage)
  {

    if (health > 0)
    {
      health = health - damage;
    }
    else
    {
      StateAnimator.SetTrigger("Dying");
      StateAnimator.SetBool("Dead",true);
    }
  }
}
