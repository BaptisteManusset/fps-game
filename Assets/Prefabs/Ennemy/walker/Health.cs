using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class Health : MonoBehaviour
{
  [ProgressBar("",5)]
  public float health = 5;
  TextMeshPro display;

  void Start()
  {
    display = GetComponentInChildren<TextMeshPro>();
    display.text = health.ToString();
  }

  public void takeDamage(float damage)
  {
    health = health - damage;
    display.text = health.ToString();
    if (health <= 0)
    {
      die();
    }
  }

  void die()
  {
    Destroy(gameObject);
  }
}
