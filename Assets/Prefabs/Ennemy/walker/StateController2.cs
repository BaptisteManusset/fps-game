using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using Random = UnityEngine.Random;

public class StateController2 : MonoBehaviour
{
  private Animator StateAnimator;
  private Renderer rend;
  public TextMeshPro display;

  public enum States { idle, patrol, hunt, dead };
  public States state;

  [ProgressBar("", 5)] public float health = 5;


  [BoxGroup("Mort")] public GameObject explosionObj;
  [BoxGroup("Mort")] public GameObject[] Drops;
  [BoxGroup("Mort")] public AudioClip deathSound;
  public bool isDead = false;

  private AudioSource aus;
  void Start()
  {
    StateAnimator = GetComponent<Animator>();
    rend = GetComponent<MeshRenderer>();
    aus = GetComponent<AudioSource>();
  }

  void FixedUpdate()
  {

    if (StateAnimator.GetCurrentAnimatorStateInfo(0).IsName("dead"))
    {
      state = States.dead;
      Dead();
    }

    if (StateAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
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
    ///////////////////////////////////////////////////////////////////////////
    //  var slt = StateAnimator.GetCurrentAnimatorStateInfo(0);
    //  Debug.Log($"{state.ToString()} > {slt.tagHash}");
    ///////////////////////////////////////////////////////////////////////////
    display.text = state.ToString();
  }

  private void Dead()
  {
    aus.clip = deathSound;
    if (!aus.isPlaying)
      aus.Play();
    GameObject drop = Drops[0];
    Instantiate(drop, transform.position, Quaternion.identity);



    GameObject expl = Instantiate(explosionObj, transform.position, Quaternion.identity);
    Destroy(expl, 2f);
    Destroy(gameObject);

  }

  private void Idle()
  {
    rend.material.color = Color.green;
    StateAnimator.SetTrigger("PatrolRequired");
  }

  private void Hunt()
  {
    rend.material.color = Color.yellow;
  }

  void Patrol()
  {
    rend.material.color = Color.red;
  }


  public void SetPlayerDetected(bool detect)
  {

    StateAnimator.SetBool("PlayerDetected", detect);


  }


  public void takeDamage(float damage)
  {
    health = health - damage;
    display.text = health.ToString();
    if (health <= 0)
    {
      StateAnimator.SetTrigger("Dying");
    }
  }


}
