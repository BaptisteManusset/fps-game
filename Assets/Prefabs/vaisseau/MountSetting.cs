using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountSetting : MonoBehaviour
{

  public GameObject player;
  public GameObject mounture;
  public bool playerIsMount = false;
  bool touchPressed = false;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (touchPressed == false)
    {
      if (playerIsMount)
      {
        if (Input.GetKeyDown(KeyCode.E))
        {
          touchPressed = true;
          player.SetActive(true);
          player.GetComponentInChildren<Camera>().enabled = true;
          mounture.GetComponentInChildren<Camera>().enabled = false;
          player.transform.position = transform.position;
          mounture.GetComponentInChildren<Levitation>().enabled = false;
          Invoke("Cooldown", .1f);
        }
      }
    }
  }
  private void OnTriggerStay(Collider other)
  {
    if (touchPressed == false)
    {
      if (playerIsMount == false)
      {
        if (Input.GetKeyDown(KeyCode.E))
        {
          touchPressed = true;
          player.transform.parent = mounture.transform;
          player.SetActive(false);
          player.GetComponentInChildren<Camera>().enabled = false;
          mounture.GetComponentInChildren<Camera>().enabled = true;
          mounture.GetComponentInChildren<Levitation>().enabled = true;
          Invoke("Cooldown",.1f);

        }
      }
    }
  }
  void Cooldown()
  {
    touchPressed = false;
  }
}
