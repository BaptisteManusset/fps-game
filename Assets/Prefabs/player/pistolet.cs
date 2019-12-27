using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pistolet : MonoBehaviour
{

  public GameObject bullet;
  public GameObject canon;


  [BoxGroup("Raycast")] public LayerMask layers;
  private int recoilDuration = 0;

  RaycastHit hit;
  LineRenderer lineRenderer;

  Camera cam;

  [ReadOnly] public GunStat gunCurrent;
  public GunStat[] guns;

  public int gunSelection = 0;



  private void Awake()
  {
    cam = GetComponent<Camera>();

    lineRenderer = GetComponent<LineRenderer>();
    lineRenderer.useWorldSpace = true;

  }

  private void Start()
  {
    GetGun();
  }

  void Update()
  {
    if (Input.GetButton("Fire1"))
    {
      if (gunCurrent != null)
      {

        Fire();
      }
      else
      {
        Debug.Log("Il n'y a pas d'arme");
      }
    }
    if (recoilDuration < gunCurrent.recoil)
    {
      recoilDuration++;
      //DrawUi();
    }
  }

  void Fire()
  {

    if (recoilDuration >= gunCurrent.recoil)
    {
      //drawLine(true);
      //Invoke("drawLine(false)", 0.1f);
      Vector3 direction = transform.forward;
      for (int i = 0; i < gunCurrent.quantity; i++)
      {
        direction.x += Random.Range(-gunCurrent.spreadFactor, gunCurrent.spreadFactor);
        direction.y += Random.Range(-gunCurrent.spreadFactor, gunCurrent.spreadFactor);
        direction.z += Random.Range(-gunCurrent.spreadFactor, gunCurrent.spreadFactor);
        Vector3 position = cam.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0));
        if (Physics.Raycast(position, direction, out hit, gunCurrent.rayLength, layers))
        {
          #region Apparition et disparition de la balle
          Debug.DrawRay(canon.transform.position, canon.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

          var bulletInstance = Instantiate(bullet, hit.point, Quaternion.identity);
          Destroy(bulletInstance, 5f);
          bulletInstance.transform.parent = hit.collider.gameObject.transform;
          #endregion

          #region Gestion de la vie
          StateController2 hl = hit.collider.gameObject.GetComponent<StateController2>();
          if (hl != null)
          {
            hl.takeDamage(gunCurrent.damage);
          }
          #endregion

          #region application de la force d'explosion
          Collider[] targets = Physics.OverlapSphere(hit.point, radius: 5);
          foreach (Collider nearObject in targets)
          {
            if (nearObject.CompareTag("Player"))
              continue;

            Rigidbody rb = nearObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
              rb.AddExplosionForce(gunCurrent.ExplosionForce, hit.point, gunCurrent.ExplosionRadius);
            }


          }
          #endregion
        }
      }
      //recoilDuration = gunCurrent.recoil;
      recoilDuration = 0;

    }
  }

  void drawLine(bool draw)
  {

    if (draw)
    {
      if (lineRenderer.enabled == false) lineRenderer.enabled = true;
      lineRenderer.SetPosition(0, canon.transform.position);

      if (Physics.Raycast(canon.transform.position, canon.transform.forward, out hit, gunCurrent.rayLength, layers))
      {
        lineRenderer.SetPosition(1, hit.point);
      }
      else
      {
        lineRenderer.SetPosition(1, canon.transform.TransformPoint(Vector3.forward * 20));
      }
    }
    else
    {
      lineRenderer.enabled = false;
    }
  }

  [Button("Get Gun")]
  void GetGun()
  {
    UngetGun();
    gunCurrent = guns[gunSelection];
    //DrawUi();
  }

  [Button("Loose Gun")]
  void UngetGun()
  {
    gunCurrent = null;
  }

  [Button("Next Gun")]
  void ChangementDegun()
  {
    if (gunSelection < guns.Length - 1)
    {
      gunSelection++;
      GetGun();
      return;
    }
    gunSelection = 0;
    recoilDuration = gunCurrent.recoil;
    //DrawUi();
  }



  
}
