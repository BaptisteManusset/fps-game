using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWithTimer : MonoBehaviour
{

  [SerializeField] private GameObject explosion;
  [SerializeField] private int countdown = 1;
  [SerializeField, ReadOnly] private bool isTrigger = false;
  private Rigidbody rb;
  private void Awake()
  {
    rb = GetComponent<Rigidbody>();
  }
  private IEnumerator ExplosionTimer()
  {
    yield return new WaitForSeconds(countdown);
    GameObject explos = Instantiate(explosion, transform.position, transform.rotation);
    explos.GetComponent<AudioSource>().Play();
    yield return new WaitForSeconds(.1f);
    Destroy(gameObject);
  }
  private void Start()
  {
    StartCoroutine(ExplosionTimer());
  }
}
