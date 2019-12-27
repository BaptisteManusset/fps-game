using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
  [BoxGroup("Vie"), ProgressBar("Vie", 20f)] public float health = 20;
  public float maxHealth = 20;



  public static Camera cam;

  private void Start()
  {
    //   cam = GetComponentInChildren<Camera>();
    DrawUi();
  }

  internal void updateHeath(float healthCount)
  {
    health += healthCount;
    health = Mathf.Clamp(health, 0, maxHealth);
    DrawUi();
  }

  public void takeDamage(float healthCount)
  {
    updateHeath(-healthCount);
  }


  [Button("Vie +5")]
  void AddHealth()
  {
    updateHeath(5);
  }

  [Button("Vie -5")]
  void RemoveHealth()
  {
    updateHeath(-5);
  }

  void DrawUi()
  {
    // Debug.Log("Game.uiManager " + Game.uiManager.ToString());
    //Debug.Log("Game.uiManager.UIHealthValue " + Game.uiManager.UIHealthValue.ToString());
    Game.uiManager.UIHealthValue.text = health.ToString();
    Game.uiManager.UIHealthFill.fillAmount = health / maxHealth;
  }

}
