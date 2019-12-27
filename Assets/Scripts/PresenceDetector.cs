using Homebrew;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenceDetector : MonoBehaviour
{
  [InfoBox("Rayon de detection du joueur par les entités")]
  [BoxGroup("Gizmos")] public bool DisplayGizmos = false;
  [BoxGroup("Gizmos")] public float dectectionRadiusShort = 3;
  [BoxGroup("Gizmos")] public Color colorSmall;
  [BoxGroup("Gizmos")] public float dectectionRadiusMedium = 7;
  [BoxGroup("Gizmos")] public Color colorMedium;
  [BoxGroup("Gizmos")] public float dectectionRadiusLarge = 15;
  [BoxGroup("Gizmos")] public Color colorLarge;


  public int intervalTime;



  void Update()
  {
    if (Game.IsPaused) return;

    DebugExtension.DebugWireSphere(transform.position, colorSmall, dectectionRadiusShort);
    DebugExtension.DebugWireSphere(transform.position, colorMedium, dectectionRadiusMedium);
    DebugExtension.DebugWireSphere(transform.position, colorLarge, dectectionRadiusLarge);

    if (Time.frameCount % intervalTime == 0)
    {
      Collider[] hitColliders = Physics.OverlapSphere(transform.position, dectectionRadiusLarge);
      int i = 0;
      while (i < hitColliders.Length)
      {
        GameObject hitCollider = hitColliders[i].gameObject;
        if (hitCollider.CompareTag("Ennemy") == true)
        {

          float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
          bool detect = false;
          if (distance <= dectectionRadiusMedium)
          {
            detect = true;
            DebugExtension.DebugArrow(hitCollider.transform.position - transform.position, transform.position, Color.magenta);
          }
          if (hitCollider.GetComponent<StateController2>() != null)
            hitCollider.GetComponent<StateController2>().SetPlayerDetected(detect);
        }
        i++;
      }
    }

  }

  void OnDrawGizmosSelected()
  {
    if (DisplayGizmos == false)
      return;
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, dectectionRadiusShort);
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, dectectionRadiusMedium);
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(transform.position, dectectionRadiusLarge);
  }
}
