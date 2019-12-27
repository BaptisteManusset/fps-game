using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
  public float radius = 1;
  public float explosionForce = 1;

  private void Start()
  {
    Invoke("boom",5f);
  }

  void boom()
  {
    Debug.Log("Boom");
    Collider[] targets = Physics.OverlapSphere(transform.position, radius);
    foreach (Collider nearObject in targets)
    {
      Rigidbody rb = nearObject.GetComponent<Rigidbody>();

      if(rb != null)
      {
        rb.AddExplosionForce(explosionForce, transform.position, radius);
      }
    }
    Invoke("boom", 5f);
  }
}
