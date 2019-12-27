using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class showEnemy : MonoBehaviour
{

  public TextMeshProUGUI text;

  void Update()
  {
    GameObject[] array = GameObject.FindGameObjectsWithTag("Ennemy");
    text.text = $"{array.Length}E \n";
  }
}
