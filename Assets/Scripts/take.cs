using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class take : MonoBehaviour
{

  public float health = 10;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void damage(float number)
  {
    Debug.Log("Hoo!");
    health -= health;

    if (health <= 0)
    {
      die();
    }
  }

  private void die()
  {
    Destroy(gameObject);  
  }
}
