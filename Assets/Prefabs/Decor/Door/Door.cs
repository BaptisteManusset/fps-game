using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

  private Animator anim;

  private void Awake()
  {
    anim = GetComponent<Animator>();
  }

  [Button]
  public void Open()
  {
    anim.SetTrigger("open");
  }

  [Button]
  public void Close()
  {
    anim.SetTrigger("close");
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      anim.SetTrigger("open");
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      anim.SetTrigger("close");
    }
  }
}
