using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailGenerator : MonoBehaviour
{

  public ParticleSystem ps;
  //public Levitation levitation;
  public Rigidbody rg;
  public TrailRenderer trailRenderer;
  float particleLifetime;
  [Range(0, 3)]
  [SerializeField]
  float multiple = 1;

  private void Awake()
  {
   /* particleSystem = GetComponent<ParticleSystem>();
    hoverMotor = GetComponent<HoverMotor>();
    rigidbody = GetComponent<Rigidbody>();
    trailRenderer = GetComponentInChildren<TrailRenderer>();*/
  }

  void Update()
  {

    var emission = ps.emission;
    emission.rateOverDistance = Mathf.Ceil(rg.velocity.magnitude * multiple);
    trailRenderer.time = rg.velocity.magnitude / 10;

  }

}
