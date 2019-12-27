using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

  [HideInInspector] public GameObject player = null;
  // [SerializeField] private AudioClip soundOnToggle = null;

  private void Awake()
  {


  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      AudioSource aus = GetComponent<AudioSource>();
      MeshRenderer mshr = GetComponentInChildren<MeshRenderer>();
      player = other.gameObject;
      doAction();
      mshr.enabled = false;
      aus.Play();
      Destroy(gameObject, aus.clip.length);

    }
  }

  public virtual void doAction()
  { }
}
